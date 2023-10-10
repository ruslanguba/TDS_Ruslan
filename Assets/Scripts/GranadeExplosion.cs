using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GranadeExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem smallExplosion;
    [SerializeField] private ParticleSystem bigExplosion;
    [SerializeField] private float currentDamage;
    [SerializeField] private int granadeCurrentLVL;
    [SerializeField] SphereCollider damageZone;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip explosionSound;

    public void SetGranadeStats(int currentLvl, int damageLvl, int scaleLlvl)
    {
        granadeCurrentLVL = currentLvl;
        currentDamage = 50 * damageLvl;
        damageZone.radius = scaleLlvl;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(granadeCurrentLVL <= 3)
        {
            ParticleSystem boom = Instantiate(smallExplosion, transform.position, Quaternion.AngleAxis(0, Vector3.up));
            damageZone.enabled = true;
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(boom.gameObject, 1f);
            Destroy(gameObject, 1.1f);
        }
        else
        {
            ParticleSystem boom = Instantiate(bigExplosion, transform.position, Quaternion.AngleAxis(0, Vector3.up));
            damageZone.enabled = true;
            Destroy(GetComponent<CapsuleCollider>());
            Destroy(boom.gameObject, 1f);
            Destroy(gameObject, 1.1f);
        }
        audioSource.PlayOneShot(explosionSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemey"))
            other.GetComponent<EnemeyHealth>().TakeDamage(currentDamage);
    }
}
