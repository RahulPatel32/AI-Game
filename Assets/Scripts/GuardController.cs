using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour {

    public AIwaypoint[] path;
    public float reachDist;
    private int currentWaypoint;
    public bool pause;
    public float speed;

	void Start() {
		reachDist = 0;
		currentWaypoint = 0;
	}

    void Update()
    {

        Patrol();
    }

    void Patrol()
    {
        float distance = Vector3.Distance(path[currentWaypoint].transform.position, transform.position);

        if(!pause)
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
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

    void DetectPlayer()
    {

    }

    IEnumerator GuardPause()
    {
        pause = true;
        yield return new WaitForSeconds(path[currentWaypoint].pauseTime);
        pause = false;
    }

}
