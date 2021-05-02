using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    NPC_Controller m_NPC_Controller;

    private void Awake()
    {
        m_NPC_Controller = GetComponent<NPC_Controller>();
        StartCoroutine(Roaming());
    }

    public float waitInterval = 1f;

    IEnumerator Roaming()
    {
        while (true)
        {
            int nextMove = Random.Range(-1, 2);
            m_NPC_Controller.Movement(nextMove, false);
            yield return new WaitForSeconds(waitInterval);
        }
    }

}
