using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool m_StandBy = false;
    private int cnt = 0;

    private void Update()
    {
        if(transform.parent.name == "Portal")
        {
            if (m_StandBy && cnt >= 3)
            {

                GameObject.Find("GameManager").SendMessage("GoNextStage");
            }
            
        }
        else if(transform.parent.name == "StartPoint")
        {
            if (m_StandBy && cnt > 3)
            {
                GameObject.Find("GameManager").SendMessage("GoPrevStage");
            }
        }
    }

    IEnumerator CountTimeForTransfer()
    {
        cnt = 0;
        while (true)
        {
            yield return StartCoroutine(WaitOneSecond());
        }       
    }

    IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);
        cnt++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            m_StandBy = true;
            StartCoroutine(CountTimeForTransfer());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            m_StandBy = false;
            StopCoroutine(CountTimeForTransfer());
        }
    }
}
