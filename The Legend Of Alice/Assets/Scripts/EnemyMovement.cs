using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody EnemyRB;
    public Transform PlayerPos;
    public float cageSize = 6;
    public Vector3 min;
    public Vector3 max;

    bool moveRight;
    bool moveLeft;
    public float movementSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        min = new Vector3(-cageSize, -cageSize, -cageSize);
        max = new Vector3(cageSize, cageSize, cageSize);

        if(Random.value > 0.5f)
        {
            moveRight = true;
            moveLeft = false;
        }
        else
        {
            moveRight = false;
            moveLeft = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        
    }
}
