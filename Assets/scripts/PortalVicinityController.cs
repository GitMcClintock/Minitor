using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalVicinityController : MonoBehaviour {

    //public GameObject portal;
    public Animator portalAnimator;

    private bool playerHasLeft = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Has Entered");
            if(playerHasLeft)
            {
                Debug.Log("Player has re-entered");
                portalAnimator.SetTrigger("close");
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("...");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Has Exited");
            playerHasLeft = true;
            portalAnimator.SetTrigger("close");
        }
    }

    public void OnOpenButtonClicked()
    {
        Debug.Log("OnButtonClicked");
        portalAnimator.SetTrigger("open");
    }

    public void OnCloseButtonClicked()
    {
        Debug.Log("OnButtonClicked");
        portalAnimator.SetTrigger("close");
    }
}
