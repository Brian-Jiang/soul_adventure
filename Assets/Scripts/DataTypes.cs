using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataTypes {
    public enum colliderType {
        Player, Wall, Point
    }

    public enum CurveStyle {
        Instantly, Linear, EaseInOut
    }

    [Serializable]
    public struct PlayerStatus {
        public float speed;
//        public float intendSpeed;
        public float rotationSpeed;
//        public float intendRotationSpeed;
        
//        private float acceleration; //  unit/second
//        private float rotationAcceleration;
        private AnimationCurve speedCurve;
        private AnimationCurve rotationalSpeedCurve;

        private float timer;

        public void CopyFrom(PlayerStatus status) {
            speed = status.speed;
//            intendSpeed = status.intendSpeed;
//            intendRotationSpeed = status.intendRotationSpeed;
//            acceleration = status.acceleration;
//            rotationAcceleration = status.rotationAcceleration;
            rotationSpeed = status.rotationSpeed;
        }

        public void UpdateFromConverter(PlayerStatusConverter converter) {
//            if (converter.instantly) {
//                speed = intendSpeed = converter.intendedSpeed;
//                rotationSpeed = intendRotationSpeed = converter.intendedRotationalSpeed;
//                acceleration = 0f;
//                rotationAcceleration = 0f;
                speed = converter.intendedSpeed;
                rotationSpeed = converter.intendedRotationalSpeed;
                return;
//            }
//            intendSpeed = converter.intendedSpeed;
//            intendRotationSpeed = converter.intendedRotationalSpeed;
//            acceleration = (intendSpeed - speed) / converter.time;
//            rotationAcceleration = (intendRotationSpeed - rotationSpeed) / converter.time;
            
//            ResetTimer();
//            speedCurve = converter.speedCurve;
//            rotationalSpeedCurve = converter.rotationalSpeedCurve;
        }

        public void Update() {
//            if (Mathf.Abs(speed - intendSpeed) > 0.03f) {
//                speed += acceleration * Time.deltaTime;
//            } else {
//                speed = intendSpeed;
//                acceleration = 0f;
//            }
//
//            if (Mathf.Abs(rotationSpeed - intendRotationSpeed) > 1f) {
//                rotationSpeed += rotationAcceleration * Time.deltaTime;
//            } else {
//                rotationSpeed = intendRotationSpeed;
//                rotationAcceleration = 0f;
//            }


//            timer += Time.deltaTime;
//
//            speed = speedCurve.Evaluate(timer);
//            rotationSpeed = rotationalSpeedCurve.Evaluate(timer);
        }

        private void ResetTimer() {
            timer = 0f;
        }
    }

    [Serializable]
    public struct PlayerStatusConverter {
        public float intendedSpeed;
        public float intendedRotationalSpeed;
        public float time; //   second
        public CurveStyle style;
//        public AnimationCurve speedChange;
//        public AnimationCurve rotationalSpeedChange;
        
    }

    
    [Serializable]
    public struct CameraStatus {
        public Vector2 deltaFocusPoint;
        public Vector2 intendDFP;
        public float size;
        public float intendSize;
        public bool hasTarget;
        public bool hasOrientation;

        public Vector2 currentTarget;
        public float currentOrientation;

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
            hasTarget = status.hasTarget;
            hasOrientation = status.hasOrientation;
        }

        public void UpdateFromConverter(CameraStatusConverter converter) {
            hasTarget = converter.willHaveTarget;
            hasOrientation = converter.willHaveOrientation;
            currentTarget = converter.nextTarget;
            currentOrientation = converter.nextOrientation;
            if (converter.instantly) {
                deltaFocusPoint = intendDFP = converter.intendedDFP;
                size = intendSize = converter.intendedSize;
                aIntendDFP = new Vector2(0f, 0f);
                aIntendSize = 0f;
                return;
            }
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
        public bool willHaveTarget;
        public bool willHaveOrientation;
        public bool instantly;

        public Vector2 nextTarget;
        public float nextOrientation;
    }

    [Serializable]
    public struct PlayerSaveInfo {
        public Vector2 savePositionDelta;
        public float saveOrientation;

        private bool active;
        private PlayerStatus saveStatus;

        public void Save(PlayerStatus status) {
            saveStatus.CopyFrom(status);
            active = true;
        }

        public void Reset() {
            active = false;
        }

        public bool IsActive() {
            return active;
        }
    }
}
