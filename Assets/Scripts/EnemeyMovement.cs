using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemeyMovement : MonoBehaviour
{
    public GameObject playerObject;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float normalMoveSpeed;
    [SerializeField] private float  currentMoveSpeed;
    [SerializeField] private Animator anim;
    [SerializeField] private bool fastEnemey;
    [SerializeField] private GameObject freezeEffect;
    [SerializeField] private GameObject targetPoint;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        if (fastEnemey == false)
            anim.SetBool("Walk Forward", true);
        if (fastEnemey == true)
            anim.SetBool("Run Forward", true);
    }

    void Update()
    {
        MoveToPlaeyr();
    }

    public void SetStartSpeed(float EnemeyLVL)
    {
        currentMoveSpeed = normalMoveSpeed + EnemeyLVL / 10;
        agent.speed = currentMoveSpeed;
    }

    public void SetPlayerObject(GameObject player)
    {
        playerObject = player;
    }

    public void MoveToTarget()
    {
        agent.destination = targetPoint.transform.position;
    }

    private void MoveToPlaeyr()
    {
        agent.destination = playerObject.transform.position;
    }

    public void OffIce()
    {
        agent.speed = normalMoveSpeed;
        freezeEffect.SetActive(false);
    }

    public void OnIce(float freezForce)
    {
        agent.speed = normalMoveSpeed * freezForce;
        freezeEffect.SetActive(true);
    }

    private void OffTornado()
    {
        agent.destination = playerObject.transform.position;
        agent.baseOffset = 0;
        agent.speed = normalMoveSpeed;
    }

    public void OnTornado(float MaxFlyTime)
    {
        agent.speed = 0;
        agent.baseOffset = 2;
        agent.destination = transform.position;
        Invoke("OffTornado", MaxFlyTime);
    }

    public void Die()
    {
        agent.speed  = 0;
    }
}
