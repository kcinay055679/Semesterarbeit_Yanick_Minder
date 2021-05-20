using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    
    public int[] RankScores = new int [Highscore.highscores.Count];
    public string[] RankNames  = new string [Highscore.highscores.Count];

    public PlayerData()
    {
        for (int i=0; i< Highscore.highscores.Count; i++)
        {
            RankNames[i] = Highscore.highscores[i].Name;
            RankScores[i] = Highscore.highscores[i].Score;
        }
    }
}
