﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleARCore;

public class ARController : MonoBehaviour {

    private List<TrackedPlane> m_NewTrackedPlanes = new List<TrackedPlane>();

    public GameObject GridPrefab;
    public GameObject Portal;

    public GameObject ARCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//Check ARCore session status.
        if(Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        //The following function will fill m_newTrackedPlanes with the planes that ARCore detected in the current frame
        Session.GetTrackables<TrackedPlane>(m_NewTrackedPlanes, TrackableQueryFilter.New);

        //Instantiate a Grid for each TrackedPlane in m_NewTrackedPlanes
        for (int i = 0; i < m_NewTrackedPlanes.Count; i++)
        {
            GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;

            // This function will set the position of grid and modify the vertices of the attached mesh.
            grid.GetComponent<GridVisualizer>().Initialize(m_NewTrackedPlanes[i]);
        }

        //Check if user touches the screen.
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        //Let's now check if the user touched any of the tracked planes
        TrackableHit hit;
        if(Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
            // Let's now place the portal on the portal on top of the tracked plane that we touched.

            // Enable the portal.
            Portal.SetActive(true);

            //Create new Anchor.
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            //Set the position of the portal to be the same as the hit position
            Portal.transform.position = hit.Pose.position;
            Portal.transform.rotation = hit.Pose.rotation;

            //We want the portal to face the camera
            Vector3 cameraPosition = ARCamera.transform.position;

            //The portal should only rotate around the Y axis
            cameraPosition.y = hit.Pose.position.y;

            //Rotate the portal to face the camera
            Portal.transform.LookAt(cameraPosition, Portal.transform.up);

            //ARcore will keep understanding the woeld and update the anchors accordingly hence we need to attach our portal to the anchor
            Portal.transform.parent = anchor.transform;

        }
	}
}