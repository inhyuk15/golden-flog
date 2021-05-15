using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingBtn : MonoBehaviour
{
    public SettingUI SettingPanel;
    // Start is called before the first frame update

    private void Start()
    {
        
    }

    public void SetActive()
    {
        SettingPanel = GameObject.Find("SettingPanel").GetComponent<SettingUI>();
        SettingPanel.Open();
    }
}
