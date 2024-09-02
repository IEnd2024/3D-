using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laserbullets : MonoBehaviour
{
    // Start is called before the first frame update
    float fireSpeed=1f;
    private void OnEnable()
    {
        fireSpeed = 5000f;
        this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*fireSpeed);
        GameObject.Destroy(this.gameObject, 4f);
    }
}
