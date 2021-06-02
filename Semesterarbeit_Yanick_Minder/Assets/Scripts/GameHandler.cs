using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;
using System.IO;
using System.Linq;


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
    public GameObject AskName;
    public GameObject Spike;


    public static List<string> Playernames = new List<string>();

    private float BoomerangnextActionTime = 0f;
    float Boomerangperiod = 0.65f;

    private float SpikePeriod = 8.5f;
    private float SpikeNextActionTime = 0f;
    int highestlocalScore;
    int highscore = 0;
    
    PlayerData data;
    public string Playername;

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


        
        highscore = Highscore.highscores[0].Score;
        HighScoreOnUI.text = "Highscore: " + highscore;


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
        
        if (Time.time > BoomerangnextActionTime) {
            float x;
            float startx;

            BoomerangnextActionTime = Time.time + Boomerangperiod;
            x = Cam.orthographicSize * Cam.aspect;
            startx = Random.Range(playertransform.position.x - x, playertransform.position.x + x);
            Instantiate(Boomerang, new Vector3( startx, 13, 0), Quaternion.identity);
        }

        
        if (Time.time > SpikeNextActionTime)
        {
            //Action
            SpikeNextActionTime = Time.time + SpikePeriod;
            float Spikex = playertransform.position.x + Random.Range(19,38);
            float Spikey = Random.Range(3f, -4.5f);
            Instantiate(Spike, new Vector3(Spikex, Spikey, 0), Quaternion.identity);

        }
        


        
        //if (highestlocalScore > highscore )
        if (ScoreOnUI.text != "Score: " + (int) playertransform.position.x && (int) playertransform.position.x > highestlocalScore )
        {
            highestlocalScore = (int) playertransform.position.x;
            ScoreOnUI.text = "Score: " + highestlocalScore;
            if (highscore < highestlocalScore)
            {
                highscore = highestlocalScore;
                HighScoreOnUI.text = "Highscore: " + highestlocalScore;
            }
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
                if (AskName.activeSelf == false)
                {
                    AskName.SetActive(true);
                    AskName.GetComponent<AskName>().startshow();
                }
                
                AskName.transform.GetChild(1).GetComponent<TMP_InputField>().text = Playername;
                Time.timeScale = 0f;
                
            }
            
            //Reset the player variables
            playertransform.position = new Vector2(1,0.55f);
            Health = 3;
            
            

            //Create new hearts
            for (int j = 0; j < Health; j++)
            {
                var new_Heart = Instantiate(HeartOnUI, new Vector3(-100 - (j * 130), -100, 0), Quaternion.identity);
                new_Heart.transform.SetParent(GameObject.FindGameObjectWithTag("CanvesUI").transform, false);
            }
            LastHealth = Health;

            
        }
    }
    
    public void GetName()
    {
        Playername = GameObject.FindGameObjectWithTag("AskInput").GetComponent<TMP_InputField>().text;
        Time.timeScale = 1f;
        Playernames.Add(Playername);
        
        Highscore.highscores.Add(new highscore(highestlocalScore, Playername));
        SaveSystem.SaveGame();
        highestlocalScore = 0;
    }

    public void DestroyOldNamePrefabs()
    {
        GameObject[] OldPrefabs = GameObject.FindGameObjectsWithTag("EnterName");
        for (int i = 0; i < OldPrefabs.Length; i++)
        {
            Destroy(OldPrefabs[i]);
        }
    }
}
