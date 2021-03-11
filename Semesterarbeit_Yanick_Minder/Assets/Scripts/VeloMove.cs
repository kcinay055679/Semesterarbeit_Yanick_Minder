using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeloMove : MonoBehaviour
{

    private Rigidbody2D rigid;
    public float speed;
    public float sprungkraft = 1;
    float Horizontalmovement=0;
    public Vector2 movement;
    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Horizontalmovement = Input.GetAxisRaw("Horizontal");

        movement = new Vector2(Input.GetAxisRaw("Horizontal"), rigid.velocity.y);

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigid.velocity.y) < 0.001f)
        {
            rigid.AddForce(new Vector2(0, sprungkraft), ForceMode2D.Impulse);
        }
        //rigid.velocity = new Vector2(Horizontalmovement * 100 * speed , rigid.velocity.y).normalized;
    }

    void FixedUpdate()
    {
        moveCharacter(movement);

        //rigid.velocity = new Vector2(Horizontalmovement * 100 * speed, rigid.velocity.y).normalized;
        //rigid.AddForce(movement * speed);
    }
    void moveCharacter(Vector2 dirction)
    {
        rigid.MovePosition((Vector2)transform.position + (dirction*speed* Time.deltaTime));
    }


}
