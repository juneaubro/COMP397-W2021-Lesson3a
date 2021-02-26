using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE,
    RUN,
    JUMP
}

public class CryptoBehaviour : MonoBehaviour
{
    bool hasLOS;
    Vector3 player;
    Animator anim;
    NavMeshAgent agent;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {

        player = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, GameObject.Find("Player").transform.position.z);

        if (hasLOS)
        {
            agent.SetDestination(player);
            anim.SetInteger("AnimState", (int)CryptoState.RUN);

            if(Vector3.Distance(player,transform.position) < 3.5)
            {
                anim.SetInteger("AnimState", (int)CryptoState.IDLE);
                transform.LookAt(player - new Vector3(0f, 1.381f,0f));
            }
        }
        else
        {
            anim.SetInteger("AnimState", (int)CryptoState.IDLE);
        }

        //if ()
        //{
        //    anim.SetInteger("AnimState", (int)CryptoState.JUMP);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            hasLOS = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasLOS = false;
        }
    }
}
