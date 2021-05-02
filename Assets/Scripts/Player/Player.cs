using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// animation, input Ã³¸®
public class Player : MonoBehaviour
{
    static public Player instance;

    Animator m_animator;
    Rigidbody2D m_rigidBody2D;
    BoxCollider2D m_collider2D;

    Player_Controller m_playerController; 

    private float speedX, speedY;
    private bool crouch, move, jump;

    private Vector2 velocity;

    private void OnEnable()
    {
        m_playerController = GetComponent<Player_Controller>();
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
    }

  

    private void FixedUpdate()
    {
        m_playerController.Movement(speedX, jump, crouch);
        

        m_animator.SetFloat("speedY", m_rigidBody2D.velocity.y);
    }


    public void onLand()
    {
        jump = false;
        m_animator.SetBool("onGround", true);
    }

    public void onCrouch()
    {
        m_animator.SetBool("crouch", crouch);
    }

    public void onJump()
    {
        m_animator.SetBool("onGround", false);
        m_animator.SetBool("jump", true);
    }

    void SetDamage(float damage)
    {
        m_animator.SetTrigger("hurt");
    }
}
