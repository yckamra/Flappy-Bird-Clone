using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworkAgentScript
{
    public NeuralNetworkAgentScript theParentsScript;
    public List<NeuronLayerScript> layers;
    

    public NeuralNetworkAgentScript(NeuralNetworkAgentScript theParentsScript, bool mutate)//NOT DONE
    {
        this.theParentsScript = theParentsScript;
        if (theParentsScript != null)// TODO: not implemented
        {
            if (mutate)
            {
                layers = new List<NeuronLayerScript>();
                NeuronLayerScript hiddenLayer = new NeuronLayerScript(theParentsScript.layers[0], true, 5, 6, "Relu");
                NeuronLayerScript outputLayer = new NeuronLayerScript(theParentsScript.layers[1], true, 6, 1, "Sig");
                layers.Add(hiddenLayer);
                layers.Add(outputLayer);
            }
            else
            {
                layers = new List<NeuronLayerScript>();
                NeuronLayerScript hiddenLayer = new NeuronLayerScript(theParentsScript.layers[0], false, 5, 6, "Relu");
                NeuronLayerScript outputLayer = new NeuronLayerScript(theParentsScript.layers[1], false, 6, 1, "Sig");
                layers.Add(hiddenLayer);
                layers.Add(outputLayer);
            }
            
        }
        else
        {
            layers = new List<NeuronLayerScript>();
            NeuronLayerScript hiddenLayer = new NeuronLayerScript(null, false, 5, 6, "Relu");
            NeuronLayerScript outputLayer = new NeuronLayerScript(null, false, 6, 1, "Sig");
            layers.Add(hiddenLayer);
            layers.Add(outputLayer);
        }
        
    }

    public bool flap(float xPositionOfPipe, float yTopOfPipe, float yBottomOfPipe, float birdYPosition, float yVelocityOfBird) // DONE
    {

        List<float> inputs = new List<float>();

        inputs.Add(MeanNormalization(xPositionOfPipe));
        inputs.Add(MeanNormalization(yTopOfPipe));
        inputs.Add(MeanNormalization(yBottomOfPipe));
        inputs.Add(MeanNormalization(birdYPosition));
        inputs.Add(MeanNormalization(yVelocityOfBird));

        
        for(int i = 0; i < layers.Count; i++)
        {
            List<float> inputsForNextLayer = layers[i].outputFromLayer(inputs);
            inputs = inputsForNextLayer;
        }
        if (inputs[0] > 0.5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float MeanNormalization(float input)
    {
        return input;
    }

    public void printNetwork()
    {
        
       Debug.Log(layers[0].neurons[0].parameters[0]);

       /*if (layers != null)
        {
            for (int j = 0; j < layers.Count; j++)
            {
                if (layers[j] != null)
                {
                    Debug.Log("---In layer " + j +"---");

                    if (layers[j].neurons != null)
                    {
                        Debug.Log("---Neurons in this layer: " + layers[j].neurons.Count + "---");
                        for (int i = 0; i < layers[j].neurons.Count; i++)
                        {
                            Debug.Log("---In neuron: " + i + "---");
                            if (layers[j].neurons[i] != null)
                            {
                                if (layers[j].neurons[i].parameters != null)
                                {
                                    for (int k = 0; k < layers[j].neurons[i].parameters.Count; k++)
                                    {
                                        Debug.Log(layers[j].neurons[i].parameters[k]);
                                    }
                                }
                                else
                                {
                                    Debug.Log("WARNING: current parameters is NULL");
                                }
                            }
                            else
                            {
                                Debug.Log("WARNING: current neuron is NULL");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("WARNING: current layer does not have neurons (is NULL)");
                    }
                }
                else
                {
                    Debug.Log("WARNING: current layer is NULL");
                }
            }
        }
        else
        {
            Debug.Log("WARNING: layers do not exist (is NULL)");
        }*/
    }
}
