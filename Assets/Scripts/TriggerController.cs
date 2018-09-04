using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEngine;

public class TriggerController : MonoBehaviour {
    public bool changePlayerStatus;
    public bool changeCameraStatus;
    public bool willSaveProgress;
    public Animator[] animatorList;
    public PlayerStatusConverter playerStatusConverter;
    public CameraStatusConverter cameraStatusConverter;
    public PlayerSaveInfo saveInfo;

    private bool triggered = false;

    public void UpdatePlayerStatus(ref PlayerStatus status) {
        if (changePlayerStatus && !triggered) {
            status.UpdateFromConverter(playerStatusConverter);
        }
    }

    public void UpdateCameraStatus(ref CameraStatus status) {
        if (changeCameraStatus && !triggered) {
            status.UpdateFromConverter(cameraStatusConverter);
        }
    }

    public void PlayAnimations() {
        foreach (var animator in animatorList) {
            animator.SetTrigger("Trigger");
        }
    }

    public void SaveProgress(PlayerStatus status) {
        if (!willSaveProgress) return;
        var triggers = GameObject.FindGameObjectsWithTag("Trigger");
        foreach (var trigger in triggers) {
            trigger.GetComponent<TriggerController>().ResetSaving();
        }

        saveInfo.Save(status);
    }

    public void ResetSaving() {
        saveInfo.Reset();
    }

    public bool IsActiveSave() {
        return saveInfo.IsActive();
    }

    public void IsTriggered() {
        triggered = true;
    }
    
#if UNITY_EDITOR

    private void OnDrawGizmos() {
//        if (LevelController.s_drawAllGizmos) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
            Handles.Label(transform.position - transform.localScale / 2, gameObject.name);
//        }
    }

#endif
}
