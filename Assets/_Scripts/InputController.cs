using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour 
{
	public Camera mainCamera;
	public CubesManager manager;
	private Direction.Cube _direction = Direction.Cube.None;
	private GameObject cubeSelected;
	private Vector3 initPointerPos;
	private Vector3 finalPointerPos;
	private bool isInputAllow = true;
	
	// Update is called once per frame
	void Update () 
	{
		if(!isInputAllow) {
			Debug.Log("InputController is blocking the inputs.");
			return;
		}

		bool mouseLeftClickDown = Input.GetMouseButtonDown(0);
		bool mouseLeftClickUp = Input.GetMouseButtonUp(0);

		if (mouseLeftClickDown) 
		{	
			this.initPointerPos = Input.mousePosition;
			checkRaycast();
		}

		if(!this.cubeSelected) return; 	// If after releases the mouse button no cube was hit by the raycast, do nothing.

		if(mouseLeftClickUp) this._direction = CheckDirection();
		
		if(this._direction != Direction.Cube.None){
			this.manager.SendCubeToNewPos(this._direction, this.cubeSelected); // Ask to Manager to send the cube to a new position.
			this._direction = Direction.Cube.None;	//Resets the direction.
		}
	}

  private Direction.Cube CheckDirection()
  {	
		finalPointerPos	= Input.mousePosition;

		float max = 50f;
		float difX = Mathf.Abs(initPointerPos.x - finalPointerPos.x);
		float difY = Mathf.Abs(initPointerPos.y - finalPointerPos.y);

		if(difX > max)
		{
			if(initPointerPos.x < finalPointerPos.x)
				return Direction.Cube.Right;
			else if(initPointerPos.x > finalPointerPos.x)
				return Direction.Cube.Left;
		}
		else if(difY > max)
		{
			if(initPointerPos.y < finalPointerPos.y)
				return Direction.Cube.Front;
			else if(initPointerPos.y > finalPointerPos.y)
				return Direction.Cube.Back;
		}

		return Direction.Cube.None;
  }

  private void checkRaycast () 
	{
 			RaycastHit hit; 
 			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); 
 			if ( Physics.Raycast (ray,out hit,100.0f)){ 
 				if(hit.transform!=null) {
					this.cubeSelected = hit.transform.gameObject;
				}
 			}
	}
}
