using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance; // Singleton pattern

    public int generation = 0;
    public TextMeshProUGUI generationText;
    public List<NeuralNetworkAgentScript> winningParents;
    public int numberWinners;
    public NeuralNetworkAgentScript topBirdSoFar;
    public float topScoreSoFar = 0;
    public int generationCounter = 1;

    void Start()
    {


        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Update()
    {
        generationText.text = generation.ToString();
    }
    public void addGeneration()
    {
        this.generation += 1;
        generationText.text = generation.ToString();
    }
    public void setTextMeshProToNew(TextMeshProUGUI text)
    {
        this.generationText = text;
    }

    public void AddParentsToWinningParents(List<GameObject> theParents)
    {

        winningParents = new List<NeuralNetworkAgentScript>();

        for(int i = 0; i < this.numberWinners; i++)
        {
            GameObject parent = theParents[i];
            NeuralNetworkAgentScript theAI = parent.GetComponent<AIBirdScript>().theAgentScript;
            winningParents.Add(theAI);
        }
    }
}
