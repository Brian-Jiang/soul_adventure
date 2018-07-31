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
            if (Mathf.Abs(rotationSpeed - intendRotationSpeed) > 1f) {
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

        private float timeD;
        private float deltaDFP;
        private float deltaSize;
        private Vector2 aIntendDFP;
        private float aIntendSize;

        public void CopyFrom(CameraStatus status) {
            deltaFocusPoint = status.deltaFocusPoint;
            intendDFP = status.intendDFP;
            size = status.size;
            intendSize = status.intendSize;
            isFollowingPlayer = status.isFollowingPlayer;
            isFollowingPlayerRotation = status.isFollowingPlayerRotation;
        }

        public void UpdateFromConverter(CameraStatusConverter converter) {
            intendDFP = converter.intendedDFP;
            intendSize = converter.intendedSize;
            timeD = converter.time;
        }

        public void Update() {
            if (!Mathf.Approximately(timeD, 0f)) {
                float disDFP = Vector2.Distance(deltaFocusPoint, intendDFP);
//                Debug.Log("distance: " + disDFP);
                deltaDFP = disDFP / timeD * Time.deltaTime * 2f; // * multiplier
                deltaFocusPoint = Vector2.MoveTowards(deltaFocusPoint, intendDFP, deltaDFP);
//                Debug.Log("current DFP: " + deltaFocusPoint);

                float disSize = Mathf.Abs(size - intendSize);
                deltaSize = disSize / timeD * Time.deltaTime * 2f; // * multiplier
                size = Mathf.MoveTowards(size, intendSize, deltaSize);
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
