using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshopCotroller :MonoBehaviour
{
    public float rollspeed = 1f;
    public float movespeed = 1f;
    public float mouseXspeed = 1f;
    public float mouseYspeed = 1f;
    public float fasterMovespeed = 1f;

    public AudioSource stay;
    public AudioSource startFly;
    public AudioSource flying;
    public AudioSource stopFly;

    private Rigidbody rigidbody;

    public  void MoveAndRotate()
    {

        float move = Input.GetAxis("Vertical");
        float roll = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (!Mathf.Approximately(0f, move))
        {
            stay.Pause();
            if (!flying.isPlaying)
            {
                startFly.Play();
            }
            if (startFly.isPlaying&&!flying.isPlaying)
            {
                flying.Play();
            }
            

            if (Input.GetKey(KeyCode.LeftShift))
            {
                rigidbody.AddRelativeForce(move * fasterMovespeed * Vector3.forward * Time.fixedDeltaTime);
                
            }
            else
            {
                rigidbody.AddRelativeForce(move * movespeed * Vector3.forward * Time.fixedDeltaTime);
            } 
        }
        else
        {
            bool isflying = false;
            if (flying.isPlaying) { isflying = true; } else { isflying = false; }
            flying.Stop();
            if (isflying) { stopFly.Play(); stay.Play(); }

        }
        if (!Mathf.Approximately(0f, roll) || !Mathf.Approximately(0f, mouseY) || !Mathf.Approximately(0f, mouseX))
        {
            rigidbody.AddRelativeTorque(new Vector3(-mouseY * mouseYspeed * Time.fixedDeltaTime,
            mouseX * mouseXspeed * Time.fixedDeltaTime, -roll * rollspeed * Time.fixedDeltaTime));
        }
        
        
    }
    void Start()
    {
        movespeed = 4000f;
        fasterMovespeed = 10000f;
        rollspeed = 2000f;
        mouseXspeed = 3000f;
        mouseYspeed = 3000f;

        rigidbody = this.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        MoveAndRotate();
        
    }
}
