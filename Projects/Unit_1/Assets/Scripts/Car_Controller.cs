using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller: MonoBehaviour
{
    public float speed;
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
        if (Input.GetKeyDown(KeyCode.Space)) { speed *= nitroFactor; maxSpeed *= nitroFactor; }
        if (Input.GetKeyUp(KeyCode.Space)) { speed /= nitroFactor; maxSpeed /= nitroFactor; }
    }
    void Movement()
    {
        var target = maxSpeed * Input.GetAxisRaw("Vertical");
        var torque = Input.GetAxisRaw("Horizontal");
        rb.AddTorque(transform.up * torque * speed * .4f);

        Vector3 force = Mathf.Clamp(target - rb.velocity.z, -speed, speed) * transform.forward;
        rb.AddForce(force);
    }
}
