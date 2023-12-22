
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AG : MonoBehaviour
{
    [SerializeField] private List<EvolutionaryChar> charList;
    private List<float> fitnessValues = new List<float>();

    private float mutationAmount = 5f;

    private const int NUM_ACTIONS = 20;
    private float timeBeforeReset = 3f;
    private float timer = 0f;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        initPop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if(timer >= timeBeforeReset / NUM_ACTIONS)
        {
            if(index < NUM_ACTIONS)
            {
                Time.timeScale = 0f;
                foreach (EvolutionaryChar character in charList)
                {
                    character.heights.Add(character.Height());
                    character.distances.Add(character.Distance());

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
            else
            {
                Time.timeScale = 0f;
                index = 0;
                foreach (EvolutionaryChar character in charList)
                {
                    fitnessValues.Add(fitnessFunction(character));
                }
                Elitism();
                resetSimulation();
            }
        }
    }

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

    private float fitnessFunction(EvolutionaryChar character)
    {
        float fitness = 0f;
        foreach (var height in character.heights)
        {
            fitness += (float)height;
        }

        /*for (int i = 0; i < character.distances.Count; i++)
        {
            fitness += 2000 / Mathf.Pow((float)character.distances[i] + 1, 2); // valoriza velocidade;
        }*/

        return fitness;
    }

    private void Elitism()
    {
        float maxFitness = fitnessValues[0];
        int indexMaxFitness = 0;

        for (int i = 1; i < fitnessValues.Count; i++)
        {
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

        Debug.Log("Max Fitness: " + maxFitness);
        Debug.Log("Index Max Fitness: " + indexMaxFitness);

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

        mutationAmount += 10f;
    }

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
