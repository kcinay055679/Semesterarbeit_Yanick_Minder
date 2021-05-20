using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;
using System.IO;


public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraFollow cameraFollow;
    public Transform playertransform;
    public Camera Cam;
    public GameObject Boomerang;
    public TextMeshProUGUI ScoreOnUI;
    public TextMeshProUGUI HighScoreOnUI;
    public Tilemap MainTilemap;
    public GameObject JumpButton;
    public GameObject CrouchButton;
    public GameObject Movement;
    public GameObject HeartOnUI;
    public int Health = 3;
    private int LastHealth;

    private float nextActionTime = 0f;
    int highestlocalScore;
    int highscore = 0;
    float period = 0.65f;
    PlayerData data;

    private void Start()
    {
        data = SaveSystem.LoadGame();

        //If Smartphone set buttons visible
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            JumpButton.SetActive(true);
            Movement.SetActive(true);
            CrouchButton.SetActive(true);
        }
        cameraFollow.Setup(() => playertransform.position);



        highscore = Highscore.highscores[Highscore.highscores.Count-1].Score;
        
        //Create hearts

        for (int j = 0; j < Health; j++)
        {
            var new_Heart = Instantiate(HeartOnUI, new Vector3(-100-(j*130), -100, 0), Quaternion.identity);
            new_Heart.transform.SetParent(GameObject.FindGameObjectWithTag("CanvesUI").transform, false);
        }
        LastHealth = Health;
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (Time.time > nextActionTime) {
            float x;
            float startx;

            nextActionTime = Time.time + period;
            x = Cam.orthographicSize * Cam.aspect;
            startx = Random.Range(playertransform.position.x - x, playertransform.position.x + x);
            Instantiate(Boomerang, new Vector3( startx, 13, 0), Quaternion.identity);
        }

        

        if (ScoreOnUI.text != "Score: " + (int) playertransform.position.x && (int) playertransform.position.x > highestlocalScore)
        {
            highestlocalScore = (int) playertransform.position.x;
            ScoreOnUI.text = "Score: " + highestlocalScore;
            if (highscore< highestlocalScore)
            {
                highscore = highestlocalScore;
            }
        }

        if (HighScoreOnUI.text != "Highscore: " + highscore)
        {
            HighScoreOnUI.text = "Highscore: " + highestlocalScore;
            highscore = highestlocalScore;
        }

        if (LastHealth != Health)
        {
            var Hearts = GameObject.FindGameObjectsWithTag("Heart");
            for (int i = 0; i < Hearts.Length; i++)
            {
                int lastelement = Hearts.Length - 1;
                Destroy(Hearts[lastelement]);
                LastHealth = Health;
            }
        }

        

        if (Health == 0)
        {   
            //Delete existing boomerangs
            var Boomerangs = GameObject.FindGameObjectsWithTag("Boomerang");
            for (int i = 0; i< Boomerangs.Length; i++)
            {
                Destroy(Boomerangs[i]);
            }
            //Reset map
            for (int Srcposx = 19; Srcposx < MainTilemap.cellBounds.max.x; Srcposx++)
            {
                for (int Srcposy = MainTilemap.cellBounds.min.y; Srcposy < MainTilemap.cellBounds.max.y; Srcposy++)
                {
                    Vector3Int SrcPos = new Vector3Int(Srcposx, Srcposy, 0);
                    MainTilemap.SetTile(SrcPos, null);
                }
            }
            GetComponent<GenerateMap>().partcounter = 1;
            MainTilemap.CompressBounds();

            string Playername;
            GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance = false;

            
            int lastimportantelement;
            if (data.RankScores.Length > 5)
            {
                lastimportantelement = 4;
            }
            else
            {
                lastimportantelement = data.RankScores.Length - 1;
            }
            
            if (data.RankScores.Length > 0 || highestlocalScore > data.RankScores[lastimportantelement])
            {   
                //Spieler nach Namen fragen
                Playername = "Yanick";
                Highscore.highscores.Add(new highscore(highestlocalScore, Playername));
            }
            
            //Reset the player variables
            playertransform.position = new Vector2(1,-0.4f);
            Health = 3;
            highestlocalScore = 0;
            

            //Create new hearts
            for (int j = 0; j < Health; j++)
            {
                var new_Heart = Instantiate(HeartOnUI, new Vector3(-100 - (j * 130), -100, 0), Quaternion.identity);
                new_Heart.transform.SetParent(GameObject.FindGameObjectWithTag("CanvesUI").transform, false);
            }
            LastHealth = Health;

            SaveSystem.SaveGame();
        }
    }
}
