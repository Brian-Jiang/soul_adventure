using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEngine;

public class TriggerController : MonoBehaviour {
    public bool changePlayerStatus;
    public bool changeCameraStatus;
    public PlayerStatusConverter playerStatusConverter;
    public CameraStatusConverter cameraStatusConverter;

    public void UpdatePlayerStatus(ref PlayerStatus status) {
        if (changePlayerStatus) {
            status.UpdateFromConverter(playerStatusConverter);
        }
    }

    public void UpdateCameraStatus(ref CameraStatus status) {
        if (changeCameraStatus) {
            status.UpdateFromConverter(cameraStatusConverter);
        }
    }
}
