
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class AG : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI generationText;
    [SerializeField] private TextMeshProUGUI fitnessText;
    [SerializeField] private List<EvolutionaryChar> charList;
    private List<float> fitnessValues = new List<float>();

    private string filePath;
    private string fileName = "fitness.txt";

    private float mutationAmount = 5f;
    private int generation = 0;

    private const int NUM_ACTIONS = 20;
    private float timeBeforeReset = 3f;
    private float timer = 0f;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        initPop();
        generationText.text = "Generation: " + generation;
        fitnessText.text = "Fitness: 0";
        filePath = Application.dataPath + "/" + fileName;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if(timer >= timeBeforeReset / NUM_ACTIONS)
        {
            // changes the action of the joints
            if(index < NUM_ACTIONS)
            {
                Time.timeScale = 0f;
                foreach (EvolutionaryChar character in charList)
                {
                    // saves the height of the character
                    character.heights.Add(character.Height());

                    foreach (var joint in character.joints)
                    {
                        joint.resetJointMovement();
                        joint.changeJointMovement(joint.actions[index]);
                    }
                }

                index++;
                timer = 0f;
                Time.timeScale = 1f;
            }
            // evolutionary step
            else
            {
                Time.timeScale = 0f;
                index = 0;
                foreach (EvolutionaryChar character in charList)
                {
                    fitnessValues.Add(fitnessFunction(character));
                }
                Elitism();
                generation++;
                generationText.text = "Generation: " + generation;
                resetSimulation();
            }
        }
    }

    // initializes the population with random values
    private void initPop()
    {
        fitnessValues.Clear();

        foreach (var character in charList)
        {
            foreach (var joint in character.joints)
            {
                joint.actions.Clear();

                for (int i = 0; i < NUM_ACTIONS; i++)
                {
                    joint.actions.Add(Random.Range(-100f, 100f));
                }
            }
        }   
    }

    // fitness function
    // values the maximum height of the character
    private float fitnessFunction(EvolutionaryChar character)
    {
        float fitness = 0f;

        for(int i = 0; i<character.heights.Count; i++)
        {
            fitness += (float)character.heights[i] * (i+1); // values later heights more
        }

        return fitness;
    }

    // Basic elistism implementation
    private void Elitism()
    {
        float maxFitness = fitnessValues[0];
        int indexMaxFitness = 0;

        for (int i = 1; i < fitnessValues.Count; i++)
        {
            // new max fitness
            if (fitnessValues[i] > maxFitness)
            {
                maxFitness = fitnessValues[i];
                indexMaxFitness = i;
                mutationAmount = 5f;
            }
        }

        if (mutationAmount > 250f)
        {
            mutationAmount = 5f; // reset mutation
        }

        using (StreamWriter writer = File.AppendText(filePath))
        {
            writer.WriteLine(maxFitness.ToString());
        }
        fitnessText.text = "Fitness: " + maxFitness;

        for (int i = 0; i < charList.Count; i++)
        {
            if (i != indexMaxFitness)
            {
                Debug.Log("Crossover");
                // crossover
                for (int j = 0; j < NUM_ACTIONS; j++)
                {
                    charList[i].headJoint.actions[j] = (charList[i].headJoint.actions[j] + charList[indexMaxFitness].headJoint.actions[j]) / 2;
                    charList[i].leftArmJoint.actions[j] = (charList[i].leftArmJoint.actions[j] + charList[indexMaxFitness].leftArmJoint.actions[j]) / 2;
                    charList[i].leftForearmJoint.actions[j] = (charList[i].leftForearmJoint.actions[j] + charList[indexMaxFitness].leftForearmJoint.actions[j]) / 2;
                    charList[i].rightArmJoint.actions[j] = (charList[i].rightArmJoint.actions[j] + charList[indexMaxFitness].rightArmJoint.actions[j]) / 2;
                    charList[i].rightForearmJoint.actions[j] = (charList[i].rightForearmJoint.actions[j] + charList[indexMaxFitness].rightForearmJoint.actions[j]) / 2;
                    charList[i].leftLegJoint.actions[j] = (charList[i].leftLegJoint.actions[j] + charList[indexMaxFitness].leftLegJoint.actions[j]) / 2;
                    charList[i].leftThighJoint.actions[j] = (charList[i].leftThighJoint.actions[j] + charList[indexMaxFitness].leftThighJoint.actions[j]) / 2;
                    charList[i].rightLegJoint.actions[j] = (charList[i].rightLegJoint.actions[j] + charList[indexMaxFitness].rightLegJoint.actions[j]) / 2;
                    charList[i].rightThighJoint.actions[j] = (charList[i].rightThighJoint.actions[j] + charList[indexMaxFitness].rightThighJoint.actions[j]) / 2;

                    // mutation
                    charList[i].headJoint.actions[j] += Random.Range(-mutationAmount, mutationAmount);
                    charList[i].leftArmJoint.actions[j] += Random.Range(-mutationAmount, mutationAmount);
                    charList[i].leftForearmJoint.actions[j] += Random.Range(-mutationAmount, mutationAmount);
                    charList[i].rightArmJoint.actions[j] += Random.Range(-mutationAmount, mutationAmount);
                    charList[i].rightForearmJoint.actions[j] += Random.Range(-mutationAmount, mutationAmount);
                    charList[i].leftLegJoint.actions[j] += Random.Range(-mutationAmount, mutationAmount);
                    charList[i].leftThighJoint.actions[j] += Random.Range(-mutationAmount, mutationAmount);
                    charList[i].rightLegJoint.actions[j] += Random.Range(-mutationAmount, mutationAmount);
                    charList[i].rightThighJoint.actions[j] += Random.Range(-mutationAmount, mutationAmount);
                }
            }
        }

        // increases mutation amount
        mutationAmount += 10f;
    }

    // restarts the characters positions and velocities
    private void resetSimulation()
    {
        timer = 0f;
        fitnessValues.Clear();
        foreach (EvolutionaryChar character in charList)
        {
            character.ResetLists();
            character.ResetChildrenPositions();
            character.ResetJoints();
            foreach (var joint in character.joints)
            {
                joint.ResetRigidbody();
            }
        }
        Time.timeScale = 1f;
    }
}
