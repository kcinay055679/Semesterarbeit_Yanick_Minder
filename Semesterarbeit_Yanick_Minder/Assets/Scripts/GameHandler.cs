using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraFollow cameraFollow;
    public Transform playertransform;
    private void Start()
    {
        cameraFollow.Setup(() => playertransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
