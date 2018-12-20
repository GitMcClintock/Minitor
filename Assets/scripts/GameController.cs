using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour
{
    public GameObject portal;
    public Animator portalAnimator;

    public GameObject player;
    //public GameObject cursor;
    public Animator openButton;
    public CharacterController playerController;
    public Texture2D mousePointer;

    private MouseLook mouseLook;
    private FirstPersonController rfpc;
    private bool moving = true;
    private bool cursorOn = false;

    // Use this for initialization
    void Start()
    {
        if(playerController != null)
        {
            //player.GetComponent("First")
            //rfpc = playerController.GetComponent<RigidbodyFirstPersonController>();
            rfpc = playerController.GetComponent<FirstPersonController>();
            if (rfpc == null) Debug.Log("No RigidbodyFirstPersonController found");
        }
        //Cursor.lockState = CursorLockMode.Locked;

        //Cursor.SetCursor(mousePointer, new Vector2(1,1), CursorMode.Auto);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(cursorOn)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 2.0f))
                {
                    Debug.Log("You clicked a " + hit.GetType());
                    //if(hit is ClickableBehaviour)
                    //{

                    //}
                    Debug.Log("You selected the " + hit.transform.tag);
                    if (hit.transform.tag == "Button")
                    {
                        //Debug.Log("Clicked button");
                        Debug.Log("openButton.Play(\"buttonAnimation\")");
                        openButton.Play("buttonAnimation");
                        portalAnimator.SetTrigger("open");
                    }
                }
            }
            else
            {
                cursorOn = true;
                //cursor.SetActive(true);
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            if(moving)
            {
                moving = false;
                
            }
            else
            {
                moving = true;
            }
            Debug.Log("moving = " + moving);
            //playerController.enabled = moving;
            //rfpc.mouseLook.SetCursorLock(!moving);
            if(cursorOn)
            {
                cursorOn = false;
                //cursor.SetActive(false);
            }

        }

    }

    public void ExitMinitor()
    {
        //if(MessageBox.Show(""))
        //{

        //}
        Application.Quit();
    }

}
