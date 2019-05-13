using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour {

	int[][] cubesOccupation = {
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
	GameObject cubeSelected;

	bool hasSetCubes = false;
	public void CubeStart(int[][] cubesOccupation){
		this.cubesOccupation = cubesOccupation;
		this.hasSetCubes = true;
	}

	public void checkSurrondings(Direction.Cube direction, GameObject targetCube)
	{
		if(!this.hasSetCubes) return;

		this.cubeSelected = targetCube;
		CubeController targetController = targetCube.GetComponent<CubeController>();
		int[] targetPos = targetController.currentPos;

		targetController.MoveCube(setCubeNewPos(targetPos, direction));
	}

	int[] setCubeNewPos(int[] currentPos, Direction.Cube _direction){
		int[] nextPos = (int[])currentPos.Clone();
		int directionValue = 0;
		bool canMove = false;
		
		int cubeID = cubesOccupation[currentPos[0]][currentPos[1]];

		if(_direction == Direction.Cube.Left){
			canMove = currentPos[1] % 4 > 0 || (currentPos[1] - 1) == 0;
			directionValue = -1;
		}
		if(_direction == Direction.Cube.Right){
			canMove = (currentPos[1] + 1) % 4 != 0 || currentPos[1] < 3;
			directionValue = 1;
		}
		if(_direction == Direction.Cube.Back){
			canMove = currentPos[1] + 4 < 16;
			directionValue = 4;
		}
		if(_direction == Direction.Cube.Front){
			canMove = currentPos[1] - 4 >= 0;
			directionValue = -4;
		}

		if(canMove){ 
			if(currentPos[0] < 3){ // Check if there is some block over the current selected
				if(cubesOccupation[currentPos[0] + 1][currentPos[1]] > 0){
					canMove = false;
				}
				if(cubesOccupation[currentPos[0]][currentPos[1]] > 1){
					canMove = false;
				}
			}
		}

		if(canMove){
			if(cubesOccupation[currentPos[0]][currentPos[1] + directionValue] <= 0){
				if(currentPos[0] >= 1){
					if(cubesOccupation[currentPos[0] - 1][currentPos[1] + directionValue] > 0) { // Se existir cubo no nivel inferior move
						nextPos[1] += directionValue;
					} else if (currentPos[0] >= 2) {
						if (cubesOccupation[currentPos[0] - 2][currentPos[1] + directionValue] > 0){ // se existe
							nextPos[0] += -1;
							nextPos[1] += directionValue;
						}
					} else if(currentPos[0] - 1 == 0){
						nextPos[0] += -1;
						nextPos[1] += directionValue;
					}
				} else if(currentPos[0] == 0){
					nextPos[1] += directionValue;
				}
			} else if(nextPos[0] < 3) {
				if(cubesOccupation[currentPos[0] + 1][currentPos[1] + directionValue] <= 0){
					nextPos[1] += directionValue;
					nextPos[0] += 1;
				}
			}
		}

		cubesOccupation[currentPos[0]][currentPos[1]] = 0;
		cubesOccupation[nextPos[0]][nextPos[1]] = cubeID;
		return nextPos;
	}
}
