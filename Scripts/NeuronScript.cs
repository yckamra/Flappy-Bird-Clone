using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronScript
{
    public string activationFunction; // "Relu" or "Sig"
    public List<float> parameters = new List<float>();
    public float bias;
    public int inputNumber;
    public bool mutated;
    public int neuronsInThisLayer;
    public float mutationDeviation = 0.1f;

    public NeuronScript(NeuronScript parentsNeuron, bool mutate, int inputNumber, string activationFunction, int neuronsInThisLayer) //DONE
    {
        this.activationFunction = activationFunction;
        this.inputNumber = inputNumber;
        this.neuronsInThisLayer = neuronsInThisLayer;

        if(parentsNeuron != null)
        {
            if (mutate)
            {
                mutateParentsParametersAndBias(parentsNeuron.parameters, parentsNeuron.bias);
            
            }
            else
            {
                parameters = parentsNeuron.parameters;
                bias = parentsNeuron.bias;
            }
        }
        else
        {
            createAndStoreRandomParametersAndBias();
        }
    }

    public float outputFromNeuron(List<float> input) // DONE
    {
        float sum = 0;

        for(int i = 0; i < parameters.Count; i++)
        {
            sum += (parameters[i] * input[i]);
        }

        sum += bias;

        if(activationFunction == "Relu")
        {
            sum = NNFunctions.Relu(sum);
        }else if(activationFunction == "Sig")
        {
            sum = NNFunctions.Sig(sum);
        }
        else
        {
            // unknown activation function
            Debug.Log("activation function not found");
        }

        return sum;
    }

    public void mutateParentsParametersAndBias(List<float> parentsParameters, float parentsBias) //TODO: NOT DONE
    {
        for(int i = 0; i < parentsParameters.Count; i++)
        {
            float rand = Random.Range(0.0f, this.mutationDeviation);
            int randNeg = Random.Range(0, 2);
            if(randNeg == 0)
            {
                parameters.Add(parentsParameters[i] + rand);


            }
            else
            {
                parameters.Add(parentsParameters[i] - rand);

            }
        }
        bias = parentsBias;
    }

    public void createAndStoreRandomParametersAndBias() //DONE
    {
        bias = 0;
        for(int i = 0; i < inputNumber; i++)
        {
            parameters.Add(NNFunctions.randomGausNum());
        }
    }

}
