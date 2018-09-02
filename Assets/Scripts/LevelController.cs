using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DataTypes;
using JetBrains.Annotations;
using UnityEngine;

public class LevelController : MonoBehaviour {
	public PlayerStatus playerStartStatus;
	public CameraStatus cameraStartStatus;
	public CameraController cameraController;
	public GameObject starter;
	
	public Transform playerStartTrans;

	private int rota = 0;

	private int bluePts;
	private int orangePts;
	private GameObject[] triggers;
	private PlayerSaveInfo lastSave;
	
	GUIContent content;

	private void Start() {
		triggers = GameObject.FindGameObjectsWithTag("Trigger");
//		lastSave.init(playerStartTrans, playerStartStatus);
		content = new GUIContent("blue pts: " + bluePts);
	}

	private void Update() {
		Debug.Log(FindLastActiveSave().name);
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
					break;
		}
	}

	private GameObject FindLastActiveSave() {
		foreach (var trigger in triggers) {
			if (trigger.GetComponent<TriggerController>().IsActiveSave()) {
				return trigger;
			}
		}

		return starter;
	}

#if UNITY_EDITOR
	private void OnGUI() {
		GUI.skin.box.normal.textColor = Color.black;
		GUI.skin.box.font = Font.CreateDynamicFontFromOSFont("Tahoma Regular", 15);
//		GUI.skin.box.fontSize = 5;
		//		Debug.Log(Font.CreateDynamicFontFromOSFont("Tahoma Regular",10));
		//		Debug.Log("Font name: " + GUI.skin.box.font.name);
		//		GUI.skin.box.fontSize = 40;
		GUI.Box(new Rect(10f, 10f, 200f, 100f), content);
		Debug.Log(content);
	}
#endif
	
}
