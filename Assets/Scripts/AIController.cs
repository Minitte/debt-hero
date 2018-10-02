using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

    /// <summary>
    /// The NavMeshAgent associated with this gameobject.
    /// </summary>
    //public Transform target;
    //public Vector3 offset = new Vector3(0f, 7.5f, 0f);
    public Light spotlight;
    private NavMeshAgent _agent;
    public float detectionRange;
    public float detectionAngle;
    public bool playerDetected = false;
    private bool follow = false;
    private float timeSinceAggro;


    // Use this for initialization
    void Start () {
        _agent = GetComponent<NavMeshAgent>();

        // Make sure that this gameobject has a NavMeshAgent
        if (_agent == null)
        {
            Debug.Log("Attempted to run AIController script without a NavMeshAgent.");
            Destroy(this);
        }
        follow = false;
        timeSinceAggro = 0;
        spotlight.spotAngle = detectionAngle;
    }
	
	// Update is called once per frame
	void Update () {

        /// <summary>
        /// If player is detected, set aggro timer to 0
        /// If player is not detected, start counting up to 60 sec before losing aggro
        /// </summary>
        if (playerDetected)
        {
            timeSinceAggro = 0;

        }
        else if (timeSinceAggro < 60)
        {

        }
        else if(playerDetected == false)
        {
            timeSinceAggro = timeSinceAggro += Time.deltaTime;
        }

        if (timeSinceAggro >= 60)
        {
            timeSinceAggro = 0;
            playerDetected = false;
            follow = false;
        }

        if(follow && PlayerDistanceFrom() > 1)
        {
            _agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        else
        {
            _agent.destination = transform.position;
        }

        /// <summary>
        /// Calculates the angle and distance from the player and follows if within specified values
        /// </summary>
        float angle = PlayerAngleFrom();
        float distance = PlayerDistanceFrom();
        if (angle <= detectionAngle && distance < detectionRange)
        {
            playerDetected = true;
            follow = true;
        }
        else
        {
            playerDetected = false;
        }

    }

    /// <summary>
    /// Calculates the angle from the player
    /// </summary>
    private float PlayerAngleFrom()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        float detection = Vector3.Angle(forward, playerPos);
        return detection;
    }

    /// <summary>
    /// Calculates the distance from the player
    /// </summary>
    private float PlayerDistanceFrom()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        float playerDistance = Vector3.Distance(transform.position, playerPos);
        return playerDistance;
    }

}
