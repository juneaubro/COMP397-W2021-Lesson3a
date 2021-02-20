using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pause : MonoBehaviour
{
    public bool isGamePaused;
    public List<MonoBehaviour> scripts;
    public List<NavMeshAgent> agents;

    private void Start()
    {
        isGamePaused = false;
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        foreach(var script in scripts)
        {
            script.enabled = !isGamePaused;
        }

        foreach (var agent in agents)
        {
            agent.enabled = !isGamePaused;
        }
    }
}
