using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform pointL;
    [SerializeField]
    private Transform pointR;

    [SerializeField] 
    private float speed = 2;
    [SerializeField]
    private bool startToRight = true;

    private Vector3 nextPosition;

    void Start()
    {
        nextPosition = startToRight ? pointR.position : pointL.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Boundary"))
        {
            nextPosition = (col.transform == pointL) ? pointR.position : pointL.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

}
