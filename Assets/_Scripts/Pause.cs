using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        agents = FindObjectsOfType<NavMeshAgent>().ToList();

        foreach (var item in FindObjectsOfType<CryptoBehaviour>())
        {
            scripts.Add(item);
        }

        scripts.Add(FindObjectOfType<PlayerController>());
        scripts.Add(FindObjectOfType<CameraController>());
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
