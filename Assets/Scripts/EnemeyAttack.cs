using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyAttack : MonoBehaviour
{
    [SerializeField] private float damageDistance;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float damage;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip champ;
    [SerializeField] private Animator anim;
    private GameObject playerObject;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        playerObject = GetComponent<EnemeyMovement>().playerObject;
    }

    public void SetStartDamage(float EnemeyLVL)
    {
        damage = damage * (1 + EnemeyLVL / 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackZone"))
        {
            StartCoroutine(AttackCorutine());
        }
    }

    IEnumerator AttackCorutine()
    {
        yield return new WaitForSeconds(attackSpeed);
        float AttackDistance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (AttackDistance < damageDistance)
        {
            playerObject.GetComponent<Health>().TakeDamage(damage);
            audioSource.PlayOneShot(champ);
            anim.SetTrigger("Attack 02");
        }
        yield return new WaitForSeconds(attackSpeed * 2);
        float NewAttackDistance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (NewAttackDistance < damageDistance)
            StartCoroutine(AttackCorutine());
    }
}
