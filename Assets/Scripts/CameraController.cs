﻿using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEditor.Animations;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform playerTransform;
	public float translationalDeltaMultiplier;
	public float changeTargetTranslationDeltaMultiplier;
	public float rotationDeltaMultiplier;
	public float changeTargetRotationDeltaMultiplier;

	private float translationDelta = 0f;
	private float rotationDelta = 0f;
	private LevelController levelController;
	private Animator animator;
	private CameraStatus status;
	private Camera camera;

	private void OnEnable() {
		levelController = (LevelController) FindObjectOfType(typeof(LevelController));
		animator = GetComponent<Animator>();
		camera = GetComponent<Camera>();
		status.Init(levelController.cameraStartStatus);
	}

//	public void PlayerDieAnimation() {
//		animator.SetTrigger("Die");
//	}

	public void Trigger(TriggerController controller) {
		controller.UpdateCameraStatus(ref status);
	}

	public void SaveProgress(TriggerController controller) {
		controller.SaveCameraProgress(status, gameObject);
	}
	
	public void UpdateStatus(CameraStatus newStatus) {
		status.CopyFrom(newStatus);
	}

	private void Update() {
		if (Time.deltaTime.CompareTo(0f) == 0) return;
		status.Update();
		
		// move position
		if (!status.hasTarget) {
			Vector2 currentPosition = transform.position;
			translationDelta =
				Vector2.Distance(currentPosition, playerTransform.position + (Vector3) status.focusDelta) / 10f *
				translationalDeltaMultiplier;
			if (translationDelta > status.GetCameraMaxDelta()) {
				translationDelta = status.GetCameraMaxDelta();
			}
			Vector2 newPosition =
				Vector2.MoveTowards(currentPosition, playerTransform.position + (Vector3) status.focusDelta,
					translationDelta);
			transform.position = (Vector3) newPosition + Vector3.back;
		} else {
			Vector2 currentPosition = transform.position;
			translationDelta =
				Vector2.Distance(currentPosition, status.currentTarget) / 10f *
				changeTargetTranslationDeltaMultiplier;
			if (translationDelta > status.GetMaxDelta()) {
				translationDelta = status.GetMaxDelta();
			}
			if (translationDelta > status.GetCameraMaxDelta()) {
				translationDelta = status.GetCameraMaxDelta();
			}
			Vector2 newPosition =
				Vector2.MoveTowards(currentPosition, status.currentTarget, translationDelta);
			transform.position = (Vector3) newPosition + Vector3.back;
		}

		// change size
		camera.orthographicSize = status.size;
		
		// change rotation
		if (!status.hasOrientation) {
			float angle = Quaternion.Angle(transform.rotation, playerTransform.rotation);
			rotationDelta = angle / 10f * rotationDeltaMultiplier;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, playerTransform.rotation, rotationDelta);
		} else {
			Quaternion temp = Quaternion.identity;
			temp.eulerAngles = new Vector3(0f, 0f, status.currentOrientation);
			float angle = Quaternion.Angle(transform.rotation, temp);
			rotationDelta = angle / 10f * changeTargetRotationDeltaMultiplier;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, temp, rotationDelta);
		}
	}
}
