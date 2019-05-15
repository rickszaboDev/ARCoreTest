using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	public int[] currentPos = {0, 0};

	bool changePosition = true;

	void Update(){
		if(changePosition){
			Vector3 nextPos = CubePosition.boxPositions[currentPos[0]][currentPos[1]].position;
			if(gameObject.tag == "Player"){
				GetComponent<Animator>().SetTrigger("moving");
				Vector3 lookPos = new Vector3(nextPos.x, transform.position.y, nextPos.z);
				transform.LookAt(lookPos);
			}

			transform.position = nextPos;
			changePosition = false;
		}
	}

	public void MoveCube(int[] pos){
		this.currentPos = pos;
		changePosition = true;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Exit")
		{
		}
	}
}