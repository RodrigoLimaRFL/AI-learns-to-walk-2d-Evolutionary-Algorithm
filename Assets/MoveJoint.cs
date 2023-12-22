using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoint : MonoBehaviour
{
    public int actionIndex = 0;
    public List<float> actions = new List<float>();

    public float timer = 0f;

    private HingeJoint2D hingeJoint2D;
    private Rigidbody2D rb2D;

    private float initialRotation = 0f;

    void Awake()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>();
        rb2D = GetComponent<Rigidbody2D>();
        initialRotation = rb2D.rotation;
        resetJointMovement();
    }

    // Change the speed of the joint
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
        rb2D.rotation = initialRotation;
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0f;
    }
}
