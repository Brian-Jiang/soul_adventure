using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEditor.Animations;
using UnityEngine;

public class CameraController : MonoBehaviour {
//	public Transform playerTransform;

//	private Camera camera;
	private Animator animator;
//	private GameObject player;
//	private PlayerController playerController;
	private CameraStatus status;
//	private Vector2 desiredVelocity;
//	private Rigidbody2D rigi;
//	private Vector3 velocity = Vector3.zero;

	public void PlayerDieAnimation() {
		animator.SetTrigger("Die");
	}
	

	private void OnEnable() {
//		rigi = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
//		player = GameObject.FindGameObjectWithTag("Player");
//		playerController = player.GetComponent<PlayerController>();
//		playerTransform = GameObject.Find("Player/PlayerCenterReferencePoint").transform;
	}


}
