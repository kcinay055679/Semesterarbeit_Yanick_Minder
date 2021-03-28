﻿using System.Collections;
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
    private Rigidbody2D rigid;
    
    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
        Button btn = Jump.GetComponent<Button>();
        btn.onClick.AddListener(Jumpvoid);
    }

    // Update is called once per frame
    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        horizontalmovementphone = joystick.Horizontal * speed;


        //Animation control
        if (horizontalMove < 0 || horizontalMove > 0) 
        { 
            animator.SetFloat("speed", Mathf.Abs(horizontalMove));  
        }
        
        if (horizontalmovementphone < 0 || horizontalmovementphone > 0)
        {
            animator.SetFloat("speed", Mathf.Abs(horizontalmovementphone));
        }

        if (horizontalMove == 0&& horizontalmovementphone == 0)
        {
            animator.SetFloat("speed",0);
        }
        //Animation control end
        if (Mathf.Abs(rigid.velocity.y)< 0.001f)
        {
            //animator.SetBool("isJump", false);

        }
        
        
        
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJump", true);
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
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        controller.Move(horizontalmovementphone * Time.fixedDeltaTime, false, false);
        jump = false;
    }
    void Jumpvoid()
    {
        jump = true;
        animator.SetBool("isJump", true);

    }
    public void OnLanding()
    {
        animator.SetBool("isJump", false);


    }
}
