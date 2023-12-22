using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoint : MonoBehaviour
{
    public int actionIndex = 0;
    public List<float> actions = new List<float>();

    public float timer = 0f;

    private HingeJoint2D hingeJoint2D;
    private Rigidbody2D rigidbody2D;

    private float initialRotation = 0f;

    void Awake()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        initialRotation = rigidbody2D.rotation;
        resetJointMovement();
    }

    public void changeJointMovement(float speed)
    {
        JointMotor2D motor = hingeJoint2D.motor;
        motor.motorSpeed = speed;
        hingeJoint2D.motor = motor;
    }

    public void resetJointMovement()
    {
        JointMotor2D motor = hingeJoint2D.motor;
        motor.motorSpeed = 0f;
        hingeJoint2D.motor = motor;
    }

    public void ResetRigidbody()
    {
        rigidbody2D.rotation = initialRotation;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.angularVelocity = 0f;
    }
}
