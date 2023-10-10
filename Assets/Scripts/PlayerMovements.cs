using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float startMoveSpeed = 0.5f;
    private float currentMoveSpeed;
    private Animator anim;
    private Rigidbody playerRigitbody;
    private bool freez;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerRigitbody = GetComponent<Rigidbody>();
        currentMoveSpeed = startMoveSpeed;
        freez = false;
    }

    public void MoveCharacter(Vector3 movement)
    {
        playerRigitbody.AddForce(movement * currentMoveSpeed, ForceMode.VelocityChange);
        anim.SetFloat("Velocity", movement.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Freez"))
        {
            if (freez == false)
            {
                freez = true;
                currentMoveSpeed = currentMoveSpeed * 0.8f;
                Invoke("FreezOff", 2);
            }
        }
    }

    private void FreezOff()
    {
        currentMoveSpeed = startMoveSpeed;
        freez = false;
    }
}
