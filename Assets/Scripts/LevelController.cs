using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataTypes;
using FlowCanvas;
using JetBrains.Annotations;
using NodeCanvas.Framework;
using UnityEditor;
using UnityEngine;

public class LevelController : MonoBehaviour {
	public PlayerStatus playerStartStatus;
	public CameraStatus cameraStartStatus;
	public CameraController cameraController;
	public GameObject starter;
	public GameObject camera;
	
//	public Transform playerStartTrans;
//	public Animator UIAnimator;

	private int rota = 0;

	private int bluePts;
	private int orangePts;
	private GameObject[] triggers;
//	private PlayerSaveInfo lastSave;

//	public static bool s_drawAllGizmos;
//	public bool drawAllGizmos;
	


	private void Awake() {
//		s_drawAllGizmos = drawAllGizmos;
	}

	private void Start() {
		triggers = GameObject.FindGameObjectsWithTag("Trigger");
//		lastSave.init(playerStartTrans, playerStartStatus);
	}

	private void Update() {
//		Debug.Log(FindLastActiveSave().name);
//		Debug.Log(Time.deltaTime);
	}

	public void PlayerDie() {
		GraphOwner.SendGlobalEvent("PlayerDie");
//		Time.timeScale = 0;
//		cameraController.PlayerDieAnimation();
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

	public void CollectPt(char color) {
		switch (color) {
				case 'b':
					Debug.Log("Collect blue pt");
					bluePts++;
					break;
				case 'o':
					Debug.Log("Collect orange pt");
					orangePts++;
					break;
		}
	}

	public int GetBluePts() {
		return bluePts;
	}

	public int GetOrangePts() {
		return orangePts;
	}

	public string GetLastSaveName() {
		return FindLastActiveSave().name;
	}

	public void RestartFromCheckpoint() {
		GraphOwner.SendGlobalEvent("Restart");
		var player = GameObject.FindGameObjectWithTag("Player");
		var lastSave = FindLastActiveSave();
		lastSave.GetComponent<TriggerController>().ResetTrigger();
		var saveInfo = lastSave.GetComponent<TriggerController>().saveInfo;
		var rotation = lastSave.transform.rotation;
		rotation.eulerAngles = new Vector3(0f, 0f, rotation.eulerAngles.z + saveInfo.saveOrientationDelta);
		player.transform.SetPositionAndRotation(lastSave.transform.position + (Vector3)saveInfo.savePositionDelta, 
			rotation);
		
		player.GetComponent<PlayerController>().UpdateStatus(saveInfo.GetPlayerStatus());
//		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		camera.GetComponent<CameraController>().UpdateStatus(saveInfo.GetCameraStatus());
//		GameObject.FindGameObjectWithTag("MainCamera").transform.SetPositionAndRotation(saveInfo.GetCameraTrans().position, saveInfo.GetCameraTrans().rotation);
		camera.transform.position = saveInfo.cameraPosition;
		Quaternion newRotation = Quaternion.Euler(0f, 0f, saveInfo.cameraOrientation);
		camera.transform.rotation = newRotation;
		
		Time.timeScale = 1f;
	}

	private GameObject FindLastActiveSave() {
		foreach (var trigger in triggers) {
			if (trigger.GetComponent<TriggerController>().IsActiveSave()) {
				return trigger;
			}
		}

		return starter;
	}
}
//
//[ExecuteInEditMode]
//public class NamingProcess : MonoBehaviour {
//	public static int triggerCount = 0;
//
//	public static void IncrementTrigger() {
//		triggerCount += 1;
//	}
//}
