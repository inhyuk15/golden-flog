using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

static public class SaveData
{
    public static void Save()
    {
        SettingData data = new SettingData();
        data.RenewData();

        string path = Application.persistentDataPath + "/Settings.dat";

        // File Open        
        using (FileStream file = File.Open(path, FileMode.Create))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(file, data);
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }

            
    }

    public static void Load()
    {
        string path  = Application.persistentDataPath + "/Settings.dat";
        if (!File.Exists(path))
        {
            Debug.Log("there is no save file");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream file = File.Open(path, FileMode.Open))
        {
            try
            {
                file.Position = 0;
                SettingData data = (SettingData)formatter.Deserialize(file);

                // ¼³Á¤
                ScoreManager.CurLife = data.Life;
                ScoreManager.CurScore = data.Score;
                ScoreManager.CurGem = data.Gem;
                ScoreManager.CurCherry = data.Cherry;

                // Setting
                Settings.canSound = data.canSound;
                Settings.langauge = data.langauge;
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }

        
        
  
        
    }
}
