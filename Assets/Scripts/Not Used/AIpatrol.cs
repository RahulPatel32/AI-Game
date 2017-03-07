using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIpatrol : MonoBehaviour {

    public Transform[] Waypoints;
    public float Speed;
    public int curWayPoint;
    public bool doPatrol = true;
    public Vector3 Target;
    public Vector3 MoveDirection;
    public Vector3 Velocity;
    private Rigidbody2D rigidBody2d;

    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(curWayPoint < Waypoints.Length)
        {
            Target = Waypoints[curWayPoint].position;
            MoveDirection = Target - transform.position;
            Velocity = rigidBody2d.velocity;

            if(MoveDirection.magnitude < 1)
            {
                curWayPoint++;
            }
            else
            {
                Velocity = MoveDirection.normalized * Speed;
            }
        }
        else
        {
            if(doPatrol)
            {
                curWayPoint = 0;
            }
            else
            {
                Velocity = Vector2.zero;
            }
        }

        rigidBody2d.velocity = Velocity;
        transform.LookAt(Target);
    }
}
