using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEditor.Purchasing;
using UnityEngine;

public class TriggerController : MonoBehaviour {
    public bool changePlayerStatus;
    public bool changeCameraStatus;
    public bool willSaveProgress;
    public Animator[] animatorList;
    public PlayerStatusConverter playerStatusConverter;
    public CameraStatusConverter cameraStatusConverter;

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

    public void SaveProgress() {
        
    }

    public void IsTriggered() {
        triggered = true;
    }
}
