using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoint : MonoBehaviour
{
    public int actionIndex = 0;
    public List<float> actions = new List<float>();

    public float timer = 0f;

    private HingeJoint2D hingeJoint2D;

    void Awake()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>();
    }

    public void changeJointMovement(float speed)
    {
        JointMotor2D motor = hingeJoint2D.motor;
        motor.motorSpeed = speed;
        hingeJoint2D.motor = motor;
    }
}
