using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameUI : MonoBehaviour
{

    public Button mineMode;
    public Button scanMode;

    void Start()
    {
        
    }

    public void Exit()
    {
        Debug.Log("Quitting");
        EditorApplication.isPlaying = false;
        //Application.Quit();
    }

}
