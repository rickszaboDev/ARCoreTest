using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CubeController {
		public Animator animContrl;

		public override void MoveCube(int[] pos)
		{
			base.MoveCube(pos);
			animContrl.SetBool("move", true);
			animContrl.SetFloat("speed", 0.5f);
			Vector3 _lookPoint = new Vector3(myNextPos.x, transform.position.y, myNextPos.z);
			transform.LookAt(_lookPoint);
		}

		public override void OnAnimationEnded()
		{
			base.OnAnimationEnded();
			animContrl.SetBool("move", false);
			animContrl.SetFloat("speed", 0f);
		}
}