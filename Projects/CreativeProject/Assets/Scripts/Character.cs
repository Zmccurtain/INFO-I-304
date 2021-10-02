using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Cell currentCell;
    public Cell targetCell;
    public Rigidbody rb;

    public int movementRange = 5;
    public int attackRange = 2;

    public float maxHP;
    public float HP;

    public float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentCell = targetCell;
    }
        // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, currentCell.center) < .1f)
        {

            if (targetCell.path.Count == 0) currentCell = targetCell;
            else
            {
                currentCell = targetCell.path[0];
                targetCell.path.RemoveAt(0);
            }
        }
        var velocity = currentCell.center - transform.position;
        velocity.y = 0;
        if (Mathf.Abs(velocity.x) < .1f && Mathf.Abs(velocity.z) < .1f)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentCell.center.x, currentCell.center.y,currentCell.center.z), .1f);
        }
        else 
        {
            rb.velocity = velocity.normalized * 20;
        }
    }

    public void takeDamage(float damage)
    {
        Debug.Log(string.Format("Took {0} damage", damage));
        HP -= damage;
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

}
