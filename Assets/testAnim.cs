using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Animation anim = GetComponent<Animation>();
		AnimationCurve curveX, curveY, curveZ;

		AnimationClip clip = new AnimationClip();
		clip.legacy = true;

		Keyframe[] keys;
		keys = new Keyframe[3];
		keys[0] = new Keyframe(0.0f, transform.position.x);
		keys[1] = new Keyframe(1.0f, transform.position.x);
		keys[2] = new Keyframe(2.0f, 0.0f);
		curveX = new AnimationCurve(keys);
		clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);

		keys = new Keyframe[3];
		keys[0] = new Keyframe(0.0f, transform.position.z);
		keys[1] = new Keyframe(1.0f, transform.position.z);
		keys[2] = new Keyframe(2.0f, 0.0f);
		curveZ = new AnimationCurve(keys);
		clip.SetCurve("", typeof(Transform), "localPosition.z", curveZ);

		keys = new Keyframe[3];
		keys[0] = new Keyframe(0.0f, transform.position.y);
		keys[1] = new Keyframe(1.0f, 0.0f);
		keys[2] = new Keyframe(2.0f, 0.0f);
		curveY = new AnimationCurve(keys);
		clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);

		anim.AddClip(clip, clip.name);
		anim.Play(clip.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
