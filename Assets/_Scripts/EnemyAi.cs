using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    Transform player;

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position);
    }
}
