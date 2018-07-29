using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes;

public class PlayerController : MonoBehaviour {
	public float tSpeed;
	
	private PlayerStatus playerStatus;
	private CameraStatus cameraStatus;
	private LevelController levelController;
	private int rota;

//	public PlayerStatus GetStatus() {
//		return  playerStatus;
//	}

	private void OnEnable() {
		levelController = (LevelController) FindObjectOfType(typeof(LevelController));
		CameraController cameraController = (CameraController) FindObjectOfType(typeof(CameraController));
		cameraController.GetStatus(ref cameraStatus);
		cameraStatus.intendSize += 5f;
		playerStatus.CopyFrom(levelController.playerStartStatus);
	}

	private void Update() {
		rota = levelController.GetRotationDir();
//		playerStatus.speed = tSpeed;
		playerStatus.Update();
//		Debug.Log(playerStatus.speed);
		transform.Translate(0f, playerStatus.speed * Time.deltaTime, 0f);
		transform.Rotate(Vector3.back * rota * playerStatus.rotationSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Wall")) {
			levelController.PlayerDie();
//			Debug.Log(other.gameObject.tag);
		}
		else if(other.CompareTag("Trigger")) {
			TriggerController controller = other.gameObject.GetComponent<TriggerController>();
			controller.Trigger(ref playerStatus, ref cameraStatus);
//			if (controller.changePlayerStatus) {
//				controller.UpdatePlayerStatus(ref playerStatus);
//			}
		}
	}
}
