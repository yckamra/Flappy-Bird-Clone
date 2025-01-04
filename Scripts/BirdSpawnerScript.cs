using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawnerScript : MonoBehaviour
{
    public GameObject AIbird;
    public int birdsToSpawn;
    public GameObject thePipeSpawner;
    public List<GameObject> subscribedTo;
    public List<GameObject> birdsAlive;
    public LogicManagerScript theLogicManager;
    public int numberOfBirdsToMoveOn = 5;

    public List<NeuralNetworkAgentScript> winners;

    // Start is called before the first frame update
    void Start()
    {
        setNumberWinners();
        geneticAlgorithmAndCreateBirds();


    }

    // Update is called once per frame
    void Update()
    {
        if (birdsAlive.Count != 0)
        {

        }
        else
        {
            theLogicManager.gameOverForNN();
        }



    }

    void setNumberWinners()
    {
        if (GameManagerScript.Instance != null)
        {
            GameManagerScript.Instance.numberWinners = numberOfBirdsToMoveOn;
            Debug.Log(GameManagerScript.Instance.numberWinners);
        }
    }

    void geneticAlgorithmAndCreateBirds()
    {
    
        if (GameManagerScript.Instance.winningParents != null)
        {

            List<NeuralNetworkAgentScript> winningParents = GameManagerScript.Instance.winningParents;

            int thirdOfBirds = birdsToSpawn / 3;
            int fourthOfBirds = birdsToSpawn / 4;
            int birdsMade = 0;

            if(GameManagerScript.Instance.generationCounter >= 1) // 50
            {
                GameManagerScript.Instance.generationCounter = 0; // 49

                for (int i = 0; i < 10; i++)
                {
                    spawnBird(null, false);
                    birdsMade += 1;
                }

                spawnBird(winningParents[0], false);
                spawnBird(winningParents[1], false);
                birdsMade += 1;
                birdsMade += 1;
                spawnBird(GameManagerScript.Instance.topBirdSoFar, false);
                birdsMade += 1;

                for (int i = 0; i < thirdOfBirds; i++)
                {
                    spawnBird(winningParents[0], true);
                    birdsMade += 1;
                }
                for (int i = 0; i < thirdOfBirds; i++)
                {
                    spawnBird(winningParents[1], true);
                    birdsMade += 1;
                }
                for (int i = 0; i < birdsToSpawn - birdsMade; i++)
                {
                    spawnBird(CrossOverParentGenetics(winningParents[0], winningParents[1]), true);
                }
            }
            else
            {
                spawnBird(winningParents[0], false);
                spawnBird(winningParents[1], false);
                birdsMade += 1;
                birdsMade += 1;
                spawnBird(GameManagerScript.Instance.topBirdSoFar, false);
                birdsMade += 1;

                for (int i = 0; i < thirdOfBirds; i++)
                {
                    spawnBird(winningParents[0], true);
                    birdsMade += 1;
                }
                for (int i = 0; i < thirdOfBirds; i++)
                {
                    spawnBird(winningParents[1], true);
                    birdsMade += 1;
                }
                for (int i = 0; i < birdsToSpawn - birdsMade; i++)
                {
                    spawnBird(CrossOverParentGenetics(winningParents[0], winningParents[1]), true);
                }
            }

        }
        else
        {
            for (int i = 0; i < birdsToSpawn; i++) // if first generation just create random birds
            {
                spawnBird(null, false);
            }
        }
    }

    private NeuralNetworkAgentScript CrossOverParentGenetics(NeuralNetworkAgentScript parent1, NeuralNetworkAgentScript parent2)
    {
        NeuralNetworkAgentScript child = new NeuralNetworkAgentScript(parent1, false);

        for(int i = 0; i < child.layers.Count; i++) // i is the layer
        {
            for(int j = 0; j < child.layers[i].neurons.Count; j++) // j is the neuron
            {
                for(int k = 0; k < child.layers[i].neurons[j].parameters.Count; k++) // k is the parameter
                {
                    int geneticNum = Random.Range(0, 2);
                    if(geneticNum == 0) // parameter is parent1
                    {
                        // do nothing as child is already a clone of parent1
                    }
                    else // parameter is parent2
                    {
                        child.layers[i].neurons[j].parameters[k] = parent2.layers[i].neurons[j].parameters[k];
                    }
                }
            }
        }
        return child;
    }

    void spawnBird(NeuralNetworkAgentScript parentAIScript, bool mutate)
    {
        GameObject newBird = Instantiate(AIbird, new Vector3(0, 0, -1), transform.rotation);
        AIBirdScript birdScript = newBird.GetComponent<AIBirdScript>();
        birdScript.thePipeSpawner = thePipeSpawner;
        subscribedTo.Add(newBird);
        birdsAlive.Add(newBird);

        if (parentAIScript != null)
        {
            if (mutate)
            {
                birdScript.theAgentScript = new NeuralNetworkAgentScript(parentAIScript, true);
            }
            else
            {
                birdScript.theAgentScript = new NeuralNetworkAgentScript(parentAIScript, false);
            }
        }
        else
        {
            birdScript.theAgentScript = new NeuralNetworkAgentScript(null, false);

        }
        
    }

    public void removeFromAliveBirdList(GameObject theBirdToRemove)
    {
        birdsAlive.Remove(theBirdToRemove);
    }

    // The sorting function
    public void QuickSortFromFitness(List<GameObject> list, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(list, low, high);
            QuickSortFromFitness(list, low, pivotIndex - 1);
            QuickSortFromFitness(list, pivotIndex + 1, high);
        }
    }

    // The partition function
    private int Partition(List<GameObject> list, int low, int high)
    {
        float pivotValue = list[high].GetComponent<AIBirdScript>().fitness; // Pivot is the fitness of the high index GameObject
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (list[j].GetComponent<AIBirdScript>().fitness < pivotValue)
            {
                i++;
                Swap(list, i, j);
            }
        }
        Swap(list, i + 1, high);
        return i + 1;
    }

    // Swap function
    private void Swap(List<GameObject> list, int index1, int index2)
    {
        GameObject temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }
}
