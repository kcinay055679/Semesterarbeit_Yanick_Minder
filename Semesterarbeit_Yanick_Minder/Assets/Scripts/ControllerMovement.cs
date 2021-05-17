using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public bool crouch = false;
    public bool resistance = false;
    
    public void Start()
    {
        Application.targetFrameRate = 60;
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
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        }

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            controller.Move(horizontalmovementphone * Time.fixedDeltaTime, crouch, jump);
        }
        jump = false;
    }
    public void Jumpvoid()
    {
        jump = true;
        animator.SetBool("isJump", true);

    }
    public void OnLanding()
    {
        animator.SetBool("isJump", false);
    }
    public void onCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }
}