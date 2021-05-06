using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraFollow cameraFollow;
    public Transform playertransform;
    public Camera Cam;
    public GameObject Boomerang;
    public Text HealthOnUI;
    public Text ScoreOnUI;
    public Text HighScoreOnUI;
    public Tilemap MainTilemap;
    public int Health = 3;

    private float nextActionTime = 0f;
    int highestlocalScore;
    int highscore = 0;
    float period = 1f;
    

    private void Start()
    {
        cameraFollow.Setup(() => playertransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float y;
        float x;
        float startx;
        if (Time.time > nextActionTime) { 
            nextActionTime = Time.time + period;
            y = playertransform.position.y + Cam.orthographicSize;
            x = Cam.orthographicSize * Cam.aspect;
            startx = Random.Range(playertransform.position.x - x, playertransform.position.x + x);
            Instantiate(Boomerang, new Vector3( startx, 13, 0), Quaternion.identity);
        }

        if(HealthOnUI.text != "HP " + Health)
        {
            HealthOnUI.text = "HP: "+ Health;
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

        if (Health == 0)
        {

            var Boomerangs = GameObject.FindGameObjectsWithTag("Boomerang");
            for (int i = 0; i< Boomerangs.Length; i++)
            {
                Destroy(Boomerangs[i]);
            }
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
            playertransform.position = new Vector2(1,-0.4f);
            Health = 3;
            highestlocalScore = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<ControllerMovement>().resistance = false;
        }
    }
}
