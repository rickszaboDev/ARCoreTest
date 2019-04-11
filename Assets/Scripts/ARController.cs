using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleARCore;

public class ARController : MonoBehaviour {

    private List<TrackedPlane> m_NewTrackedPlanes = new List<TrackedPlane>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Check ARCore
        if(Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        //The following function will fill m_newTrackedPlanes with the planes that ARCore detected in the current frame
        Session.GetTrackables<TrackedPlane>(m_NewTrackedPlanes, TrackableQueryFilter.New);
	}
}
