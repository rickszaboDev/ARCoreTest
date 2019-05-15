using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStartSet : MonoBehaviour {

	int[][] cubesOccupation = {
			new int[] { // Level 0
				0, 0, 2, 0,
				0, 2, 2, 2,
				0, 0, 2, 1,
				0, 0, 1, 1
			},
			new int[] { // Level 1
				0, 0, 2, 0,
				0, 2, 0, 2,
				0, 0, 2, 0,
				0, 0, 0, 0
			},
			new int[] { // Level 2
				0, 0, 1, 0,
				0, 0, 0, 2,
				0, 0, 0, 0,
				0, 0, 0, 0
			},
			new int[] { // Level 3
				0, 0, 0, 0,
				0, -1, 0, 1,
				0, 0, 0, 0,
				0, 0, 0, 0
			}
		};
	public GameObject[] cubeElement;

	public CubesManager manager;

	void Start () {
		for(int i = 0; i < cubesOccupation.Length; i++){
			for(int j = 0; j < cubesOccupation[i].Length; j++){
				if(cubesOccupation[i][j] > 0){
					GameObject newCube = Instantiate(cubeElement[cubesOccupation[i][j]]) as GameObject;
					CubeController newCubeController = newCube.GetComponent<CubeController>();
					newCubeController.currentPos = new int[] {i, j};
				} else if(cubesOccupation[i][j] < 0){
					GameObject newCube = Instantiate(cubeElement[3]) as GameObject;
					CubeController newCubeController = newCube.GetComponent<CubeController>();
					newCubeController.currentPos = new int[] {i, j};
				}
			}
			
			manager.CubeStart(cubesOccupation);
		}
	}
}
