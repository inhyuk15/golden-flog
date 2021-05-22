using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] GameObject m_FeedbackEffect;
    [SerializeField] AudioClip m_GettingSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SetScore();
        }
    }

    void SetScore()
    {
        ScoreManager.GetLife();
        Instantiate(m_FeedbackEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(m_GettingSound, transform.position, Settings.volume);

        Destroy(gameObject);
    }
}
