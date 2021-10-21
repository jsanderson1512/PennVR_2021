 using UnityEngine;
 using UnityEditor;
using System.Linq;

[CustomEditor(typeof(XRF_InteractionController))] //fix this name after class
public class InteractionControllerEditor : Editor
{
    //!! rename this "InteractionControllerEditor" after class

    private int previousOnCount;
    private int previousOffCount;

    public override void OnInspectorGUI()
    {
        XRF_InteractionController script = (XRF_InteractionController)target;


        //highlight material if raycast is selected!
        if (script.gameObject.GetComponent<Collider>().isTrigger == false)
        {
            script.HighlightMaterial = (Material)EditorGUILayout.ObjectField("Highlight Material", script.HighlightMaterial, typeof(Material), true);

        }



        script.myType = (XRF_InteractionController.InteractionType)EditorGUILayout.EnumPopup("Interaction Type", script.myType);



        if (script.myType == XRF_InteractionController.InteractionType.AnimationController)
        {
            script.ObjectWithAnimation = (GameObject)EditorGUILayout.ObjectField("Object with Animation", script.ObjectWithAnimation, typeof(GameObject),true);
        }
        else if (script.myType == XRF_InteractionController.InteractionType.SceneChangeController)
        {
            script.SceneToLoad = EditorGUILayout.TextField("Scene to Load", script.SceneToLoad);
        }
        else if (script.myType == XRF_InteractionController.InteractionType.OnOffController)
        {
            script.NumberOfThingsToTurnON = EditorGUILayout.IntField("Number of Things to Turn ON", script.NumberOfThingsToTurnON);
            GameObject[] tempOns = script.StartOFFClickON;
            if (previousOnCount != script.NumberOfThingsToTurnON)
            {
                script.StartOFFClickON = new GameObject[script.NumberOfThingsToTurnON];
            }
            for (int i = 0; i < script.NumberOfThingsToTurnON;i++)
            {
                if (tempOns != null)
                {
                    if (tempOns.Length > i)
                    {
                        script.StartOFFClickON[i] = tempOns[i];
                    }
                }
                script.StartOFFClickON[i] = (GameObject)EditorGUILayout.ObjectField("Start OFF Click ON", script.StartOFFClickON[i], typeof(GameObject), true);
            }

            script.NumberOfThingsToTurnOFF = EditorGUILayout.IntField("Number of Things to Turn OFF", script.NumberOfThingsToTurnOFF);
            GameObject[] tempOffs = script.StartONClickOFF;
            if (previousOffCount != script.NumberOfThingsToTurnOFF)
            {
                script.StartONClickOFF = new GameObject[script.NumberOfThingsToTurnOFF];
            }
            for (int i = 0; i < script.NumberOfThingsToTurnOFF; i++)
            {
                if (tempOffs != null)
                {
                    if (tempOffs.Length > i)
                    {
                        script.StartONClickOFF[i] = tempOffs[i];
                    }
                }

                script.StartONClickOFF[i] = (GameObject)EditorGUILayout.ObjectField("Start ON Click OFF", script.StartONClickOFF[i], typeof(GameObject), true);
            }
            previousOnCount = script.NumberOfThingsToTurnON;
            previousOffCount = script.NumberOfThingsToTurnOFF;
        }
    }
}