using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject Player;
    Transform StartPoint;
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
        Player = GameObject.Find("Player");
        StartPoint = GameObject.Find("StartPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
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
