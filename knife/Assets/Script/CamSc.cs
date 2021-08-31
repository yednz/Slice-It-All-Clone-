using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSc : MonoBehaviour
{
    private Transform target;
    private Vector3 desiredPos;
    public Vector3 offset;

    [Range(0.0f, 1.0f)] public float smoothSpeed;
    [Range(-1.0f, 1.0f)] public float targetDiff;

    void Start()
    {
        target = GameManager.GetInstance().knife;
    }

    private void FixedUpdate()
    { 
        desiredPos = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.LookAt(target.position + new Vector3(0, 1, 1) * targetDiff);
    }
}
