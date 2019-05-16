using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTimeCubeContrl : CubeController 
{
	bool hasBack = true;
	public override void MoveCube(int[] pos)
	{
		base.MoveCube(pos);
		hasBack = false;
	}
	public override void OnAnimationEnded()
	{
		if(!hasBack)
		{
			StartCoroutine("backToInitialPosition");
		} else {
			base.OnAnimationEnded();
		}
	}
	IEnumerator backToInitialPosition()
	{
		Direction.Cube backDirection = inverseDirection(this.lastDirection);

		yield return new WaitForSeconds(2);
		manager.SendCubeToNewPos(backDirection, gameObject);
		hasBack = true;
	}
	private Direction.Cube inverseDirection(Direction.Cube _dir)
	{
		Direction.Cube _inverseDirection = Direction.Cube.None;
		if(_dir == Direction.Cube.Left)
		{
			_inverseDirection = Direction.Cube.Right;
		}
		else if(_dir == Direction.Cube.Right)
		{
			_inverseDirection = Direction.Cube.Left;
		}
		else if(_dir == Direction.Cube.Front)
		{
			_inverseDirection = Direction.Cube.Back;
		}
		else if(_dir == Direction.Cube.Back)
		{
			_inverseDirection = Direction.Cube.Front;
		}

		return _inverseDirection;
	}
}
