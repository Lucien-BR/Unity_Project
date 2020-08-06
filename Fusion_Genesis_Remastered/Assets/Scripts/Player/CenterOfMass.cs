using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    public Vector3 CenterOfMass2;
    public bool Awake;
    protected Rigidbody r;


    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        r.centerOfMass = CenterOfMass2;
        r.WakeUp();
        Awake = !r.IsSleeping();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * CenterOfMass2, 1f);

    }

}
