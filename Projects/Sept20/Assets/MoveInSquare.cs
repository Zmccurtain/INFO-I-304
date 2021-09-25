using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInSquare : MonoBehaviour
{
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer % 10 > 7.5)
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        else if (timer % 10 > 5)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }

        else if (timer % 10 > 2.5)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
    }
}
