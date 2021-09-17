using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller: MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float nitroFactor;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Nitro();
    }
    void FixedUpdate()
    {
        Movement();
    }
    void Nitro()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { acceleration *= nitroFactor; maxSpeed *= nitroFactor; }
        if (Input.GetKeyUp(KeyCode.Space)) { acceleration /= nitroFactor; maxSpeed /= nitroFactor; }
    }
    void Movement()
    {
        var v = Input.GetAxisRaw("Vertical");
        var targetV = transform.TransformDirection(transform.forward*maxSpeed * v);
        var targetH = Input.GetAxisRaw("Horizontal");
        rb.AddTorque(transform.up * targetH * transform.InverseTransformDirection(rb.velocity).z);
        Debug.Log(transform.forward);
        //Vector3 torque = Mathf.Clamp(targetH * rb.velocity.z, -acceleration, acceleration) * transform.up;

        Vector3 force = Mathf.Clamp((targetV - rb.velocity).magnitude * v, -acceleration, acceleration) * transform.forward;
        rb.AddForce(force);
        //rb.AddTorque(torque);
    }
}
