using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera MainCamera;
    public Transform target;
    public float smoothSpeed = 0.525f;
    public Vector3 offset;

    private bool Camera2D;
    private bool finishOrtho;
    private Vector3 topViewPos;
    private Quaternion topViewRot;

    private void Start() {
        topViewPos = new Vector3(0.0f,20.0f,0.0f);
        topViewRot = transform.rotation;
        Camera2D = true;
        finishOrtho = true;
        MainCamera = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
        if(Camera2D){
                Vector3 desiredPosition = target.position + offset;
                float changeCamera = Mathf.Lerp(MainCamera.orthographicSize,10.0f,0.015f);
                MainCamera.orthographicSize = changeCamera;
            if(transform.rotation != Quaternion.Euler(90.0f,0.0f,0.0f)){
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90.0f,0.0f,0.0f), smoothSpeed);
                transform.rotation = smoothedRotation;
                transform.position = smoothedPosition;
            }
            else{
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
        else{
            if(finishOrtho){
                Vector3 desiredPosition = new Vector3(-12.0f,11.0f,-22.0f);
                Quaternion desiredRotation = Quaternion.Euler(26.4f,30.0f,0.0f);
                float changeCamera = Mathf.Lerp(MainCamera.orthographicSize,24.0f,0.015f);
                MainCamera.orthographicSize = changeCamera;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation,desiredRotation, smoothSpeed);

                transform.position = smoothedPosition;
                transform.rotation = smoothedRotation;
            }
        }
    }

    public void SwitchCamera(){
        Camera2D = !Camera2D;
        finishOrtho = true;
    }
}