using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ShipRotation : MonoBehaviour
{
    [SerializeField]
    float maxAngleVelocity;


    Quaternion lastRotationKnown;
    /*
    [SerializeField]
    float maxRotationSpeed;*/


    //Make sure you attach a Rigidbody in the Inspector of this GameObject
    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity_E;
    Vector3 m_EulerAngleVelocity_Q;
    // Start is called before the first frame update
    void Start()
    {
        //Set the axis the Rigidbody rotates in (100 in the y axis)
        m_EulerAngleVelocity_Q = new Vector3(0, -maxAngleVelocity, 0);
        m_EulerAngleVelocity_E = new Vector3(0, maxAngleVelocity, 0);

        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Quaternion deltaRotation = new Quaternion();
        if (Input.GetKey(KeyCode.Q))
        {
            deltaRotation = Quaternion.Euler(m_EulerAngleVelocity_Q * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E)){
            deltaRotation = Quaternion.Euler(m_EulerAngleVelocity_E * Time.deltaTime);
        }
        deltaRotation = deltaRotation.normalized;
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) { 
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        }
        processRotation();
    }

    void processRotation() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            float horiz = Input.GetAxisRaw("Horizontal");
            float verti = Input.GetAxisRaw("Vertical");
            Vector3 movement = new Vector3(horiz, 0f, verti);
            lastRotationKnown = Quaternion.LookRotation(movement);   
        }
            transform.rotation = lastRotationKnown;
    }

}
