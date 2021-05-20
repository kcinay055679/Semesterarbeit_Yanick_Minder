using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.IO;

public class Highscore : MonoBehaviour
{
    public static List<highscore> highscores = new List<highscore>();
    public GameObject Highscorepreset;
    public TMP_ColorGradient Gold;
    public TMP_ColorGradient Silver;
    public TMP_ColorGradient Bronze;
    private int maxranks=5;
    // Start is called before the first frame update
    void Start()
    {
        PlayerData data = SaveSystem.LoadGame();

        
        
        List<highscore> SortedHighscores = highscores.OrderBy(highscores => highscores.Score).ToList();
        SortedHighscores.Reverse();

        if (highscores.Count < maxranks)
        {
            maxranks = highscores.Count;
        }

        for (int i = 0; i < maxranks; i++)
        {
            var NewRank = Instantiate(Highscorepreset, new Vector3(0, 190 - (i * 120), 0), Quaternion.identity);
            NewRank.transform.SetParent(GameObject.FindGameObjectWithTag("HighscoreTable").transform, false);

            TextMeshProUGUI Position = NewRank.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Position.text = i+1+".";

            TextMeshProUGUI Score = NewRank.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            Score.text = SortedHighscores[i].Score + "";

            TextMeshProUGUI Name = NewRank.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            Name.text = SortedHighscores[i].Name + "";

            if (i==0)
            {
                Position.colorGradientPreset = Gold;
                Score.colorGradientPreset = Gold;
                Name.colorGradientPreset = Gold;

            }else if (i==1)
            {
                Position.colorGradientPreset = Silver;
                Score.colorGradientPreset = Silver;
                Name.colorGradientPreset = Silver;
            }
            else if (i==2)
            {
                Position.colorGradientPreset = Bronze;
                Score.colorGradientPreset = Bronze;
                Name.colorGradientPreset = Bronze;
            }
        }
    }
}
public class highscore
{
    public int Score { get; set; }
    public string Name { get; set; }

    public highscore( int Score, string Name )
    {
        this.Score = Score;
        this.Name = Name;
    }
}
