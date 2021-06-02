using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Collections.Generic;

public static class SaveSystem 
{

    public static void SaveGame()
    {
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.lol";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadGame()
    {

        

        string path = Application.persistentDataPath + "/save.lol";

        if (!File.Exists(path))
        {
            GameHandler.Playernames = new List<string>();
            Highscore.highscores.Add(new highscore(0, "- - - -"));
            Highscore.highscores.Add(new highscore(0, "- - - -"));
            Highscore.highscores.Add(new highscore(0, "- - - -"));
            Highscore.highscores.Add(new highscore(0, "- - - -"));
            Highscore.highscores.Add(new highscore(0, "- - - -"));
            
            SaveGame();
        }

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream streamopen = new FileStream(path, FileMode.Open);
            PlayerData correctdata = formatter.Deserialize(streamopen) as PlayerData;





            Highscore.highscores.Clear();

            List<highscore> localhighscores = new List<highscore>();
            for (int i = 0; i < correctdata.RankNames.Length; i++)
            {
                localhighscores.Add(new highscore(correctdata.RankScores[i], correctdata.RankNames[i]));
                
            }
            List<highscore> SortedHighscores = localhighscores.OrderBy(highscores => highscores.Score).ToList();
            SortedHighscores.Reverse();

            Highscore.highscores = SortedHighscores;

            GameHandler.Playernames = new List<string>();
            for (int i = 0; i < correctdata.Playernames.Length; i++)
            {
                GameHandler.Playernames.Add(correctdata.Playernames[i]);
            }

            streamopen.Close();
            return correctdata;
        }
        else
        {
            Debug.LogError("Save file not  found in " + path);
            return null;
        }
    }
}
