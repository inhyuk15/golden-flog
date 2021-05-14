using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// animation, input 처리
public class Player : MonoBehaviour
{
    static public Player instance;

    Animator m_animator;
    Rigidbody2D m_rigidBody2D;
    BoxCollider2D m_collider2D;

    [SerializeField]
    Player_Controller m_playerController; 

    [SerializeField]
    private float speedX, speedY;
    private float climbVelocity;

    private bool crouch, move, jump, climb;


    [SerializeField]
    private Vector2 velocity;

    private void OnEnable()
    {
        m_animator = GetComponent<Animator>();
        m_rigidBody2D= GetComponent<Rigidbody2D>();
        m_collider2D = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
    }

    void getInput()
    {
        speedX = Input.GetAxis("Horizontal");
        move = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            jump = true;
        }

        crouch = Input.GetKey(KeyCode.DownArrow);

        m_animator.SetBool("move", move);
        m_animator.SetFloat("speedX", speedX);

        m_animator.SetBool("jump", jump);


        // 사다리 위에 있을 때 키를 누른다면 사다리를 타는거
        if (m_playerController.m_OnLaddered)
        {
            climbVelocity = 0f;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                climbVelocity = 1f;
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                climbVelocity = -1f;
            }
            
            
            if (climbVelocity != 0)
            {
                climb = true;
                m_animator.SetFloat("climbSpeed", 1f);
            }
            else
            {
                m_animator.SetFloat("climbSpeed", 0f);
            }
        }
        else
        {
            climb = false;
            climbVelocity = 0f;
            m_animator.SetFloat("climbSpeed", 1f);
        }
    }

  

    private void FixedUpdate()
    {
        m_playerController.Movement(speedX, jump, crouch, climb, climbVelocity);

        m_animator.SetFloat("speedY", m_rigidBody2D.velocity.y);
    }


    public void OnLand()
    {
        jump = false;
        m_animator.SetBool("onGround", true);
    }

    public void OnCrouch(bool _crouch)
    {
        m_animator.SetBool("crouch", _crouch);
    }

    public void OnJump()
    {
        m_animator.SetBool("onGround", false);
        m_animator.SetBool("jump", true);
    }

    public void OnClimb(bool _climb)
    {
        m_animator.SetBool("climb", _climb);
    }

    public void OnLadder(bool _OnLadder)
    {
        m_animator.SetBool("onLadder", _OnLadder);
    }

    void SetDamage(float damage)
    {
        m_animator.SetTrigger("hurt");
    }

}
