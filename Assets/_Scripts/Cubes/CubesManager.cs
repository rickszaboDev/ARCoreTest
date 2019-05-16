using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour {

	private int[][] cubesOccupation = {
		new int[] { // Level 0
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0
		},
		new int[] { // Level 1
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0
		},
		new int[] { // Level 2
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0
		},
		new int[] { // Level 3
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0,
			0, 0, 0, 0
		},
	};
	private GameObject cubeSelected;
	private List<GameObject> cubeList = new List<GameObject>();
	private bool hasSetCubes = false;
	public static bool isCubesInteractable = true;
	public void CubeStart(int[][] cubesOccupation, List<GameObject> cubeList){
		this.cubeList = cubeList;
		this.cubesOccupation = cubesOccupation;
		this.hasSetCubes = true;
	}
	public void SendCubeToNewPos(Direction.Cube direction, GameObject targetCube)
	{
		if(!this.hasSetCubes || !CubesManager.isCubesInteractable) 
		{
			Debug.Log("CubesManager is blocking the cubes.");
			return;
		}

		this.cubeSelected = targetCube;
		CubeController targetController = targetCube.GetComponent<CubeController>();
		Dictionary<string, int> newCubePos = targetController.GetCubeNewPos(cubesOccupation, direction);
		cubesOccupation[newCubePos["newX"]][newCubePos["newY"]] = newCubePos["cubeID"];
		cubesOccupation[newCubePos["x"]][newCubePos["y"]] = 0;
	}
}