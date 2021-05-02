using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_CeilingCheck;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private Collider2D m_CrouchDisabledCollider;


    Rigidbody2D m_rigidbody2D;


    private bool m_Grounded;
    

    private float k_GroundedRadius = 0.1f;
    private float k_CeilingRadius = 0.1f;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { };

    public UnityEvent OnLandEvent;
    public BoolEvent OnCrouchEvent;
    public UnityEvent OnJumpEvent;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        
        Collider2D[] ground_colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for(int i= 0; i< ground_colliders.Length; i++)
        {
            if(ground_colliders[i].gameObject != gameObject)
            {
                
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }

        if (!wasGrounded && !m_Grounded)
        {
            OnJumpEvent.Invoke();
        }
        
        Collider2D[] ceiling_colliders = Physics2D.OverlapCircleAll(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);

        canStand = true;
        for (int i=0; i< ceiling_colliders.Length; i++)
        {
            if(ceiling_colliders[i].gameObject != gameObject)
            {
                if (wasCrouching)
                {
                    canStand = false;
                }
            }
        }

    }

    public float moveSpeed = 10f;
    public float jumpPower = 10f;
    public float crouchSpeed = 0.5f;

    public void Movement(float move, bool jump, bool crouch)
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
        

        // jump
        if (m_Grounded && jump)
        {
            m_rigidbody2D.AddForce(new Vector2(0, jumpPower));
            m_Grounded = false;
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
