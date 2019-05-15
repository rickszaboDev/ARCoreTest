using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	[HideInInspector] public int[] curPos = {0, 0};

	private bool changePosition = true;
	Animation anim;
	void Start()
	{
		anim = GetComponent<Animation>();
	}
	void Update()
	{
		if(changePosition)	this.sendToNewPos();
	}
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Exit")
		{
		}
	}
	private void moveCube(int[] pos)
	{
		this.curPos = pos;
		changePosition = true;
		
		setAnimationTo(this.curPos);
	}

	private void setAnimationTo(int[] pos){
		Vector3 nextPos = CubePosition.boxPositions[pos[0]][pos[1]].position;
		AnimationCurve curveX, curveY, curveZ;

		AnimationClip clip = new AnimationClip();
		clip.legacy = true;

		Keyframe[] keys;
		keys = new Keyframe[3];
		keys[0] = new Keyframe(0.0f, transform.position.x);
		keys[1] = new Keyframe(1.0f, transform.position.x);
		keys[2] = new Keyframe(2.0f, nextPos.x);
		curveX = new AnimationCurve(keys);
		clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);

		keys = new Keyframe[3];
		keys[0] = new Keyframe(0.0f, transform.position.z);
		keys[1] = new Keyframe(1.0f, transform.position.z);
		keys[2] = new Keyframe(2.0f, nextPos.z);
		curveZ = new AnimationCurve(keys);
		clip.SetCurve("", typeof(Transform), "localPosition.z", curveZ);

		keys = new Keyframe[3];
		keys[0] = new Keyframe(0.0f, transform.position.y);
		keys[1] = new Keyframe(1.0f, nextPos.y);
		keys[2] = new Keyframe(2.0f, nextPos.y);
		curveY = new AnimationCurve(keys);
		clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);

		anim.AddClip(clip, clip.name);
		anim.Play(clip.name);
	}


	private void sendToNewPos()
	{
			Vector3 nextPos = CubePosition.boxPositions[curPos[0]][curPos[1]].position;		
			transform.position = nextPos;
			changePosition = false;
	}
	private bool checkBoundaries(int[] currentPos, Direction.Cube _direction){
		bool canMove = false;

		if(_direction == Direction.Cube.Left){
			canMove = currentPos[1] % 4 > 0 || (currentPos[1] - 1) == 0;
		}
		if(_direction == Direction.Cube.Right){
			canMove = (currentPos[1] + 1) % 4 != 0 || currentPos[1] < 3;
		}
		if(_direction == Direction.Cube.Back){
			canMove = currentPos[1] + 4 < 16;
		}
		if(_direction == Direction.Cube.Front){
			canMove = currentPos[1] - 4 >= 0;
		}

		return !canMove;
	}
	private bool checkBlockOverTarget(int[][] cubesOccupation, int[] _currentPos){
		if(_currentPos[0] < 3){ // Check if there is some block over the current selected
			if(cubesOccupation[_currentPos[0] + 1][_currentPos[1]] > 0){
				return true;
			}
			if(cubesOccupation[_currentPos[0]][_currentPos[1]] > 1){
				return true;
			}
		}
		return false;
	}
	private int setDirectionValue(Direction.Cube _direction){
		var directionValue = 0;
		
		if(_direction == Direction.Cube.Left){
			directionValue = -1;
		}
		if(_direction == Direction.Cube.Right){
			directionValue = 1;
		}
		if(_direction == Direction.Cube.Back){
			directionValue = 4;
		}
		if(_direction == Direction.Cube.Front){
			directionValue = -4;
		}

		return directionValue;
	}
	public Dictionary<string, int> GetCubeNewPos(int[][] cubesOccupation, Direction.Cube _direction)
	{
		var _value = new Dictionary<string, int> { 
			{"x", this.curPos[0]},
			{"y", this.curPos[1]},
			{"cubeID", cubesOccupation[this.curPos[0]][this.curPos[1]]},
			{"newX", this.curPos[0]},
			{"newY", this.curPos[1]}
		};

		if(checkBoundaries(this.curPos, _direction)) return _value;
		if(checkBlockOverTarget(cubesOccupation, this.curPos)) return _value;
		
		int[] nextPos = (int[])this.curPos.Clone();
		var directionValue = setDirectionValue(_direction);

		if(cubesOccupation[this.curPos[0]][this.curPos[1] + directionValue] <= 0)
		{ 				
			if(this.curPos[0] >= 1)
			{																
				if(cubesOccupation[this.curPos[0] - 1][this.curPos[1] + directionValue] > 0) 
				{ 	
					nextPos[1] += directionValue;												
				}
				else if (this.curPos[0] >= 2) 
				{												
					if (cubesOccupation[this.curPos[0] - 2][this.curPos[1] + directionValue] > 0)
					{
						nextPos[0] += -1;														
						nextPos[1] += directionValue;											
					}
				} 
				else if(this.curPos[0] - 1 == 0)
				{															
					nextPos[0] += -1;
					nextPos[1] += directionValue;
				}
			} 
			else if(this.curPos[0] == 0)
			{
				nextPos[1] += directionValue;
			}
		} 
		else if(nextPos[0] < 3) 
		{
			if(cubesOccupation[this.curPos[0] + 1][this.curPos[1] + directionValue] <= 0)
			{
				nextPos[1] += directionValue;
				nextPos[0] += 1;
			}
		}
		
		_value["newX"] = nextPos[0];
		_value["newY"] = nextPos[1];

		moveCube(nextPos);
		return _value;
	}
}