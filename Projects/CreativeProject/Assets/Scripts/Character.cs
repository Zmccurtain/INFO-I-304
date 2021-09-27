using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Cell currentCell;
    public int movement = 3;
    public Vector3 loc;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        loc = transform.position;
    }
        // Update is called once per frame
    void Update()
    {
        var velocity = loc - transform.position;
        if (velocity.normalized.y < -.6f)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(loc.x, transform.position.y, loc.z), .1f);
        }
        else 
        {
            rb.velocity = velocity.normalized * 20;
        }

        
    }
}
