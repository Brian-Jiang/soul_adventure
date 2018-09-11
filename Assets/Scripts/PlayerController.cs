using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes;

public class PlayerController : MonoBehaviour {
//	public AnimationCurve leftRotate;
//	public AnimationCurve rightRotate;

	private CameraController cameraController;
	private PlayerStatus playerStatus;
	private CameraStatus cameraStatus;
	private LevelController levelController;
	private int rota;
//	private bool onLeft;
//	private bool onRight;
	
//	public void Swipe(bool left) {
//		if (left) {
//			onLeft = true;
//		}
//		else {
//			onRight = true;
//		}
//	}

	public PlayerStatus GetPlayerStatus() {
		return playerStatus;
	}

	private void OnEnable() {
		levelController = (LevelController) FindObjectOfType(typeof(LevelController));
		cameraController = (CameraController) FindObjectOfType(typeof(CameraController));
		playerStatus.CopyFrom(levelController.playerStartStatus);
	}

	private void Update() {
		rota = levelController.GetRotationDir();
		playerStatus.Update();
		transform.Translate(0f, playerStatus.speed * Time.deltaTime, 0f);
		transform.Rotate(Vector3.back * rota * playerStatus.rotationSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Wall")) {
			levelController.PlayerDie();
		} else if(other.CompareTag("Trigger")) {
			var controller = other.gameObject.GetComponent<TriggerController>();
			controller.UpdatePlayerStatus(ref playerStatus);
			cameraController.Trigger(controller);
			controller.PlayAnimations();
			controller.SaveProgress(playerStatus);
			controller.IsTriggered();
		} else if (other.CompareTag("blue_pt")) {
			levelController.CollectPt('b');
			Destroy(other.gameObject);
		} else if (other.CompareTag("orange_pt")) {
			levelController.CollectPt('o');
			Destroy(other.gameObject);
		}
	}
}
