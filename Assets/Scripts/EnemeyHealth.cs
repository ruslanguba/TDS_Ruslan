using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float expiriens;
    [SerializeField] private GameObject bloodDie;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private BoxCollider trigger;
    [SerializeField] private GameObject[] bonus;
    [SerializeField] private AudioClip hitSound1, hitSound2, deathSound;
    [SerializeField] private ParticleSystem bloodSpray;
    private int bonusType;
    private EnemeyMovement enemeyMovementClass;
    private GameManager gameManagerClass;
    private PlaerLevel plaerLevelClass;
    private Animator anim;
    private AudioSource audioSource;
    
    private void Awake()
    {
        enemeyMovementClass = GetComponent<EnemeyMovement>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetGameManager(GameManager gameManager)
    {
        gameManagerClass = gameManager;
    }

    public void SetPlayerLevel(PlaerLevel plaerLevel)
    {
        plaerLevelClass = plaerLevel;
    }

    public void SetStarStats(float EnemeyLVL )
    {
        maxHealth = maxHealth * (1 + EnemeyLVL / 5);
        transform.localScale = transform.localScale * (1 + EnemeyLVL / 20);
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        ShowDamage.Instance.AddText((int)damage, transform.position);
        bloodSpray.Play();
        anim.SetTrigger("Take Damage");
        PlayHitSound();
        CheckIsAlive();
    }

    private void CheckIsAlive()
    {
        if (currentHealth <= 0)
            Death();
    }

    private void Death()
    {
        boxCollider.enabled = false; // удаляю коллайдер чтобы при повторном попадании не издавал звуков и не создавал доп бонусов
        Destroy(trigger); // удаляю триггер чтобы Rotator не начислял опыт с мертвого врага
        plaerLevelClass.CollectExpiriens(expiriens);
        gameManagerClass.EnemeyKilled();
        anim.SetTrigger("Die");
        PlayDeathSound();
        GameObject BloodStream = Instantiate(bloodDie, transform.position, Quaternion.identity);
        RandomBonus();
        enemeyMovementClass.Die();
        Destroy(gameObject, 1);
        Destroy(BloodStream, 2);
    }

    private void PlayDeathSound()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(deathSound);
    }

    private void PlayHitSound()
    {
        int r = Random.Range(0, 2);
        if (r == 1)
            audioSource.PlayOneShot(hitSound2);
        else
            audioSource.PlayOneShot(hitSound1);
    }
    
    private void RandomBonus()
    {
        int randomVelue = Random.Range(0, 100);
        if (randomVelue < 60)
        {
            if (randomVelue < 2)
            {
                bonusType = 5;
            }
            else if (randomVelue < 4)
            {
                bonusType = 4;
            }
            else if (randomVelue < 10)
            {
                bonusType = 3;
            }
            else if (randomVelue < 15)
            {
                bonusType = 2;
            }
            else if (randomVelue < 30)
            {
                bonusType = 1;
            }
            else if (randomVelue < 60)
            {
                bonusType = 0;
            }
            GameObject BonusIteam = Instantiate(bonus[bonusType], transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
