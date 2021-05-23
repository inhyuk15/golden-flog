using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public GameObject Explore;
    private bool isImmune = false;

    [SerializeField]
    public AudioClip m_DestroySound;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && ScoreManager.CurLife > 0)
        {
            if(!isImmune)
            {
                isImmune = true;
                StartCoroutine(SetDamage(other));
            }
        }

        if(ScoreManager.CurLife <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        var panel = GameObject.Find("GameOverPanel").GetComponent<CanvasGroup>();
        panel.gameObject.GetComponent<ClearPanelScore>().UpdateScore();
        panel.alpha = 1;
        panel.blocksRaycasts = true;
    }

    
    IEnumerator SetDamage(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().SetDamage(1);
        yield return new WaitForSeconds(2f);
        isImmune = false;
    }

    // Player에서 호출
    public void SetDamage(int damage)
    {
        Instantiate(Explore, gameObject.transform);

        // Score
        ScoreManager.DefeatEnemy(30);
        AudioSource.PlayClipAtPoint(m_DestroySound, transform.position, Settings.volume);

        StartCoroutine(Destroy());
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(0.3f);

        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(gameObject);
    }
}



