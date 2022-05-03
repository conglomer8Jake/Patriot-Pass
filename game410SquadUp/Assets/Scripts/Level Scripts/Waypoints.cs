using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    public Transform currWaypoint;
    // speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;
    public enum gooseState
    {
        increasing, decreasing
    }
    public gooseState gS;
	// Use this for initialization
	private void Start () {
        waypointIndex = 0;
        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
        gS = gooseState.increasing;
	}
	
	// Update is called once per frame
	private void Update () {

        // Move Enemy
        currWaypoint = waypoints[waypointIndex];
        if (gS == gooseState.increasing) MoveRight();
        if (gS == gooseState.decreasing) MoveLeft();
	}

    // Makes enemy move
    public void MoveRight()
    {
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
            if (waypointIndex < 3) waypointIndex += 1;
            else if (waypointIndex == 3) gS = gooseState.decreasing;
            }
        
     }
      public void MoveLeft()
    {
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector3.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
            if (waypointIndex > 0) waypointIndex -= 1;
            else if (waypointIndex == 0) gS = gooseState.increasing;
        }
        
     }
}