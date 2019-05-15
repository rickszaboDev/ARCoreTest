using UnityEngine;

public class CubePosition : MonoBehaviour {

	public Transform[] boxPositionLevel1 = new Transform[16];
	public Transform[] boxPositionLevel2 = new Transform[16];
	public Transform[] boxPositionLevel3 = new Transform[16];
	public Transform[] boxPositionLevel4 = new Transform[16];

	public static Transform[][] boxPositions = new Transform[4][];

	void Awake(){
		boxPositions[0] = boxPositionLevel1;
		boxPositions[1] = boxPositionLevel2;
		boxPositions[2] = boxPositionLevel3;
		boxPositions[3] = boxPositionLevel4;
	}
}
