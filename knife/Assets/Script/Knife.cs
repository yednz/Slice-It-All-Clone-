using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Rigidbody rb;
    public float force = 5f;
    public float torque = 20f;


    private RigidbodyConstraints orginalRigibodyConstraints;

    private float lastTimePressedButton;
    private bool freezePosZ = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        orginalRigibodyConstraints = rb.constraints;
    }
    void Start()
    {
        lastTimePressedButton = Time.time;
        StartCoroutine(dumpClear());
    }


    void Update()
    {
        switch (GameManager.GetInstance().state)
        {
            case GameManager.State.WaitToStart:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.isKinematic = false;
                    Jump();
                    GameManager.GetInstance().state = GameManager.State.Playing;
                }
                break;
            case GameManager.State.Playing:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                break;
            case GameManager.State.Dead:
                break;
            case GameManager.State.Win:
                break;
            default:
                break;
        }
        float angle = transform.localEulerAngles.x;
        angle = (angle > 180) ? angle - 360 : angle;


        if (angle > 70 && angle < 120)
        {
            rb.angularDrag = 10f;
        }
        else
        {
            rb.angularDrag = 0.05f;
        }



        if (Time.time >= lastTimePressedButton + 0.8f && freezePosZ)
        {
            lastTimePressedButton = Time.time;
            FreezePosZ();
            freezePosZ = false;
        }
    }

    private void Jump()
    {
        rb.constraints = orginalRigibodyConstraints;

        rb.AddForceAtPosition(force * new Vector3(0, 0.6f, 0.075f),transform.position, ForceMode.Impulse);
        rb.AddTorque(torque, 0f, 0f, ForceMode.Impulse);

        lastTimePressedButton = Time.time;
        freezePosZ = true;
    }
    private void FreezePosZ()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "wood")
        {
            coll.gameObject.GetComponent<BoxCollider>().enabled = false;
            coll.gameObject.transform.parent = GameManager.GetInstance().dump;

            Rigidbody wood0rb = coll.gameObject.transform.GetChild(0).GetComponent<Rigidbody>();
            Rigidbody wood1rb = coll.gameObject.transform.GetChild(1).GetComponent<Rigidbody>();

            wood0rb.isKinematic = false;
            wood0rb.AddForce(2f * new Vector3(1f, 0f, 0f), ForceMode.Impulse);
            wood0rb.AddTorque(0f, 0f, -2f, ForceMode.Impulse);

            wood1rb.isKinematic = false;
            wood1rb.AddForce(2f * new Vector3(-1f, 0f, 0f), ForceMode.Impulse);
            wood1rb.AddTorque(0f, 0f, 2f, ForceMode.Impulse);


            rb.AddForce(0.25f * Vector2.down, ForceMode.Impulse);
        }
    }


    IEnumerator dumpClear()
    {
        yield return new WaitForSeconds(50f);

        for (int i = 0; i < GameManager.GetInstance().dump.childCount; i++)
        {
            Destroy(GameManager.GetInstance().dump.GetChild(i).gameObject);
        }
        StartCoroutine(dumpClear());
    }
}
