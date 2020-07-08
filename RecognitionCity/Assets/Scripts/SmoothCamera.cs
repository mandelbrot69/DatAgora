﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform center;
    public Vector3 axis = Vector3.up;
    public float radius = 2.0f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;

    float rot_duration = 4.0f;
    Vector3 desiredPosition;
    public bool isStarting = false;
    Quaternion final_rot;


    IEnumerator rotateOBJ()
    {
        final_rot = final_rot * Quaternion.Euler(0, 0, 180);
        float rot_elapsedTime = 0.0F;
        while (rot_elapsedTime < rot_duration)
        {
            transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
            desiredPosition = (transform.position - center.position).normalized * radius + center.position;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
            rot_elapsedTime += Time.deltaTime * 0.25f;
            yield return null;
        }
        isStarting = false;
        yield return null;  
    }

    void Update()
    {
        if (isStarting) StartCoroutine("rotateOBJ");
    }

    public void StartCameraMovment(Transform target)
    {
        center = target;
        transform.position = (transform.position - center.position).normalized * radius + center.position;
        isStarting = true;
    }
}



