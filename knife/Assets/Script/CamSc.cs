using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSc : MonoBehaviour
{
    private Transform knife;
    private Vector3 camPosDif;
    void Start()
    {
        knife = GameManager.GetInstance().knife;

        camPosDif = transform.position - knife.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = knife.transform.position + camPosDif;
    }
}
