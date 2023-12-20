using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoint : MonoBehaviour
{
    private float timeBeforeReset = 5f;

    private int actionIndex = 0;
    public List<float> actions = new List<float>();

    private float timer = 10000f;

    private SpriteRenderer spriteRenderer;
    private HingeJoint2D hingeJoint2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hingeJoint2D = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBeforeReset/actions.Count && actionIndex < actions.Count)
        {
            changeJointMovement(actions[actionIndex]);
            actionIndex++;
            timer = 0f;
        }
    }

    private void changeJointMovement(float speed)
    {
        JointMotor2D motor = hingeJoint2D.motor;
        motor.motorSpeed = speed;
        hingeJoint2D.motor = motor;
    }
}
