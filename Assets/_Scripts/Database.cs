using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour {
	public int[][] cubesOccupation = {
		new int[] { // Level 0
			0, 1, 1, 1,
			0, 0, 1, 1,
			0, 0, 1, 1,
			0, 0, 1, 1
		},
		new int[] { // Level 1
			0, 0, 0, 0,
			0, 0, 0, 1,
			0, 0, 0, 0,
			0, 0, 0, 0
		},
		new int[] { // Level 2
			0, 0, 0, 0,
			0, 0, 0, 1,
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

}
