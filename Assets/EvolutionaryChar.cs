using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EvolutionaryChar : MonoBehaviour
{
    [SerializeField] public MoveJoint headJoint;
    [SerializeField] public MoveJoint leftArmJoint;
    [SerializeField] public MoveJoint leftForearmJoint;
    [SerializeField] public MoveJoint rightArmJoint;
    [SerializeField] public MoveJoint rightForearmJoint;
    [SerializeField] public MoveJoint leftLegJoint;
    [SerializeField] public MoveJoint leftThighJoint;
    [SerializeField] public MoveJoint rightLegJoint;
    [SerializeField] public MoveJoint rightThighJoint;

    public MoveJoint[] joints = new MoveJoint[9];

    private Vector3[] originalPositions;
    private Quaternion[] originalRotations;

    [SerializeField] private Transform floor;
    [SerializeField] private Transform flag;

    [SerializeField] private Transform cabeca;

    public List<float> heights = new List<float>();
    public List<float> distances = new List<float>();

    void Awake()
    {
        // Get the number of child objects
        int childCount = transform.childCount;

        // Initialize the array to store original positions
        originalPositions = new Vector3[childCount];
        originalRotations = new Quaternion[childCount];

        // Store original local positions
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            originalPositions[i] = child.localPosition;
            originalRotations[i] = child.localRotation;
        }

        joints[0] = headJoint;
        joints[1] = leftArmJoint;
        joints[2] = leftForearmJoint;
        joints[3] = rightArmJoint;
        joints[4] = rightForearmJoint;
        joints[5] = leftLegJoint;
        joints[6] = leftThighJoint;
        joints[7] = rightLegJoint;
        joints[8] = rightThighJoint;
    }

    public float Height()
    {
        return Mathf.Abs(cabeca.position.y - floor.position.y);
    }

    public float Distance()
    {
        return Mathf.Abs(flag.position.x - cabeca.position.x);
    }

    public void ResetLists()
    {
        heights.Clear();
        distances.Clear();
    }

    public void ResetChildrenPositions()
    {
        // Get the number of child objects
        int childCount = transform.childCount;

        // Reset local positions to their original positions
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.localPosition = originalPositions[i];
            child.localRotation = originalRotations[i];
        }
    }
}
