using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SimpleMovement : MonoBehaviour
{

    [SerializeField]
    float acceleration;

    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float deceleration;

    [SerializeField]
    float rotationSpeed;

    float maxDiagonalSpeed;

    bool diagonal;

    bool A;
    bool W;
    bool S;
    bool D;

    // Start is called before the first frame update
    void Start()
    {
        maxDiagonalSpeed = (float)Math.Sqrt(Math.Pow(maxSpeed, 2) / 2); // pythagoras
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 vec = rb.velocity;

        W = Input.GetKey(KeyCode.W);
        A = Input.GetKey(KeyCode.A);
        S = Input.GetKey(KeyCode.S);
        D = Input.GetKey(KeyCode.D);
        diagonal = (W && A && !S && !D) ^ (W && D && !S && !A) ^ (S && A && !W && !D) ^ (S && D && !W && !A);

        if (vec.z < 0)
        {
            if (vec.z > -0.3f)
                vec.z = 0;
            else
                vec.z = deceleration;
        }
        else if (vec.z > 0) {
            if (vec.z < 0.3f)
                vec.z = 0;
            else
                vec.z = -deceleration;
        }

        if (vec.x < 0)
        {
            if (vec.x > -0.3f)
                vec.x = 0;
            else
                vec.x = deceleration;
        }
        else if (vec.x > 0)
        {
            if (vec.x < 0.3f)
                vec.x = 0;
            else
                vec.x = -deceleration;
        }

        if (A) { vec.z = acceleration;  }
        if (D) { vec.z = -acceleration; }
        if (W) { vec.x = acceleration;  }
        if (S) { vec.x = -acceleration; }
        /*
        if (W || A || S || D) {
            testRotation(vec);
        }*/
        rb.velocity = processMomentum(vec);
    }

    void testRotation(Vector3 posFinal) {
        UnityEngine.Vector3 posActual = GetComponent<Transform>().rotation.eulerAngles;
        bool left = true;
        float angle = (float)Vector3.Angle(posActual, posFinal);
        if (angle <= 179)
            left = false;
        GetComponent<Transform>().Rotate(0f, 2 * angle * Time.deltaTime, 0f);
    }

    Vector3 processMomentum(Vector3 acc)
    {
        Vector3 final = new Vector3(0, 0, 0);

        Vector3 actual = GetComponent<Rigidbody>().velocity;
        bool[] wasd = { actual.x > 0, actual.z > 0, actual.x < 0, actual.z < 0 };

        float maxLocal = 0;
        if (diagonal)
        {
            maxLocal = maxDiagonalSpeed;
        }
        else
        {
            maxLocal = maxSpeed;
        }

        if (actual.x + acc.x > maxLocal)
        {
            final.x = maxLocal;
        }
        else if (actual.x + acc.x < -maxLocal)
        {
            final.x = -maxLocal;
        }
        else
        {
            final.x = actual.x + acc.x;
        }

        if (actual.z + acc.z > maxLocal)
        {
            final.z = maxLocal;
        }
        else if (actual.z + acc.z < -maxLocal)
        {
            final.z = -maxLocal;
        }
        else
        {
            final.z = actual.z + acc.z;
        }

        return final;
    }
}
