using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float camStick = 10;
    [SerializeField] private float rotSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, 
            target.transform.position, 
            camStick*Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, rotSpeed * Time.deltaTime);
    }
}
