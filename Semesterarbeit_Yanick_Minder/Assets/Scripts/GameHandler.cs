using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraFollow cameraFollow;
    public Transform playertransform;
    public Camera Cam;
    public GameObject Boomerang;

    private float nextActionTime = 0f;
    float period = 3;
    
    private void Start()
    {
        //Boomerang = GameObject.FindGameObjectWithTag("Boomerang");
        cameraFollow.Setup(() => playertransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float y;
        float x;
        float startx;
        float starty;



        if (Time.time > nextActionTime) { 
            nextActionTime = Time.time + period;
            y = playertransform.position.y + Cam.orthographicSize;
            x = Cam.orthographicSize * Cam.aspect;

            

            startx = Random.Range(playertransform.position.x - x, playertransform.position.x + x);
            //starty = Random.Range();
            //Instantiate(Boomerang, new Vector3(x, y, 0), Quaternion.identity);
            Instantiate(Boomerang, new Vector3( startx, y, 0), Quaternion.identity);
            //Boomerang.transform.position = new Vector2(playertransform.transform.position.x + 5 * Time.deltaTime, playertransform.transform.position.y);
        }
    }
}
