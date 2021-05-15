using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    Player Player;
    Transform StartPoint;

    [SerializeField]
    private Text m_ScoreText;
    [SerializeField]
    private Text m_CherryText;
    [SerializeField]
    private Text m_GemText;
    [SerializeField]
    private Text m_LifeText;

    [Header("Score 글자크기")]
    public int ScoreSize = 20;
    public int NumberSize = 15;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Player = GameObject.FindObjectOfType<Player>();
        StartPoint = GameObject.Find("StartPoint").transform;
    }

    void SetScore()
    {
        m_CherryText.text = string.Format("{0}", ScoreManager.CurCherry);
        m_GemText.text = string.Format("{0}", ScoreManager.CurGem);
        m_LifeText.text = string.Format("{0}", ScoreManager.CurLife);

        m_ScoreText.text = string.Format("<size={1}>Score</size>\n<size={2}>{0}</size>", ScoreManager.CurScore, ScoreSize, NumberSize);
    }

    // Update is called once per frame
    void Update()
    {
        SetScore();
    }

    void GoNextStage()
    {        
        SceneManager.LoadScene("Stage" + (int.Parse(SceneManager.GetActiveScene().name.Substring(5,1)) + 1));
        Player.transform.position = StartPoint.position;
    }

    void GoPrevStage()
    {
        SceneManager.LoadScene("Stage" + (int.Parse(SceneManager.GetActiveScene().name.Substring(5,1)) - 1));
    }
}
