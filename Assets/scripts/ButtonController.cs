using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.queriesHitTriggers = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
    }
    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
    }
}
