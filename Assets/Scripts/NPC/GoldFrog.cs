using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFrog : MonoBehaviour
{
    public CanvasGroup clearPanel;
    public Transform startPosition;
    public Transform PlayerPos;

    private void Start()
    {
        startPosition = GameObject.Find("StartPoint").transform;
        clearPanel = GameObject.Find("ClearPanel").GetComponent<CanvasGroup>();
    }

    public void ShowClearPanel()
    {
        clearPanel.gameObject.GetComponent<ClearPanelScore>().UpdateScore();

        clearPanel.alpha = 1;
        clearPanel.blocksRaycasts = true;


        PlayerPos = GameObject.Find("Player").transform;
        PlayerPos.position = startPosition.position;
    }
}
