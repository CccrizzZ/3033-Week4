using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager Instance {get; private set;}

    public bool CursorActive{get; private set;} = true;



    private void Awake()
    {
        // make sure there is only one instance
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

    }


    // toggle cursor
    private void EnableCursor(bool enable)
    {
        if (enable)
        {
            
            CursorActive = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;    
        }
        else
        {
            CursorActive = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }



    private void OnEnable()
    {
        AppEvent.CursorEnabled += EnableCursor;
    }


    private void OnDisable()
    {
        AppEvent.CursorEnabled -= EnableCursor;        
    }

}
