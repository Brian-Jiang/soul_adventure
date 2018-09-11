using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataTypes;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class LevelController : MonoBehaviour {
	public PlayerStatus playerStartStatus;
	public CameraStatus cameraStartStatus;
	public CameraController cameraController;
	public GameObject starter;
	
	public Transform playerStartTrans;
	public Animator UIAnimator;

	private int rota = 0;

	private int bluePts;
	private int orangePts;
	private GameObject[] triggers;
	private PlayerSaveInfo lastSave;

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

	private GameObject FindLastActiveSave() {
		foreach (var trigger in triggers) {
			if (trigger.GetComponent<TriggerController>().IsActiveSave()) {
				return trigger;
			}
		}

		return starter;
	}
}
