using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTimeCubeContrl : CubeController {

	public override void MoveCube(int[] pos){
		base.MoveCube(pos);
	}

	void OnTriggerEnter(Collider col){

	}
}
