using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Func<Vector3> GetCameraFollowPositionFunc;
    void Start()
    {
        
        
    }
    public void Setup( Func<Vector3> GetCameraFollowPositionFunc )
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;
        cameraFollowPosition.y -=  0.375f;
        transform.position = cameraFollowPosition;
    }
}

