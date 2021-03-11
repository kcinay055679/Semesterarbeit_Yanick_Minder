using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class NormMovement : MonoBehaviour
{

    public float speed = 1;
    public float sprungkraft = 1;
    public Joystick joystick;
    public Button Jump;

    float horizontalmovementpc;
    float horizontalmovementphone;
    private Rigidbody2D rigid;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Button btn = Jump.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        Application.targetFrameRate = 60;
    }
    
    // Update is called once per frame
    void Update()
    {

        
        horizontalmovementpc = Input.GetAxisRaw("Horizontal");
        horizontalmovementphone = joystick.Horizontal;

        //rigid.velocity = new Vector3(horizontalmovementpc * speed, rigid.velocity.y) * Time.deltaTime ;

        //rigid.velocity = new Vector3(horizontalmovementphone * speed, rigid.velocity.y) * Time.deltaTime;

        transform.position += new Vector3(horizontalmovementpc, 0, 0) * speed * Time.deltaTime;
        transform.position += new Vector3(horizontalmovementphone, 0, 0) * speed * Time.deltaTime;


        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigid.velocity.y) < 0.001f)
        {
            rigid.AddForce(new Vector2(0, sprungkraft), ForceMode2D.Impulse);
        }
    }
    public void TaskOnClick() {
        if (Mathf.Abs(rigid.velocity.y) < 0.001f)
        {
            rigid.AddForce(new Vector2(0, sprungkraft), ForceMode2D.Impulse);
        }
    }

}

    






