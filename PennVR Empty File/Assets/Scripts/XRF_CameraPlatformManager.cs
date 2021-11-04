using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class XRF_CameraPlatformManager : MonoBehaviour
{
    [Header ("Flythrough Camera Stuff")] 
    public GameObject FlythroughCameraParent;

    [Header("VR Stuff")]
    public GameObject VRCameraParent;
    public GameObject VRCameraEye;


    private void Awake()
    {

#if UNITY_STANDALONE_WIN
        Debug.Log("Stand Alone Windows");
        VRCameraParent.SetActive(true);

        if (VRCameraEye.GetComponent<SteamVR_Camera>().enabled)
        {
            Debug.Log("VR Mode");
            FlythroughCameraParent.SetActive(false);
            VRCameraParent.SetActive(true);
        }
        else
        {
            Debug.Log("Flythrough Mode");
            FlythroughCameraParent.SetActive(true);
            VRCameraParent.SetActive(false);
        }

#elif UNITY_WEBGL
        Debug.Log("WebGL");

#elif UNITY_IOS
        Debug.Log("Iphone");

#elif UNITY_ANDROID
        Debug.Log("Android");
        //inside of unity_android we will need to differentiate between oculus and mobile android...

#else
        Debug.Log("Any Other Platform");

#endif
    }
}
