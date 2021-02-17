using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float speed = 10;
    float horizontalmovement = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalmovement = Input.GetAxisRaw("Horizontal") * speed;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalmovement * Time.fixedDeltaTime, false, false);
    }
}
