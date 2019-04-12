using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleARCore;

public class ARController : MonoBehaviour {

    private List<TrackedPlane> m_NewTrackedPlanes = new List<TrackedPlane>();
    public GameObject GridPrefab;

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

        //Instantiate a Grid for each TrackedPlane in m_NewTrackedPlanes
        for (int i = 0; i < m_NewTrackedPlanes.Count; i++)
        {
            GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;

            // This function will set the position of grid and modify the vertices of the attached mesh.
            grid.GetComponent<GridVisualizer>().Initialize(m_NewTrackedPlanes[i]);
        }
	}
}