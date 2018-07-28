using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataTypes;

public class PlayerController : MonoBehaviour {
	public float tSpeed;
	
	private PlayerStatus status;
	private LevelController levelController;
	private int rota;

//	public PlayerStatus GetStatus() {
//		return  status;
//	}

	private void OnEnable() {
		levelController = (LevelController) FindObjectOfType(typeof(LevelController));
		status.CopyFrom(levelController.playerStartStatus);
	}

	private void Update() {
		rota = levelController.GetRotationDir();
//		status.speed = tSpeed;
		status.Update();
		Debug.Log(status.speed);
		transform.Translate(0f, status.speed * Time.deltaTime, 0f);
		transform.Rotate(Vector3.back * rota * status.rotationSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Wall")) {
			levelController.PlayerDie();
			Debug.Log(other.gameObject.tag);
		}
		else if(other.CompareTag("Trigger")) {
			TriggerController controller = other.gameObject.GetComponent<TriggerController>();
			if (controller.changePlayerStatus) {
				controller.UpdatePlayerStatus(ref status);
			}
		}
	}
}
