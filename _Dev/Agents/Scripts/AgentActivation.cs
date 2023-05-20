using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentActivation : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        /*AgentController agentController = other.GetComponent<AgentController>();
        agentController.enabled = true;
        agentController.ScriptEnabled();*/
        Crowder crowderController = other.GetComponent<Crowder>();
        if (crowderController)
        {
            crowderController.enabled = true;
            crowderController.ScriptEnabled();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /*AgentController agentController = other.GetComponent<AgentController>();
        agentController.ScriptDisabled();
        agentController.enabled = false;*/
        Crowder crowderController = other.GetComponent<Crowder>();
        if (crowderController)
        {
            crowderController.ScriptDisabled();
            crowderController.enabled = false;
        }

    }
}
