using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceRain : MonoBehaviour
{
    [SerializeField] private float startFreezeForce = 0.9f;
    private float currentFreezeForce, FreezeForceLVL;
    private int IceRainLVL; // в будущем додавить урон в секунду при высоком уровне

    private void Awake()
    {
        currentFreezeForce = startFreezeForce;
    }

    public void IncreaseIceRainForceLVL()
    {
        FreezeForceLVL++;
        currentFreezeForce = startFreezeForce * (1 - FreezeForceLVL / 10);
        IceRainLVL++;
    }

    public void IncreaseIceRainLVL()
    {
        IceRainLVL++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemey"))
        {
            other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed * currentFreezeForce;
            //other.GetComponent<EnemeyMovement>().FindRain();
        }
    }
}
