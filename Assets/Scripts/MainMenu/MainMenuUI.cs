using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private static MainMenuUI instance;

    [SerializeField]
    public CanvasGroup MainMenuPanel;
    [SerializeField]
    public CanvasGroup LoadingPanel;
    [SerializeField]
    public CanvasGroup MainMenuBtn;

    [SerializeField]
    public QuantumTek.QuantumUI.QUI_Bar ProgressBar;

    [SerializeField]
    public AudioSource BackgroundAudio;
    public AudioClip[] audioClips;

    [SerializeField]
    public CanvasGroup ClearPanel;
    [SerializeField]
    public CanvasGroup GameOverPanel;

    public bool OnStart = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    IEnumerator LoadScene(string nextStage)
    {
        yield return null;
        LoadingPanel.alpha = 1;

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextStage);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if(ProgressBar.fillAmount < 0.9f)
            {
                ProgressBar.fillAmount = Mathf.MoveTowards(ProgressBar.fillAmount, 0.9f, Time.deltaTime);
            }
            else if(ProgressBar.fillAmount >= 0.9)
            {
                ProgressBar.fillAmount = Mathf.MoveTowards(ProgressBar.fillAmount, 1f, Time.deltaTime);
            }


            if(ProgressBar.fillAmount >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
                MainMenuPanel.alpha = 0f;
                yield return new WaitForSeconds(1f);
                LoadingPanel.alpha = 0f;
                
                MainMenuPanel.blocksRaycasts = false;
            }
        }

        if(nextStage == "MainMenu")
        {
            MainMenuPanel.alpha = 1f;
            MainMenuPanel.blocksRaycasts = true;

            MainMenuBtn.alpha = 0;
            MainMenuBtn.blocksRaycasts = false;

            BackgroundAudio.clip = audioClips[0];
            if (Settings.canSound)
                BackgroundAudio.Play();
            else
                BackgroundAudio.Stop();
        }
        else
        {            
            MainMenuBtn.alpha = 1f;
            MainMenuBtn.blocksRaycasts = true;

            BackgroundAudio.clip = audioClips[1];
            if (Settings.canSound)
                BackgroundAudio.Play();
            else
                BackgroundAudio.Stop();
        }
    }

    public void StartBtn()
    {
        StartCoroutine(LoadScene("Stage1"));
    }

    public void GoMainMenu()
    {
        ClearPanel.alpha = 0;
        ClearPanel.blocksRaycasts = false;

        GameOverPanel.alpha = 0;
        GameOverPanel.blocksRaycasts = false;


        StartCoroutine(LoadScene("MainMenu"));
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(1f);
    }


    // Setting
    public void SetSound(bool sound)
    {
        Settings.canSound = sound;
        if (sound)
        {
            BackgroundAudio.Play();
        }
        else
        {
            BackgroundAudio.Stop();
        }
    }

    public void SetVolume(float sliderValue)
    {
        Settings.volume = sliderValue / 100;
        
        BackgroundAudio.volume = sliderValue / 100;
    }

    public void SetScoreZero()
    {
        ScoreManager.CurScore = 0;
        ScoreManager.CurLife = 3;

        SaveData.Save();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
