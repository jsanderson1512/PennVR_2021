

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class XRF_VRControllerRaycastInteractions : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float laserDistance = 100.0f;
    public GameObject controllerGameObject;
    #endregion

    #region PRIVATE VARIABLES
    private bool dontHighlight;
    private Material[] tempMaterialsHigh;
    private Material[] matsHigh;
    private GameObject tempSelectedObject;
    private GameObject hitObject;
    private bool Clickable;
    #endregion

    #region AWAKE AND START FUNCTIONS
    private void Awake()
    {

     
    }

    void Start()
    {
        SteamVR_TrackedController trackedController = controllerGameObject.GetComponent<SteamVR_TrackedController>();
        trackedController.TriggerClicked += new ClickedEventHandler(TriggerClick);
        trackedController.TriggerUnclicked += new ClickedEventHandler(TriggerUnClick);
        trackedController.PadClicked += new ClickedEventHandler(PadClicked);
        trackedController.MenuButtonClicked += new ClickedEventHandler(MenuClick);
        trackedController.Gripped += new ClickedEventHandler(Gripped);

    }
    #endregion

    private void Update()
    {
        Vector3 origin = controllerGameObject.transform.position;
        Vector3 direction = controllerGameObject.transform.forward;
        Ray ray = new Ray(origin, direction);

        RaycastHit myRayHit;
        if (Physics.Raycast(ray, out myRayHit, laserDistance) && controllerGameObject.activeSelf)
        {
            //i shot out a ray and it hit something
            Debug.Log("I hit something");
            hitObject = myRayHit.transform.gameObject;

            if (!hitObject.GetComponent<Collider>().isTrigger && hitObject.GetComponent<XRF_InteractionController>())
            {
                //i shot out a ray and hit something with an interaction controller
                Debug.Log("I hit something with an interaction controller on it");
                RayHit(hitObject);
                Clickable = true;

            }
            else
            {
                RayMissed();
            }
        }
        else
        {
            RayMissed();
        }
    }



    public void ClickTheButton(GameObject hitObject)
    {
        XRF_InteractionController[] myInteractions = hitObject.GetComponents<XRF_InteractionController>();
        foreach (XRF_InteractionController t in myInteractions)
        {
            t.DoTheThing();
        }
    }

    void RayHit(GameObject touchObject)
    {
        if (tempSelectedObject != touchObject)
        {
            if (tempSelectedObject != null)
            {
                UnHighlightObj(tempSelectedObject);
            }
        }
        tempSelectedObject = touchObject;
        HighlightObj(tempSelectedObject);
    }
    void RayMissed()
    {
        //Debug.Log("ray missed");
        Clickable = false;

        if (tempSelectedObject != null)
        {
            UnHighlightObj(tempSelectedObject);
            tempSelectedObject = null;
        }
    }
    void HighlightObj(GameObject highlightThis)
    {
        MeshRenderer rend = highlightThis.transform.gameObject.GetComponent<MeshRenderer>();
        if (rend != null)
        {
            if (!dontHighlight && highlightThis.GetComponent<XRF_InteractionController>().isSelected == false)
            {
                tempMaterialsHigh = highlightThis.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials;
                matsHigh = new Material[tempMaterialsHigh.Length];

                Material highlightMaterial = highlightThis.GetComponent<XRF_InteractionController>().HighlightMaterial;

                for (int i = 0; i < tempMaterialsHigh.Length; i++)
                {
                    matsHigh[i] = highlightMaterial;
                }
                highlightThis.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials = matsHigh;
                dontHighlight = true;
                highlightThis.GetComponent<XRF_InteractionController>().isSelected = true;
            }
        }
    }
    void UnHighlightObj(GameObject unHighlightThis)
    {
        MeshRenderer rend = unHighlightThis.GetComponent<MeshRenderer>();
        if (rend != null && unHighlightThis.GetComponent<XRF_InteractionController>().isSelected == true)
        {
            unHighlightThis.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials = tempMaterialsHigh;
            dontHighlight = false;
            unHighlightThis.GetComponent<XRF_InteractionController>().isSelected = false;
        }
    }







    void TriggerClick(object sender, ClickedEventArgs e)
    {
        Debug.Log("hey i clicked the trigger button");

        if (Clickable)
        {
            ClickTheButton(hitObject);
        }
    }

    void TriggerUnClick(object sender, ClickedEventArgs e)
    {
        Debug.Log("hey i UN clicked the trigger button");

    }

    void PadClicked(object sender, ClickedEventArgs e)
    {
        Debug.Log("hey i clicked the pad button");
        float PadLimitHigh = 0.7f;
        float PadLimitLow = 0.3f;
        if (e.padX < -(PadLimitHigh) && e.padY < PadLimitLow)
        { //Left


        }
        else if (e.padX > PadLimitHigh && e.padY < PadLimitLow)
        { //Right


        }
    }

    void Gripped(object sender, ClickedEventArgs e)
    {
        Debug.Log("hey i clicked the grip button");

    }

    void MenuClick(object sender, ClickedEventArgs e)
    {
        Debug.Log("hey i clicked the menu button");

    }
}