using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEngine;

public class TriggerController : MonoBehaviour {
    public bool changePlayerStatus;
    public bool changeCameraStatus;
    public PlayerStatusConverter playerStatusConverter;
    public CameraStatusConverter cameraStatusConverter;

    public void Trigger(ref PlayerStatus status, ref CameraStatus cameraStatus) {
        if (changePlayerStatus) {
            UpdatePlayerStatus(ref status);
        }

        if (changeCameraStatus) {
            UpdateCameraStatus(ref cameraStatus);
        }
        gameObject.SetActive(false);
    }

    private void UpdatePlayerStatus(ref PlayerStatus status) {
        status.UpdateFromConverter(playerStatusConverter);
    }

    private void UpdateCameraStatus(ref CameraStatus status) {
        status.UpdateFromConverter(cameraStatusConverter);
    }
}
