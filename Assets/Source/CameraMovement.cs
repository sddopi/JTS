﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
    // Damp movement
    public float dampTime = 0.3f;
    // Target actor of Camera
    protected Transform targetActor;
    //
    protected Bounds cameraBounds;
    // Current Velocity
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        // Get Player
        targetActor = GameObject.FindGameObjectWithTag("Player").transform;
        //
        GameObject background = GameObject.FindGameObjectWithTag("Background");
        //
        if (background != null)
        {
            cameraBounds = background.renderer.bounds;
        }
    }

	void FixedUpdate () 
    {
        // Valid
        if (targetActor != null && cameraBounds != null) 
        {
            // Get Half Screen Location of target
            Vector3 cameraPoint = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            // Get Viewport Location of target
            Vector3 targetPoint = camera.WorldToViewportPoint(targetActor.position);
            // Vector Distance
            Vector3 distance = targetActor.position - cameraPoint;
            // Calculate Camera Destination
            Vector3 destination = transform.position + distance;
            //
            destination.x = Mathf.Clamp(destination.x, cameraBounds.min.x - cameraPoint.x, cameraBounds.max.x - cameraPoint.x);
            destination.y = transform.position.y;
            destination.z = transform.position.z;
            //
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            
        }
	}
}
