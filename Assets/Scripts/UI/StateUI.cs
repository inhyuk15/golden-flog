using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class StateUI : MonoBehaviour
{
    public static StateUI instance;

    [SerializeField]
    public CanvasGroup cg;
    [SerializeField]
    public AudioListener listener;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            SceneManager.sceneLoaded += SetVisible;
            SceneManager.sceneLoaded += AudioListenerOnOff;
            SceneManager.sceneUnloaded += SetUnvisible;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AudioListenerOnOff(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu") listener.enabled = false;
        else listener.enabled = true;
    }

    public void SetVisible(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu") return;

        cg.alpha = 1f;
        cg.blocksRaycasts = true;
    }

    public void SetUnvisible(Scene scene)
    {
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
    }
}
