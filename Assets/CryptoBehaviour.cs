using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE,
    RUN,
    JUMP,
    KICK
}

public class CryptoBehaviour : MonoBehaviour
{
    public GameObject Player;
    bool hasLOS;
    Vector3 player;
    Animator anim;
    NavMeshAgent agent;

    [Header("Attack")]
    public float distance;
    public PlayerController playerC;
    public float damageDelay = 1.0f;
    public float kickForce;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerC = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {

        player = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, GameObject.Find("Player").transform.position.z);

        if (hasLOS)
        {
            agent.SetDestination(player);
            anim.SetInteger("AnimState", (int)CryptoState.RUN);

            if (Vector3.Distance(player, transform.position) < distance)
            {
                transform.LookAt(player - new Vector3(0f, 1.381f, 0f));

                anim.SetInteger("AnimState", (int)CryptoState.KICK);

                if (Time.frameCount % 60 == 0)
                {
                    StartCoroutine(Kick());
                }

                if (agent.isOnOffMeshLink)
                {
                    anim.SetInteger("AnimState", (int)CryptoState.JUMP);
                }
            }
        }
        else if (hasLOS)
        {
            anim.SetInteger("AnimState", (int)CryptoState.RUN);
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
    IEnumerator Kick()
    {

        yield return new WaitForSeconds(damageDelay);

        var direction = Vector3.Normalize(Player.transform.position - transform.position);
        PlayerController.controller.SimpleMove(transform.position + Vector3.forward * kickForce);
        playerC.TakeDamage(20);


        StopCoroutine(Kick());
    }
}
