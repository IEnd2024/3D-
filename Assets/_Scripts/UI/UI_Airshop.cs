using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class UI_Airshop : MonoBehaviour
{
    public Texture2D Airshopcur;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(Airshopcur, Vector2.zero, CursorMode.Auto);        

    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) { Application.Quit(); }
    }

}
