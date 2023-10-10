using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deathPunnel;
    [SerializeField] private GameObject scorePunnel;
    [SerializeField] private Text healthIndicatorText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private PlayerMovements playerMovements;
    [SerializeField] private PlayerShooter playerShooter;
    private float currentHealth;
    private Animator anim;
    private GameManager gameManagerClass;

    private void Awake()
    {
        currentHealth = maxHealth;
        gameManagerClass = FindObjectOfType<GameManager>();
        anim = GetComponentInChildren<Animator>();
        playerMovements = GetComponent<PlayerMovements>();
        playerShooter = GetComponent<PlayerShooter>();
        healthIndicatorText.text = Mathf.Round(currentHealth).ToString();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckIsAlive();
        healthIndicatorText.text = Mathf.Round(currentHealth).ToString();
    }

    private void CheckIsAlive()
    {
        if (currentHealth <= 0)
            Death();
    }

    private void Death()
    {
        playerMovements.enabled = false;
        playerShooter.enabled = false;
        Debug.Log("Death");
        anim.SetTrigger("Death");
        StartCoroutine(routine: DeathCorutine());
        audioSource.PlayOneShot(deathSound);
    }

    public void HealthColected(int health)
    {
        currentHealth += health;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        healthIndicatorText.text = Mathf.Round(currentHealth).ToString();
    }

    public void MaxHealthCollect(int maxHeathColected)
    {
        maxHealth += maxHeathColected;
    }

    IEnumerator DeathCorutine()
    {
        yield return new WaitForSeconds(2);
        gameManagerClass.CheckScore();
        deathPunnel.SetActive(true);
        scorePunnel.SetActive(true);
        Time.timeScale = 0;
    }
}
