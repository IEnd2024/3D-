using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshopAttack : MonoBehaviour
{
    public Transform rightFirePoint;
    public Transform leftFirePoint;
    public List<GameObject> weapons;
    public AudioSource a;
    // Update is called once per frame
    bool Canfire
    {
        get
        {
            _cooldowm -= Time.deltaTime;
            return _cooldowm <= 0f;
        }
    }
    float _cooldowm;
    public  void Shoot()
    {

        if (Canfire&&Input.GetMouseButton(0))
        {
            Instantiate(weapons[0], rightFirePoint.position, rightFirePoint.rotation);
            Instantiate(weapons[0], leftFirePoint.position, rightFirePoint.rotation);
            _cooldowm = 0.25f;
            a.Play();

        }
        
    }
    void FixedUpdate()
    {
        Shoot();
    }

}
