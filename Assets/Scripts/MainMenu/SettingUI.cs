using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SettingUI : MonoBehaviour
{
    Animator m_Animator;

    [SerializeField]
    public Button SaveButton;

    [SerializeField]
    public CanvasGroup SettingPanelCanvasGroup;

    private void Awake()
    {
        SaveData.Load();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        UnityAction action = SaveData.Save;
        SaveButton.onClick.AddListener(action);
    }

    public void Open()
    {
        SettingPanelCanvasGroup.alpha = 1;
        SettingPanelCanvasGroup.blocksRaycasts = true;
        m_Animator.SetBool("open", true);
    }
    

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        m_Animator.SetBool("open", false);
        yield return new WaitForSeconds(0.5f);
        SettingPanelCanvasGroup.alpha = 0;
        SettingPanelCanvasGroup.blocksRaycasts = false;

    }
}
