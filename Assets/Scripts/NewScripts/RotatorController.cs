using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorController : MonoBehaviour
{
    [SerializeField] private GameObject rotator;
    [SerializeField] private GameObject player;
    [SerializeField] private float startRotatorActiveTime;
    [SerializeField] private float rotatorChargeTime;
    private float currentRotatorActiveTime;
    private int rotatorTimeLVL = 1;

    void Start()
    {
        transform.position = player.transform.position;
        currentRotatorActiveTime = startRotatorActiveTime;
    }

    void LateUpdate()
    {
        MoveRotator();
    }

    public void ActivateRotator()
    {
        StartCoroutine(RotatorTimer());
    }

    private void MoveRotator()
    {
        transform.position = player.transform.position;
        transform.Rotate(0, 1, 0);
    }

    IEnumerator RotatorTimer()
    {
        rotator.SetActive(true);
        yield return new WaitForSeconds(currentRotatorActiveTime);
        rotator.SetActive(false);
        yield return new WaitForSeconds(rotatorChargeTime -  currentRotatorActiveTime);
        StartCoroutine(RotatorTimer());
    }

    public void IncreaseRotatorTimeLVL()
    {
        rotatorTimeLVL++;
        currentRotatorActiveTime = currentRotatorActiveTime + rotatorTimeLVL;
    }
}
