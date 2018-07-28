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
        public float intendRotationSpeed;
        
        private float acceleration; //  unit/second
        private float rotationAcceleration;

        public void CopyFrom(PlayerStatus status) {
            speed = status.speed;
            intendSpeed = status.intendSpeed;
            intendRotationSpeed = status.intendRotationSpeed;
            acceleration = status.acceleration;
            rotationAcceleration = status.rotationAcceleration;
            rotationSpeed = status.rotationSpeed;
        }

        public void UpdateFromConverter(PlayerStatusConverter converter) {
            intendSpeed = converter.intendedSpeed;
            acceleration = (intendSpeed - speed) / converter.time;
        }

        public void Update() {
            if (Mathf.Abs(speed - intendSpeed) > 0.02f) {
                speed += acceleration * Time.deltaTime;
            }
        }
    }

    [Serializable]
    public struct PlayerStatusConverter {
        public float intendedSpeed;
        public float intendedRotationalSpeed;
        public float time; //   second
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
