using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController2D controller;
    public Joystick joystick;
    public Button Jump;
    public Animator animator;

    public float speed = 0f;

    float horizontalMove = 0f;
    float horizontalmovementphone = 0f;
    
    bool jump = false;
    bool crouch = false;
    
    public void Start()
    {
        Application.targetFrameRate = 60;
        Button btn = Jump.GetComponent<Button>();
        btn.onClick.AddListener(jumpvoid);
    }

    // Update is called once per frame
    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime;
        horizontalmovementphone = joystick.Horizontal * speed * Time.fixedDeltaTime;

       
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));  
        //animator.SetFloat("speed", Mathf.Abs(horizontalmovementphone));
        
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            crouch = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            crouch = false;
        }
    }
    public void FixedUpdate()
    {
        controller.Move(horizontalMove, crouch, jump);
        controller.Move(horizontalmovementphone, false, false);
        jump = false;
    }
    void jumpvoid()
    {
        jump = true;

    }
}
