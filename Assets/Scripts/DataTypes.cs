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
            intendRotationSpeed = converter.intendedRotationalSpeed;
            acceleration = (intendSpeed - speed) / converter.time;
            rotationAcceleration = (intendRotationSpeed - rotationSpeed) / converter.time;
        }

        public void Update() {
            if (Mathf.Abs(speed - intendSpeed) > 0.02f) {
                speed += acceleration * Time.deltaTime;
            }
            if (Mathf.Abs(rotationSpeed - intendRotationSpeed) > 0.5f) {
                rotationSpeed += rotationAcceleration * Time.deltaTime;
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
        public Vector2 intendDFP;
        public float size;
        public float intendSize;
        public bool isFollowingPlayer;
        public bool isFollowingPlayerRotation;

        private Vector2 aIntendDFP;
        private float aIntendSize;

        public void CopyFrom(CameraStatus status) {
            deltaFocusPoint = status.deltaFocusPoint;
            intendDFP = status.intendDFP;
            size = status.size;
            intendSize = status.intendSize;
            aIntendDFP = status.aIntendDFP;
            aIntendSize = status.aIntendSize;
            isFollowingPlayer = status.isFollowingPlayer;
            isFollowingPlayerRotation = status.isFollowingPlayerRotation;
        }

        public void UpdateFromConverter(CameraStatusConverter converter) {
            intendDFP = converter.intendedDFP;
            intendSize = converter.intendedSize;
            aIntendDFP = (intendDFP - deltaFocusPoint) / converter.time;
            aIntendSize = (intendSize - size) / converter.time;
        }

        public void Update() {
            if (Vector2.Distance(deltaFocusPoint, intendDFP) > 0.05f) {
                deltaFocusPoint += aIntendDFP * Time.deltaTime;
            }
            if (Mathf.Abs(size - intendSize) > 0.1f) {
                size += aIntendSize * Time.deltaTime;
            }
        }
    }

    [Serializable]
    public struct CameraStatusConverter {
        public Vector2 intendedDFP;
        public float intendedSize;
        public float time; //   second
    }
}
