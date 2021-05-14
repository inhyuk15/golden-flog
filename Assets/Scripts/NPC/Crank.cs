using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    [SerializeField]
    public GameObject Bridge;
    [SerializeField]
    public GameObject Effect;

    [SerializeField]
    public Transform[] Parent;

    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;
    [SerializeField]
    private Sprite m_NextSprite;

    public void MakeBridge()
    {
        for(int i = 0; i< Parent.Length; i++)
        {
            Transform nextPos = Parent[i];

            // effect
            Instantiate(Effect, nextPos.position, Quaternion.identity);
            // bridge
            Instantiate(Bridge, nextPos.position, Quaternion.identity);
        }

        m_SpriteRenderer.sprite = m_NextSprite;

        
        Destroy(gameObject.GetComponent<QuestGiver>());
    }


}
