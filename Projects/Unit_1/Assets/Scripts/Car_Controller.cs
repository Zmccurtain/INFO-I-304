using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller: MonoBehaviour
{
    public float speed;
    public float maxSpeed;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float target = 0;
        if (Input.GetKey(KeyCode.W))
        {
            target = maxSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(transform.up*-1);
        }
        Vector3 force = Mathf.Clamp(target - rb.velocity.z, -speed, speed) * transform.forward;
        rb.AddForce(force);
    }
}
