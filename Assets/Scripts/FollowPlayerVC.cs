using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerVC : MonoBehaviour {
	public Transform playerTransform;
	public float rotationDeltaMultiplier;
	
	private float rotationDelta = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float angle = Quaternion.Angle(transform.rotation, playerTransform.rotation);
		rotationDelta = angle / 10f * rotationDeltaMultiplier;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, playerTransform.rotation, rotationDelta);
//		transform.rotation = Quaternion.Euler(0f, 0f, 50f);
	}
	
}
