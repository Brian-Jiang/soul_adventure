using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEngine;

public class TriggerController : MonoBehaviour {
    public bool changePlayerStatus;
    public PlayerStatusConverter statusConverter;

    public void UpdatePlayerStatus(ref PlayerStatus status) {
        status.UpdateFromConverter(statusConverter);
    }
}
