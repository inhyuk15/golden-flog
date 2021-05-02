using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] Collider2D m_Collider2D;
    [SerializeField] GameObject enemy;

    public int EnemyCnt = 3;
    private int m_EnemyCnt = 0;

    float m_width, m_height;
    // Start is called before the first frame update
    void Start()
    {
        m_width = m_Collider2D.transform.localScale.x;
        m_height = m_Collider2D.transform.localScale.y;

        StartCoroutine(CreateEnemy());
    }

    IEnumerator CreateEnemy()
    {
        while(m_EnemyCnt < EnemyCnt)
        {
            float yPos = transform.position.y + Random.Range(-m_height, m_height) / 2, xPos = transform.position.x + Random.Range(-m_width, m_width) / 2;
            Instantiate(enemy, new Vector2(xPos, yPos), Quaternion.identity);
            m_EnemyCnt++;
            yield return new WaitForSeconds(.5f);
        }
    }
}
