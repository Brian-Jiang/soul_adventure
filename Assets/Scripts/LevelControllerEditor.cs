using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelController))]
[CanEditMultipleObjects]
public class LevelControllerEditor : Editor {
	private SerializedProperty playerStartTrans;
	
	void OnEnable() {
		playerStartTrans = serializedObject.FindProperty("playerStartTrans");
	}

	public override void OnInspectorGUI() {
		LevelController controller = (LevelController) target;
		serializedObject.Update();
		
//		base.OnInspectorGUI();
		DrawDefaultInspector();
		controller.playerStartTrans.position = EditorGUILayout.Vector2Field("start", controller.playerStartTrans.position);
		
		serializedObject.ApplyModifiedProperties();
	}
}
