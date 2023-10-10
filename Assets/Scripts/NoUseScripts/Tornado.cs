using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tornado : MonoBehaviour
{
    [SerializeField] private Vector3 targetVector;
    [SerializeField] private float speed;
    [SerializeField] private float startMaxFlyTime;
    private float tornadoLVL = 1;
    private float tornadoForceLVL;
    private float maxFlyTime;

    void Start()
    {
        startMaxFlyTime = 3;
        maxFlyTime = startMaxFlyTime;
        targetVector = new Vector3(transform.position.x + Random.Range(-6, 6), transform.position.y, transform.position.z + Random.Range(-6, 6));
    }

    void Update()
    {
        TornadoMove();
    }

    private void TornadoMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetVector, Time.deltaTime * speed);
        float dist = Vector3.Distance(transform.position, targetVector);
        if (dist < 0.5f)
            targetVector = new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y, transform.position.z + Random.Range(-3, 3));
    }

    public void IncreaseTornadoForceLVL()
    {
        ++tornadoForceLVL;
        maxFlyTime = startMaxFlyTime + tornadoForceLVL;
    }

    public void IncreaseLVL()
    {
        tornadoLVL++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemeyMovement>() != null)
            other.GetComponent<EnemeyMovement>().OnTornado(maxFlyTime);
    }
}
