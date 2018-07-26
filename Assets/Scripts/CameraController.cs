using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEditor.Animations;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform playerTransform;

//	private Camera camera;
	private Animator animator;
	private GameObject player;
	private PlayerController playerController;
	private CameraStatus status;
	private Vector2 desiredVelocity;
	private Rigidbody2D rigi;
	private Vector3 velocity = Vector3.zero;

	public void PlayerDieAnimation() {
		animator.SetTrigger("Die");
	}
	

	private void OnEnable() {
		rigi = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
//		camera = GetComponent<Camera>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerController = player.GetComponent<PlayerController>();
		playerTransform = GameObject.Find("Player/PlayerCenterReferencePoint").transform;
//		Debug.Log(playerTransform.position);
//		Follow(playerTransform.position, 5f);
//		Vector2.SmoothDamp(transform.position, playerTransform.position, ref desiredVelocity, 5f);
	}

	private void Update () {
//		transform.position = player.transform.position + Vector3.back;
//		Follow(playerTransform, 0.1f);
//		CalculateAndMove();
	}

//	private void CalculateAndMove() {
//		float a = playerController.GetStatus().acceleration;
//		bool accelerationIsNegative = a < 0;
//		float d = Mathf.Log10(Mathf.Abs(a) + 1);
//		float angle = Vector2.Angle(Vector2.left, playerTransform.position - transform.position);
//		float dx = d * Mathf.Cos(Mathf.Deg2Rad * angle);
//		float dy = d * Mathf.Sin(Mathf.Deg2Rad * angle);
//		if (accelerationIsNegative) {
//			transform.position = new Vector3(playerTransform.position.x + dx, playerTransform.position.y + dy, -10f);
//		}
//		else {
//			transform.position = new Vector3(playerTransform.position.x - dx, playerTransform.position.y - dy, -10f);
//		}
//	}

//	void Follow(Transform target, float dampTime) {
//		if (target)
//		{
//			Vector3 point = camera.WorldToViewportPoint(target.position);
//			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
//			Vector3 destination = transform.position + delta;
//			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
//		}
//	}
}
