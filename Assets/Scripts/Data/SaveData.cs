using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

static public class SaveData
{
    public static void Save()
    {
        Data data = new Data();
        data.RenewData();

        string path = Application.persistentDataPath + "/Settings.dat";


        // File Open
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Create);

        formatter.Serialize(file, data);
    }

    public static void Load()
    {
        string path  = Application.persistentDataPath + "/Settings.dat";
        if (!File.Exists(path)) return;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        Data data = (Data)formatter.Deserialize(file);
        file.Close();

        // ¼³Á¤
        ScoreManager.CurLife = data.Life;
        ScoreManager.CurScore = data.Score;
        ScoreManager.CurGem = data.Gem;
        ScoreManager.CurCherry = data.Cherry;

        // Setting
        Settings.canSound = data.canSound;
        Settings.langauge = data.langauge;
    }
}
