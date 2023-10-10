using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealier : MonoBehaviour
{
    [SerializeField] private float damage;
    // Почему если сделать его не SerializeField оно становится ноль и не изменяется из класса игрока?
    [SerializeField] GameObject bloodSplashPrefab;

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.CompareTag("Enemey"))
        collision.gameObject.GetComponent<EnemeyHealth>().TakeDamage(damage);
        Destroy(gameObject);
    }

    public void SetDamage(float bulletDamage)
    {
        damage = bulletDamage;
    }
}
