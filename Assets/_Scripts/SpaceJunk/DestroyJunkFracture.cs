using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyJunkFracture : MonoBehaviour
{
    public float deathTime = 10f;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        Destroy(this.gameObject, deathTime);
    }
}
