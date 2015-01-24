using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

public class AssignScript : ScriptableWizard
{

    public MonoBehaviour theBehaviour;
    String strHelp = "Select Game Objects";
    GameObject[] gos;

    void OnWizardUpdate()
    {
        helpString = strHelp;
        //isValid = (theBehaviour != null);
    }

    void OnWizardCreate()
    {
        gos = Selection.gameObjects;
        foreach (GameObject go in gos)
        {
            go.AddComponent<clusterScript>();
        }
    }

    [MenuItem("Custom/Assign Script", false, 4)]
    static void assignScript()
    {
        ScriptableWizard.DisplayWizard("Assign Script", typeof(AssignScript), "Assign");
    }
}