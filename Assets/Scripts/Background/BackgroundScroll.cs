using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

    [SerializeField] private float m_ScrollSpeed = .01f;

    private SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        float nextDist = Mathf.Repeat(Time.time * m_ScrollSpeed, 1);
        m_SpriteRenderer.material.mainTextureOffset = new Vector2(nextDist, 0);
        
    }
}
