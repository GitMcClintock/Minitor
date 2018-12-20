using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrreryPivotCOntroller : MonoBehaviour {

    private float angle;

    void Start()
    {
        angle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        angle = 5f * Time.deltaTime;        // degrees per second
        transform.Rotate(new Vector3(0, 0, 1), angle, Space.Self);
    }
}
