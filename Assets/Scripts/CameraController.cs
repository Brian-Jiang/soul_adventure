using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEditor.Animations;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	private Animator animator;
	private CameraStatus status;

	private void OnEnable() {
		animator = GetComponent<Animator>();
	}

	public void PlayerDieAnimation() {
		animator.SetTrigger("Die");
	}
}
