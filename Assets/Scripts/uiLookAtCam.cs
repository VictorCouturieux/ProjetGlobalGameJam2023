using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiLookAtCam : MonoBehaviour
{
    public Camera myCam;
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (myCam.orthographic && transform.rotation != myCam.transform.rotation) 
        {
            transform.rotation = myCam.transform.rotation;
        }
        else if (!myCam.orthographic)
        {
            transform.LookAt(myCam.transform);
        }
    }
}
