using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private PlayerMovements pm;
    private PlayerShooter shooter;
    private Vector3 movement;
    void Start()
    {
        pm = GetComponent<PlayerMovements>();
        shooter = GetComponent<PlayerShooter>();
    }

    private void FixedUpdate()
    {
        PlayerInputAxeises();
        pm.MoveCharacter(movement);
        Shoot();
    }

    private void PlayerInputAxeises()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical).normalized;
    }

    private void Shoot()
    {
        if(Input.GetMouseButton(0))
        {
            shooter.IsShooting = true;
        }
        else shooter.IsShooting = false;
    }
}
