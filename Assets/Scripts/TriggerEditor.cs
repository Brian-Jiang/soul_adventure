using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(TriggerController))]
//[CanEditMultipleObjects]
public class TriggerEditor : Editor {
//	SerializedProperty playerStatusConverterProperty;
//	SerializedProperty cameraStatusConverterProperty;
//	private bool changePlayerStatus;
//	private bool changeCameraStatus;
//    
//	void OnEnable() {
//		changePlayerStatus = serializedObject.FindProperty("changePlayerStatus").boolValue;
//		changeCameraStatus = serializedObject.FindProperty("changeCameraStatus").boolValue;
//		playerStatusConverterProperty = serializedObject.FindProperty("playerStatusConverter");
//		cameraStatusConverterProperty = serializedObject.FindProperty("cameraStatusConverter");
//	}
//	
//	public override void OnInspectorGUI(){
//		serializedObject.Update();
//
//		changePlayerStatus = EditorGUILayout.Toggle("Change Player Status", changePlayerStatus);
//		if (changePlayerStatus) {
//			EditorGUILayout.Slider(playerStatusConverterProperty.FindPropertyRelative("intendedSpeed"), 0f, 30f, "Intended Player Speed");
//			EditorGUILayout.Slider(playerStatusConverterProperty.FindPropertyRelative("intendedRotationalSpeed"), 5f, 200f, "Intended Player Rotational Speed");
//			EditorGUILayout.Slider(playerStatusConverterProperty.FindPropertyRelative("time"), 1f, 15f, "Time");
//		}
//		
//		changeCameraStatus = EditorGUILayout.Toggle("Change Camera Status", changeCameraStatus);
//		Debug.Log(changeCameraStatus);
//		if (changeCameraStatus) {
////			EditorGUILayout.Slider(cameraStatusConverterProperty.FindPropertyRelative("intendedFocusDelta"), 0f, 30f, "Intended DFP");
//			EditorGUILayout.Slider(cameraStatusConverterProperty.FindPropertyRelative("intendedSize"), 3f, 25f, "Intended Camera Size");
//			EditorGUILayout.Slider(cameraStatusConverterProperty.FindPropertyRelative("time"), 1f, 15f, "Time");
//		}
//		
//		serializedObject.ApplyModifiedProperties();
//	}
}
