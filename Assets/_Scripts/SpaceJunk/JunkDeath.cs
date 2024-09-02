using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject junkfactrue;
    public Detonator explosion;

    public void TakeDamage(Vector3 hitpoint)
    {
        Explosion(hitpoint);
    }
    private void Explosion(Vector3 hitpoint)
    {
        if (junkfactrue != null)
        {
            Instantiate(junkfactrue, this.transform.position, Quaternion.identity);
        }
        if (explosion != null)
        {
            Instantiate(explosion, this.transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hit = collision.GetContact(0).point;
        if (collision.gameObject.tag == "weapon")
        {
            TakeDamage(hit);
        }
        
    }
}
