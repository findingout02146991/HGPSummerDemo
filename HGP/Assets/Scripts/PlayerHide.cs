﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    private bool hiding = false;
    public bool Hiding
    {
        get
        {
            return hiding;
        }
    }
    private float originalXpos;
    private float originalYpos;
    private float newXpos;
    private float newYpos;
    private bool hideAction = false;
    private bool leavingHiding = false;
    [SerializeField]
    private float hideSpeed = 15;
    private bool colliding;
    private Rigidbody2D rBD2D;
    private BoxCollider2D bC2D;
    private BoxCollider2D collidingWith;

    void Awake()
    {
        rBD2D = GetComponent<Rigidbody2D>();
        bC2D = GetComponent<BoxCollider2D>();
        rBD2D.useFullKinematicContacts = true;
        colliding = false;
    }

    void FixedUpdate()
    {
        float step = hideSpeed * Time.deltaTime;
        if (hiding)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(newXpos, newYpos), step);
            //rBD2D.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(newXpos,newYpos),step));
        }
        else
        {
            if (leavingHiding)
            {
                //transform.position = new Vector2(originalXpos, originalYpos);
                Vector2 originalPosition = new Vector2(originalXpos, originalYpos);
                transform.position = Vector2.MoveTowards(transform.position, originalPosition, step);
                if ((Vector2)transform.position == originalPosition)
                {
                    leavingHiding = false;
                    rBD2D.isKinematic = false;
                }
            }
        }
        //Debug.Log(hiding + " " + leavingHiding);
    }

    void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    hideAction = true;
        //}
        if (colliding)
        {
            if (bC2D.IsTouching(collidingWith))
            {
                if (Input.GetKeyDown("space"))
                {
                    if (collidingWith.gameObject.tag == "Prop")
                    {
                        if (hiding)
                        {
                            hiding = false;
                            leavingHiding = true;
                        }
                        else
                        {
                            hiding = true;
                            newXpos = collidingWith.gameObject.transform.position.x;
                            newYpos = collidingWith.gameObject.transform.position.y;
                            originalXpos = transform.position.x;
                            originalYpos = transform.position.y;
                            rBD2D.isKinematic = true;
                        }
                    }
                    //hideAction = false;
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //if (hideAction)
        //{
        //    if (col.gameObject.tag == "Prop")
        //    {
        //        if (hiding)
        //        {
        //            hiding = false;
        //            leavingHiding = true;
        //        }
        //        else
        //        {
        //            hiding = true;
        //            newXpos = col.gameObject.transform.position.x;
        //            newYpos = col.gameObject.transform.position.y;
        //            originalXpos = transform.position.x;
        //            originalYpos = transform.position.y;
        //            rBD2D.isKinematic = true;
        //        }
        //    }
        //    hideAction = false;
        //}
        //Debug.Log("colliding");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        colliding = true;
        collidingWith = col.gameObject.GetComponent<BoxCollider2D>();
        Debug.Log("enter");
    }

    void OnCollisionExit2D(Collision2D col)
    {
        colliding = false;
        Debug.Log("exit");
    }
}
