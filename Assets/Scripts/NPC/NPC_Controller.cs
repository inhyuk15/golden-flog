using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC_Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private GameObject m_CheckGroundAhead;

    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    
    public UnityEvent OnJumpEvent;

    // Start is called before the first frame update
    void Awake()
    {
        //m_Rigidbody2D = GetComponent<Rigidbody2D>();

        //m_SpriteRenderer = GetComponent<SpriteRenderer>();

        // localScale의 x가 -1인거 고려해서
        m_CheckGroundAhead.transform.localPosition = new Vector2(-2, 0);
    }

    protected bool facingRight = false;

    public float moveSpeed = 1f;
    public float jumpPower = 5f;

    public void Movement(float move, bool jump)
    {
        
        // Flip
        if (move > 0 && facingRight)
        {
            Flip();
        }
        else if (move < 0 && !facingRight)
        {
            Flip();
        }

        m_Rigidbody2D.velocity = new Vector2(move * moveSpeed, m_Rigidbody2D.velocity.y);

        //jump
        if (jump)
        {
            if(OnJumpEvent != null)
                OnJumpEvent.Invoke();
            m_Rigidbody2D.AddForce(new Vector2(0, jumpPower));
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        //Vector3 nextScale = transform.localScale;

        //nextScale.x *= -1;
        //transform.localScale = nextScale;
        m_SpriteRenderer.flipX = facingRight; 
    }

}
