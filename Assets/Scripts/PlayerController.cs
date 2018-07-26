using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes;

public class PlayerController : MonoBehaviour {
	public float tSpeed;
//	public float tA;
	
	private PlayerStatus status;
	private LevelController levelController;
	private int rota;

	public PlayerStatus GetStatus() {
		return  status;
	}

	private void OnEnable() {
		levelController = (LevelController) FindObjectOfType(typeof(LevelController));
		status.CopyFrom(levelController.playerStartStatus);
		Debug.Log(status);
	}

	private void Update() {
		rota = levelController.GetRotationDir();
		status.speed = tSpeed;
//		status.acceleration = tA;
		transform.Translate(0f, status.speed * Time.deltaTime, 0f);
		transform.Rotate(Vector3.back * rota * status.rotationSpeed * Time.deltaTime);
//		status.acceleration++;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		levelController.PlayerDie();
		Debug.Log(other.gameObject.tag);
	}
}
