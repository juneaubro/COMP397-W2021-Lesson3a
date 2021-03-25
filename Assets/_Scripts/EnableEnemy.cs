using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnableEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnableNavMeshAgent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnableNavMeshAgent()
    {
        yield return new WaitForSeconds(delay);
        agent.enabled = true;
        StopCoroutine(EnableNavMeshAgent());
    }
}
