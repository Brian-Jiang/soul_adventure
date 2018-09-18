using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataTypes {

    public enum CurveStyle {
        Instantly, Linear, EaseInOut
    }

    [Serializable]
    public struct PlayerStatus {
        public float speed;
        public float rotationSpeed;
        
        private AnimationCurve speedCurve;
        private AnimationCurve rotationalSpeedCurve;

        private float timer;

        public void CopyFrom(PlayerStatus status) {
            speed = status.speed;
            rotationSpeed = status.rotationSpeed;
            speedCurve = new AnimationCurve(status.speedCurve.keys);
            rotationalSpeedCurve = new AnimationCurve(status.rotationalSpeedCurve.keys);
            timer = status.timer;
        }

        public void Init(PlayerStatus status) {
            ResetTimer();
            speed = status.speed;
            rotationSpeed = status.rotationSpeed;
            speedCurve = AnimationCurve.Constant(0f, 1f, speed);
            rotationalSpeedCurve = AnimationCurve.Constant(0f, 1f, rotationSpeed);
        }

        public void UpdateFromConverter(PlayerStatusConverter converter) {
            switch (converter.style) {
                case CurveStyle.Instantly:
                    speed = converter.intendedSpeed;
                    rotationSpeed = converter.intendedRotationalSpeed;
                    speedCurve = AnimationCurve.Constant(0f, 1f, speed);
                    rotationalSpeedCurve = AnimationCurve.Constant(0f, 1f, rotationSpeed);
                    break;
                case CurveStyle.Linear:
                    ResetTimer();
                    speedCurve = AnimationCurve.Linear(0f, speed, converter.time, converter.intendedSpeed);
                    rotationalSpeedCurve =
                        AnimationCurve.Linear(0f, rotationSpeed, converter.time, converter.intendedRotationalSpeed);
                    break;
                case CurveStyle.EaseInOut:
                    ResetTimer();
                    speedCurve = AnimationCurve.EaseInOut(0f, speed, converter.time, converter.intendedSpeed);
                    rotationalSpeedCurve =
                        AnimationCurve.EaseInOut(0f, rotationSpeed, converter.time, converter.intendedRotationalSpeed);
                    break;
            }

        }

        public void Update() {
            timer += Time.deltaTime;
            speed = speedCurve.Evaluate(timer);
            rotationSpeed = rotationalSpeedCurve.Evaluate(timer);
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
    }

    
    [Serializable]
    public struct CameraStatus {
        public Vector2 focusDelta;
        public float size;
        
        public bool hasTarget;
        public bool hasOrientation;

        public Vector2 currentTarget;
        public float currentOrientation;

        private AnimationCurve sizeCurve;
        private float toTargetMaxDelta;
        private float cameraMaxDelta;
        private float timer;

        public float GetCameraMaxDelta() {
            return cameraMaxDelta;
        }
        
        public float GetMaxDelta() {
            return toTargetMaxDelta;
        }

        public void Init(CameraStatus status) {
            ResetTimer();
            focusDelta = status.focusDelta;
            size = status.size;
            hasTarget = status.hasTarget;
            hasOrientation = status.hasOrientation;
            currentTarget = status.currentTarget;
            currentOrientation = status.currentOrientation;
            toTargetMaxDelta = 0.1f;
            cameraMaxDelta = 999f;
            sizeCurve = AnimationCurve.Constant(0f, 1f, size);
        }

        public void CopyFrom(CameraStatus status) {
            focusDelta = status.focusDelta;
            size = status.size;
            hasTarget = status.hasTarget;
            hasOrientation = status.hasOrientation;
            currentTarget = status.currentTarget;
            currentOrientation = status.currentOrientation;
            toTargetMaxDelta = status.toTargetMaxDelta;
            cameraMaxDelta = status.cameraMaxDelta;
            sizeCurve = status.sizeCurve;
            timer = status.timer;
        }

        public void UpdateFromConverter(CameraStatusConverter converter) {
            hasTarget = converter.willHaveTarget;
            hasOrientation = converter.willHaveOrientation;
            currentTarget = converter.nextTarget;
            currentOrientation = converter.nextOrientation;
            focusDelta = converter.intendedFocusDelta;
            toTargetMaxDelta = converter.toTargetMaxSpeed * Time.deltaTime;
            cameraMaxDelta = converter.cameraMaxSpeed * Time.deltaTime;
            switch (converter.sizeStyle) {
                case CurveStyle.Instantly:
                    size = converter.intendedSize;
                    sizeCurve = AnimationCurve.Constant(0f, 1f, size);
                    break;
                case CurveStyle.Linear:
                    ResetTimer();
                    sizeCurve = AnimationCurve.Linear(0f, size, converter.time, converter.intendedSize);
                    break;
                case CurveStyle.EaseInOut:
                    ResetTimer();
                    sizeCurve = AnimationCurve.EaseInOut(0f, size, converter.time, converter.intendedSize);
                    break;
            }
        }

        public void Update() {
            timer += Time.deltaTime;
            size = sizeCurve.Evaluate(timer);
        }
        
        private void ResetTimer() {
            timer = 0f;
        }
    }

    [Serializable]
    public struct CameraStatusConverter {
        public Vector2 intendedFocusDelta;
        public float intendedSize;
        public float time; //   second
        public CurveStyle sizeStyle;
        public bool willHaveTarget;
        public bool willHaveOrientation;

        public Vector2 nextTarget;
        public float nextOrientation;
        public float toTargetMaxSpeed;
        public float cameraMaxSpeed;
    }

    [Serializable]
    public struct PlayerSaveInfo {
        public Vector2 savePositionDelta;
        public float saveOrientationDelta;

        [HideInInspector]
        public Vector3 cameraPosition;
        [HideInInspector]
        public float cameraOrientation;

        private bool active;
        private PlayerStatus playerStatus;
        private CameraStatus cameraStatus;
//        private Transform cameraTrans;

        public void SavePlayer(PlayerStatus status) {
            playerStatus.CopyFrom(status);
            active = true;
        }

        public void SaveCamera(CameraStatus status, GameObject camera) {
            cameraStatus.CopyFrom(status);
            cameraPosition = camera.transform.position;
            cameraOrientation = camera.transform.rotation.eulerAngles.z;
//            cameraTrans = camera.transform;
        }

        public void Reset() {
            active = false;
        }

        public bool IsActive() {
            return active;
        }
        
        public PlayerStatus GetPlayerStatus() {
            return playerStatus;
        }
        
        public CameraStatus GetCameraStatus() {
            return cameraStatus;
        }

//        public Transform GetCameraTrans() {
//            return cameraTrans;
//        }
    }
}
