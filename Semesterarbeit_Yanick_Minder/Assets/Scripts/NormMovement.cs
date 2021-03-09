using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormMovement : MonoBehaviour
{

    public float speed = 1;
    public float sprungkraft = 1;
    public Joystick joystick;

    float verticalmovement;
    private Rigidbody2D rigid;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        verticalmovement = Input.GetAxis("Horizontal");
        verticalmovement = joystick.Horizontal;
        transform.position += new Vector3(verticalmovement, 0, 0) * speed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigid.velocity.y) < 0.001f)
        {
            rigid.AddForce(new Vector2(0, sprungkraft), ForceMode2D.Impulse);
        }
    }
}


