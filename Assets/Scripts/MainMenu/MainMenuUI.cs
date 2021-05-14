using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    public CanvasGroup LoadingPanel;

    [SerializeField]
    public QuantumTek.QuantumUI.QUI_Bar ProgressBar;

    public bool OnStart = false;

    private void Start()
    {
    }



    IEnumerator LoadScene()
    {
        yield return null;
        LoadingPanel.alpha = 1;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Stage1");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return new WaitForSeconds(1f);
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
                yield return new WaitForSeconds(1f);
                LoadingPanel.alpha = 0f;
            }
        }
    }

    public void StartBtn()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(1f);
    }

    public void Settings()
    {

    }
}
