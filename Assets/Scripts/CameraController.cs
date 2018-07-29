using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEditor.Animations;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform playerTransform;
	public float translationalDeltaMultiplier;
	public float rotationDeltaMultiplier;
	public float sizeDeltaMultiplier;

	private float translationDelta = 0f;
	private float rotationDelta = 0f;
//	private float cameraSizeDelta = 0f;
	private LevelController levelController;
	private Animator animator;
	private CameraStatus status;
	private Camera camera;

	private void OnEnable() {
		levelController = (LevelController) FindObjectOfType(typeof(LevelController));
		animator = GetComponent<Animator>();
		camera = GetComponent<Camera>();
		status.CopyFrom(levelController.cameraStartStatus);
	}

	public ref CameraStatus GetStatus(ref CameraStatus cStatus) {
//		cStatus = status;
		return ref status;
	}

	public void PlayerDieAnimation() {
		animator.SetTrigger("Die");
	}

	private void Update() {
		status.Update();
		
		// move position
		Vector2 currentPosition = transform.position;
		translationDelta = Vector2.Distance(currentPosition, playerTransform.position) / 10f * translationalDeltaMultiplier;
		Vector2 newPosition = Vector2.MoveTowards(currentPosition, playerTransform.position, translationDelta);
		newPosition += status.deltaFocusPoint;
		transform.position = (Vector3)newPosition + Vector3.back;

		// change size
		camera.orthographicSize = status.size;
		
		// change rotation
		float angle = Quaternion.Angle(transform.rotation, playerTransform.rotation);
		rotationDelta = angle / 10f * rotationDeltaMultiplier;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, playerTransform.rotation, rotationDelta);
	}
}
