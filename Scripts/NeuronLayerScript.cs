using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronLayerScript
{
    public List<NeuronScript> neurons;
    public int numberOfNeurons;
    public int numberOfInputs;
    public bool mutate;
    public NeuronLayerScript parentsLayer;
    public string activationFunction;

    public NeuronLayerScript(NeuronLayerScript parentsLayer, bool mutate, int numberOfInputs, int numberOfNeurons, string activationFunction) // DONE
    {

        this.numberOfInputs = numberOfInputs;
        this.numberOfNeurons = numberOfNeurons;
        this.activationFunction = activationFunction;

        if(parentsLayer != null)
        {
            if (mutate) // has parent and we are mutating
            {
                neurons = new List<NeuronScript>();
                this.parentsLayer = parentsLayer;
                this.mutate = mutate;
                mutateParentsNeurons(parentsLayer);
            }
            else // has parent but we are not mutating (making a copy)
            {
                neurons = parentsLayer.neurons;
                this.parentsLayer = parentsLayer;
                this.mutate = mutate;
            }
        }
        else // no parent and creating a completely new network
        {
            this.parentsLayer = null;
            this.mutate = false;
            neurons = new List<NeuronScript>();
            createNewLayerOfNeurons();
        }
    }

    public void mutateParentsNeurons(NeuronLayerScript parentsLayer) // DONE
    {
        for(int i = 0; i < numberOfNeurons; i++)
        {
            NeuronScript aNeuron = new NeuronScript(parentsLayer.neurons[i], true, numberOfInputs, parentsLayer.neurons[i].activationFunction, numberOfNeurons);
            neurons.Add(aNeuron);
        }
    }

    public void createNewLayerOfNeurons() // DONE
    {
        for(int i = 0; i < numberOfNeurons; i++)
        {
            NeuronScript newNeuron = new NeuronScript(null, false, this.numberOfInputs, this.activationFunction, this.numberOfNeurons);
            neurons.Add(newNeuron);
        }
    }
    public List<float> outputFromLayer(List<float> inputs) // DONE
    {
        List<float> outputs = new List<float>();
        for(int i = 0; i < numberOfNeurons; i++)
        {
            outputs.Add(neurons[i].outputFromNeuron(inputs));
        }
        return outputs;
    }
}
