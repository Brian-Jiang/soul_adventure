using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataTypes {
    public enum colliderType {
        Player, Wall, Point
    }

    [Serializable]
    public struct PlayerStatus {
        public float speed;
        public float intendSpeed;
        public float rotationSpeed;
        
        private float acceleration;
        

        public void CopyFrom(PlayerStatus status) {
            speed = status.speed;
            intendSpeed = status.intendSpeed;
            acceleration = status.acceleration;
            rotationSpeed = status.rotationSpeed;
        }
    }

    
    [Serializable]
    public struct CameraStatus {
        public Vector2 deltaFocusPoint;
        public float zoomSize;
        public bool isFollowingPlayer;
        public bool isFollowingPlayerRotation;

        public void CopyFrom(CameraStatus status) {
            deltaFocusPoint = status.deltaFocusPoint;
            zoomSize = status.zoomSize;
            isFollowingPlayer = status.isFollowingPlayer;
            isFollowingPlayerRotation = status.isFollowingPlayerRotation;
        }
    }
}
