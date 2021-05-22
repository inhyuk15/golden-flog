using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private LayerMask m_WhatIsLadder;
    [SerializeField] private LayerMask m_WhatIsEnemy;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_CeilingCheck;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private Collider2D m_CrouchDisabledCollider;


    Rigidbody2D m_rigidbody2D;


    private bool m_Grounded;
    public bool m_OnLaddered;
    private bool m_OnEnemy;

    private float k_GroundedRadius = 0.1f;
    private float k_CeilingRadius = 0.1f;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { };

    public BoolEvent OnCrouchEvent;
    public UnityEvent OnJumpEvent;

    public BoolEvent OnClimbEvent;
    public BoolEvent OnLadderEvent;

    private bool wasCrouching = false;
    private bool canStand = true;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        if(OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
        if (OnJumpEvent == null)
            OnJumpEvent = new UnityEvent();
        if (OnClimbEvent == null)
            OnClimbEvent = new BoolEvent();
        if (OnLadderEvent == null)
            OnLadderEvent = new BoolEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // ground
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] ground_colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for(int i= 0; i< ground_colliders.Length; i++)
        {
            if(ground_colliders[i].gameObject != this.gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }

        // ladder
        Collider2D[] ladder_colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsLadder);
        for (int i = 0; i < ladder_colliders.Length; i++)
        {
            if (ladder_colliders[i].gameObject != this.gameObject)
            {
                m_OnLaddered = true;
                OnLadderEvent.Invoke(true);
            }
        }

        if(ladder_colliders.Length == 0)
        {
            m_OnLaddered = false;
            // 사다리에서 벗어난 경우
            OnLadderEvent.Invoke(false);
            OnClimbEvent.Invoke(false);
        }



        if (!wasGrounded && !m_Grounded)
        {
            OnJumpEvent.Invoke();
        }
        
        // ceiling
        Collider2D[] ceiling_colliders = Physics2D.OverlapCircleAll(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);

        canStand = true;
        for (int i=0; i< ceiling_colliders.Length; i++)
        {
            if(ceiling_colliders[i].gameObject != this.gameObject)
            {
                if (wasCrouching)
                {
                    canStand = false;
                }
            }
        }

        // enemy
        Collider2D[] enemy_colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_CeilingRadius, m_WhatIsEnemy);

        for(int i =0; i< enemy_colliders.Length; i++)
        {
            if(enemy_colliders[i].gameObject != this.gameObject)
            {
                m_OnEnemy = true;
                enemy_colliders[i].GetComponentInParent<Enemy>().SetDamage(1);
            }
        }

    }

    public float moveSpeed = 10f;
    public float jumpPower = 10f;
    public float crouchSpeed = 0.5f;
    public float climbSpeed = 6f;


    public void Movement(float move, bool jump, bool crouch, bool climb, float climbDir)
    {
        // crouch
        if (m_Grounded)
        {            
            if (crouch || !canStand)
            {
                if (!wasCrouching)
                {
                    wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                if (m_CrouchDisabledCollider != null)
                    m_CrouchDisabledCollider.enabled = false;

                move *= crouchSpeed;

            }
            else
            {
                // crouch 버튼을 떼지만 설 수 없는 경우 그냥 넘어간다.
                if (canStand)
                {
                    if (wasCrouching)
                    {
                        wasCrouching = false;
                        OnCrouchEvent.Invoke(false);
                    }

                    if (m_CrouchDisabledCollider != null)
                        m_CrouchDisabledCollider.enabled = true;
                }
            }
        }

        // move
        if (m_Grounded || m_AirControl)
        {
            m_rigidbody2D.velocity = new Vector2(move * moveSpeed, m_rigidbody2D.velocity.y);
        }

        // climb
        if (m_OnLaddered && climb)
        {
            OnClimbEvent.Invoke(true);
            if (climbDir != 0)
            {
                m_rigidbody2D.velocity = new Vector2(move * moveSpeed, climbSpeed * climbDir);
            }
            else if (climbDir == 0)
            {
                m_rigidbody2D.velocity = new Vector2(move * moveSpeed, 0);
            }
        }
        else
        {
            OnClimbEvent.Invoke(false);
        }



        // jump
        if ((m_Grounded && jump) || m_OnEnemy)
        {
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, 0);

            m_rigidbody2D.AddForce(new Vector2(0, jumpPower));
            m_Grounded = false;
            m_OnEnemy = false;

            OnJumpEvent.Invoke();
        }



        // flip
        if (move > 0 && facingRight)
        {
            Flip();
        }
        else if (move < 0 && !facingRight)
        {
            Flip();
        }
    }

    private bool facingRight = false;

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 nextScale = transform.localScale;
        nextScale.x *= -1;
        transform.localScale = nextScale;
    }
}
