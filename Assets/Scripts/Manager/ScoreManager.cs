using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    static public float CurScore = 0;
    static public int CurLife = 3;
    static public int CurCherry = 0;
    static public int CurGem = 0;

    static public void GetGem()
    {
        CurGem++;
        CurScore += Settings.GemScore;
    }

    static public void GetCherry()
    {
        CurCherry++;
        CurScore += Settings.CherryScore;
    }

    static public void GetLife()
    {
        CurLife++;
    }

    static public void LoseLife()
    {
        CurLife--;
    }

    static public void DefeatEnemy(int score)
    {
        CurScore += score;
    }

    
}
