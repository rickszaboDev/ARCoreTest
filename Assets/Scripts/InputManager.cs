using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public Camera mainCamera;
	public CubesManager manager;
	Direction.Cube _direction = Direction.Cube.None;
	GameObject cubeSelected;

	Vector3 initPointerPos;
	Vector3 finalPointerPos;

	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) 
		{	
			this.initPointerPos = Input.mousePosition;
			checkRaycast();
		}

		if(cubeSelected == null) return;

		if(Input.GetMouseButtonUp(0)) this._direction = CheckDirection();
		
		if(this._direction != Direction.Cube.None){
			this.manager.checkSurrondings(this._direction, this.cubeSelected);
			this._direction = Direction.Cube.None;
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
 			//suppose i have two objects here named obj1 and obj2.. how do i select obj1 to be transformed 
 			if(hit.transform!=null) { 
 				// transform.Translate (Time.deltaTime, 0, 0, Space.Self); 
				this.cubeSelected = hit.transform.gameObject;
			} 
 		}
	}
}
