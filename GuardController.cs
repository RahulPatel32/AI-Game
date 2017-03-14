using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour {

    public AIwaypoint[] path;
    public float reachDist;
    private int currentWaypoint;
    public bool pause;
    public float speed;

    public bool spotted = false;

    public Transform sightStart, sightEnd;

   
    private Quaternion lookRotation;
    private Vector3 direction;

	void Start() {
		reachDist = 0;
		currentWaypoint = 0;
	}

    void Update()
    {

        Patrol();
        Raycasting();
    }

    void Patrol()
    {
        float distance = Vector3.Distance(path[currentWaypoint].transform.position, transform.position);

        if(!pause)
        {
            transform.Rotate(0, 0, 1);

            transform.position = Vector3.MoveTowards(transform.position, path[currentWaypoint].transform.position, speed * Time.deltaTime);

            if(distance == reachDist)
            {
                StartCoroutine(GuardPause());
                currentWaypoint++;
            }
            if(currentWaypoint ==path.Length)
            {
                currentWaypoint = 0;
            }
        }
    }

    void Raycasting()
    {
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 <<LayerMask.NameToLayer("Player"));
    }

    void Behaviours()
    {

    }

    IEnumerator GuardPause()
    {
        pause = true;
        yield return new WaitForSeconds(path[currentWaypoint].pauseTime);
        pause = false;
    }

}
