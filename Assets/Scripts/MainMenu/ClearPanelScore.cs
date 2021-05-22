using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearPanelScore : MonoBehaviour
{
    [SerializeField]
    public Text ScoreText;

    public void UpdateScore()
    {
        ScoreText.text = ScoreManager.CurScore.ToString();
    }
}
