using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static bool isgameStart=false;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.ShowPanel<StartPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
