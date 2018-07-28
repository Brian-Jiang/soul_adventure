using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerVC : MonoBehaviour {
	public Transform playerTransform;
	public float rotationDeltaMultiplier;
	
	private float rotationDelta = 0f;
	
	void Update () {
		float angle = Quaternion.Angle(transform.rotation, playerTransform.rotation);
		rotationDelta = angle / 10f * rotationDeltaMultiplier;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, playerTransform.rotation, rotationDelta);
	}
	
}
