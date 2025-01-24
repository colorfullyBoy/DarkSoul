using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTrans;
    public Transform cameraTrans;
    public Transform cameraPivotTrans;
    private Transform myTrans;
    private LayerMask ignoreLayers;
    public float lookSpeed = 0.1f;
    //与其说是速度不如说缓动时间
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;
    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;
    public float minPivot = -35;
    public float maxPivot = 35;

    public static CameraHandler instance;

    private void Awake() {
        CameraHandler.instance = this;
        myTrans = this.transform;
        defaultPosition = cameraTrans.localPosition.z;
        //用来忽略指定层级碰撞的
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
    }

    public void followTarget(float dt){
        Vector3 targetPosition = Vector3.Lerp(myTrans.position, targetTrans.position, dt/ followSpeed);
        myTrans.position = targetPosition;
    }

    public void handlerCameraRotation(float dt, float mouseX, float mouseY){
        lookAngle += mouseX * lookSpeed/ dt;
        pivotAngle -= mouseY * pivotSpeed / dt;
        pivotAngle = Math.Clamp(pivotAngle, minPivot, maxPivot);
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTrans.rotation = targetRotation;
        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTrans.localRotation = targetRotation;
    }
}
