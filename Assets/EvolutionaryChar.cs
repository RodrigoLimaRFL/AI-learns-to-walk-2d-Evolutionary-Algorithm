using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionaryChar : MonoBehaviour
{
    [SerializeField] private MoveJoint headJoint;
    [SerializeField] private MoveJoint leftArmJoint;
    [SerializeField] private MoveJoint leftForearmJoint;
    [SerializeField] private MoveJoint rightArmJoint;
    [SerializeField] private MoveJoint rightForearmJoint;
    [SerializeField] private MoveJoint leftLegJoint;
    [SerializeField] private MoveJoint leftThighJoint;
    [SerializeField] private MoveJoint rightLegJoint;
    [SerializeField] private MoveJoint rightThighJoint;

    private const int NUM_ACTIONS = 20;

    [SerializeField] private Transform cabeca;

    private List<float> heights = new List<float>();
    private float[] distances = new float[NUM_ACTIONS];

    private float timeBeforeReset = 5f;

    private float[] headJointActions = new float[NUM_ACTIONS];
    private float[] leftArmJointActions = new float[NUM_ACTIONS];
    private float[] leftForearmJointActions = new float[NUM_ACTIONS];
    private float[] rightArmJointActions = new float[NUM_ACTIONS];
    private float[] rightForearmJointActions = new float[NUM_ACTIONS];
    private float[] leftLegJointActions = new float[NUM_ACTIONS];
    private float[] leftThighJointActions = new float[NUM_ACTIONS];
    private float[] rightLegJointActions = new float[NUM_ACTIONS];
    private float[] rightThighJointActions = new float[NUM_ACTIONS];

    private float timer = 0f;

    void Start()
    {
        for (int i = 0; i < NUM_ACTIONS; i++)
        {
            // Temp
            headJointActions[i] = Random.Range(-100f, 100f);
            leftArmJointActions[i] = Random.Range(-100f, 100f);
            leftForearmJointActions[i] = Random.Range(-100f, 100f);
            rightArmJointActions[i] = Random.Range(-100f, 100f);
            rightForearmJointActions[i] = Random.Range(-100f, 100f);
            leftLegJointActions[i] = Random.Range(-100f, 100f);
            leftThighJointActions[i] = Random.Range(-100f, 100f);
            rightLegJointActions[i] = Random.Range(-100f, 100f);
            rightThighJointActions[i] = Random.Range(-100f, 100f);

            headJoint.actions.Add(headJointActions[i]);
            leftArmJoint.actions.Add(leftArmJointActions[i]);
            leftForearmJoint.actions.Add(leftForearmJointActions[i]);
            rightArmJoint.actions.Add(rightArmJointActions[i]);
            rightForearmJoint.actions.Add(rightForearmJointActions[i]);
            leftLegJoint.actions.Add(leftLegJointActions[i]);
            leftThighJoint.actions.Add(leftThighJointActions[i]);
            rightLegJoint.actions.Add(rightLegJointActions[i]);
            rightThighJoint.actions.Add(rightThighJointActions[i]);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBeforeReset/NUM_ACTIONS)
        {
            if (cabeca != null)
            {
                // Get the height of the sprite
                heights.Add(cabeca.position.y);

                // Print or use the objectHeight as needed
                Debug.Log("Object Height: " + cabeca.position.y);
            }
        }
    }
}
