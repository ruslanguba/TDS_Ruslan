using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorPllayerFollow : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        transform.position = player.transform.position;
    }

    void LateUpdate()
    {
        transform.Rotate(0, 2, 0);
        MoveRotator();
    }
    private void MoveRotator()
    {
        transform.position = player.transform.position;
    }
}
