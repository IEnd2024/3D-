using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateJunk : MonoBehaviour
{
    public Transform star;
    public List<GameObject> junk;
    public int junkMax=1;
    public float distance = 1f;
    public float radiusMin = 1f;
    public float radiusMax = 1f;
    public float scaleMax = 1f;
    public float yAxisrange = 1f;
    private void OnEnable()
    {
        radiusMin = 3100f;
        radiusMax = 3300f;
        junkMax = 1000;
        scaleMax = 10f;
        yAxisrange = 300f;
        Createjunk();
    }
    // Update is called once per frame
    public void Createjunk()
    {
        for(int i = 0; i < junkMax;++i )
        {
            GameObject obj = Instantiate(junk[Random.Range(0, junk.Count)], star.position, Quaternion.identity);
            obj.GetComponent<Collider>().enabled = false;

            float scale = Random.Range(0.5f, scaleMax);
            obj.transform.localScale = new Vector3(scale, scale, scale);

            obj.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * Random.Range(0f, scaleMax));

            Vector3 max = Random.onUnitSphere * Random.Range(radiusMin, radiusMax);
            obj.transform.position += max;
            obj.transform.position = new Vector3(obj.transform.position.x,
                Random.Range(star.position.y - yAxisrange, star.position.y + yAxisrange),obj.transform.position.z);           
            distance = Vector3.Distance(obj.transform.position, star.position);
            if (distance <= radiusMin-1000f ) 
            { 
                GameObject.Destroy(obj);
                --i;
            }
            else { obj.GetComponent<Collider>().enabled = true; }

     
        }
        
        
    }
}
