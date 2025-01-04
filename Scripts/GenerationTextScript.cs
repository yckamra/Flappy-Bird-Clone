using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerationTextScript : MonoBehaviour
{
    void Start()
    {
        if(GameManagerScript.Instance != null)
            GameManagerScript.Instance.setTextMeshProToNew(gameObject.GetComponent<TextMeshProUGUI>());
    }
}
