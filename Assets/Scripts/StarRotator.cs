using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotator : MonoBehaviour
{
    [SerializeField] private float currentDamage;
    [SerializeField] private float startDamage;
    [SerializeField] private int metalStarDamageLVL;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shuriken;

    private void Start()
    {
        startDamage = 10;
        currentDamage = startDamage + metalStarDamageLVL;
    }

    void Update()
    {
        transform.Rotate(0, 10, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemey"))
        {
            other.GetComponent<EnemeyHealth>().TakeDamage(currentDamage);
            audioSource.PlayOneShot(shuriken);
        }
    }

    public void IncreaseStarLVL()
    {
        metalStarDamageLVL++;
        currentDamage = currentDamage + metalStarDamageLVL;
    }
}
