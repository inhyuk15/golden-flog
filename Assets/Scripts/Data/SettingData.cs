using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SettingData
{
    public bool canSound;
    public int langauge;

    public int Language;
    public int Life;
    public float Score;
    public int Gem;
    public int Cherry;


    public void RenewData()
    {
        Life = ScoreManager.CurLife;
        Score = ScoreManager.CurScore;
        Gem = ScoreManager.CurGem;
        Cherry = ScoreManager.CurCherry;

        // Setting
        canSound = Settings.canSound;
        langauge = Settings.langauge;
    }
}
