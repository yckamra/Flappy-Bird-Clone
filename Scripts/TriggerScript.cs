using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public LogicManagerScript logicScript;
    public BoxCollider2D theBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(theBoxCollider.enabled)
        {
            logicScript.addScore();
        }
        theBoxCollider.enabled = false;
    }
}
