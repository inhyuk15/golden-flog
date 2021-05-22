using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject PlayerPrefeb;
    GameObject PlayerInstance;

    [SerializeField]
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


    public Transform playerPosition;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject); 
        }
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*
        if(scene.name != "MainMenu" && playerPosition == null)
        {
            playerPosition = GameObject.Find("StartPoint").GetComponent<Transform>();
            Debug.Log("Go main");
        }

        string path = Application.dataPath + "/PlayerPosition.json";

        // Save
        if (scene.name == "MainMenu" && PlayerInstance != null)
        {
            playerPosition = PlayerInstance.GetComponent<Transform>();
            string data = JsonConvert.SerializeObject(playerPosition);
            File.WriteAllText(path, data);

            Debug.Log("Save");
            Destroy(PlayerInstance.gameObject);
        }
        // Load
        else if (scene.name == "Stage1")
        {
            if (File.Exists(path) && PlayerInstance == null)
            {
                string jData = File.ReadAllText(path);
                playerPosition = JsonConvert.DeserializeObject<Transform>(jData);
                PlayerInstance = Instantiate(PlayerPrefeb, playerPosition.position, Quaternion.identity);
                PlayerInstance.transform.position = playerPosition.position;

                Debug.Log(PlayerInstance.transform.position);
            }
        }
        */
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
        PlayerPrefeb.transform.position = StartPoint.position;
    }

    void GoPrevStage()
    {
        SceneManager.LoadScene("Stage" + (int.Parse(SceneManager.GetActiveScene().name.Substring(5,1)) - 1));
    }

    public void GameOver(bool over)
    {
        Debug.Log("Game Over");
    }
}
