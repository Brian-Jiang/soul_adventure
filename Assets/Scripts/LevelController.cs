using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataTypes;
using UnityEngine;

public class LevelController : MonoBehaviour {
	public PlayerStatus playerStartStatus;
	public CameraStatus cameraStartStatus;
	public CameraController cameraController;
	
	public Transform playerStartTrans;

	private int rota = 0;
	private PlayerSaveInfo lastSave;

	private void Start() {
		lastSave.init(playerStartTrans, playerStartStatus);
	}

	public void PlayerDie() {
		Time.timeScale = 0;
		cameraController.PlayerDieAnimation();
	}

	public void OnTouchEnter(bool left) {
		if (left) {
			rota -= 1;
		}
		else {
			rota += 1;
		}
	}
	
	public void OnTouchExit(bool left) {
		if (left) {
			rota += 1;
		}
		else {
			rota -= 1;
		}
	}

	public int GetRotationDir() {
		return rota;
	}
}
