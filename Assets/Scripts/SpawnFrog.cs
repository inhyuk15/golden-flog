using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFrog : SpawnPoint
{
    protected override IEnumerator CreateEnemy()
    {
        while (m_EnemyCnt < EnemyCnt)
        {
            float yPos = transform.position.y + Random.Range(-m_height, m_height) / 2, xPos = transform.position.x + Random.Range(-m_width, m_width) / 2;

            int nextColor = Random.Range(0, 5);
            bool nextFlip = (Random.Range(0, 1) % 2 == 0 ? true : false);

            var frog = Instantiate(m_Prefab, new Vector2(xPos, yPos), Quaternion.identity).GetComponent<Frog>();
            frog.transform.parent = gameObject.transform;

            frog.SetColor(FrogColor.colors[nextColor]);
            frog.Flip(nextFlip);
            

            

            m_EnemyCnt++;
            yield return new WaitForSeconds(.5f);
        }
    }
}

public class FrogColor
{
    public static List<Color> colors = new List<Color>
    {
        new Color(0.376f,0.622f,0.237f),
        new Color(0.830f,0.019f,0.753f),
        new Color(0.416f,0.372f,1f),
        new Color(0.269f,0.787f,0.877f),
        new Color(1f,0.548f,0f),
        new Color(0.1320784f,1f,0f),
    };
}