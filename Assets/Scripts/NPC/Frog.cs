using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Frog : NPC
{
    NPC_Controller m_NPC_Controller;
    Animator m_Animator;

    public UnityEvent CrockEvent;

    private void Awake()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        m_NPC_Controller = GetComponent<NPC_Controller>();
        m_Animator = GetComponent<Animator>();
        StartCoroutine(Roaming());
    }

    public float waitInterval = 1f;

    IEnumerator Roaming()
    {
        while (true)
        {
            int nextMove = Random.Range(0, 3) - 1;
            bool jump = (nextMove != 0) ? true : false;
            if(jump)
                m_NPC_Controller.Movement(nextMove, jump);
            else
            {
                int nextCrock = Random.Range(0, 3);
                if (nextCrock > 1) CrockEvent.Invoke();
            } 
            yield return new WaitForSeconds(waitInterval);
        }
    }

    public void Jump()
    {
        m_Animator.SetTrigger("jump");
    }

    public void Crock()
    {
        m_Animator.SetTrigger("crock");
    }


}
