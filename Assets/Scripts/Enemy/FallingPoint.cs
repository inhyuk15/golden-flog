using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPoint : MonoBehaviour
{
    [SerializeField]
    public Transform SpawnPoint;
    [SerializeField]
    public AudioClip m_FallingSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && ScoreManager.CurLife > 0)
        {
            Vector3 nextPoint = SpawnPoint.position;
            nextPoint.z = other.transform.position.z;
            other.transform.position = nextPoint;

            //GameManager.instance.playerPosition.position = nextPoint;
            AudioSource.PlayClipAtPoint(m_FallingSound, transform.position);

            other.SendMessage("SetDamage", 1);
        }
    }
}
