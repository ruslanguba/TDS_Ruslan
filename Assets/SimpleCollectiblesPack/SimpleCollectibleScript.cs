using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {SilverCoin, GoldCoin, LittleHealth, BigHealth, Heart, Gem}; 
	public CollectibleTypes CollectibleType; 
	public bool rotate; 
	public float rotationSpeed;
	public AudioClip collectSound;
	public GameObject collectEffect;
	[SerializeField] private ParticleSystem CollectEffect;

	private Health health;
	private GameManager GM;
	private MoneyController MoneyController;
	private int littleHeal = 25, bigHeal = 50, heart = 20, silver = 25, gold = 50;
	private float gem = 100;

	[SerializeField] private AudioSource audioSource;

	void Start () {
		health = FindObjectOfType<Health>();
		GM = FindObjectOfType<GameManager>();
        MoneyController = FindObjectOfType<MoneyController>();
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
	    {
			Collect ();
		}
	}

	public void Collect()
	{
		if(collectSound)
			audioSource.PlayOneShot(collectSound);
		if (collectEffect)
		{
			CollectEffect.Play();
		}

		
		if (CollectibleType == CollectibleTypes.SilverCoin) 
		{
            MoneyController.CollectMoney(silver);
		}
		if (CollectibleType == CollectibleTypes.GoldCoin) 
		{
            MoneyController.CollectMoney(gold);
		}
		if (CollectibleType == CollectibleTypes.LittleHealth) {

			health.HealthColected(littleHeal);
		}
		if (CollectibleType == CollectibleTypes.BigHealth) {

			health.HealthColected(bigHeal);
		}
		if (CollectibleType == CollectibleTypes.Heart) {

			health.MaxHealthCollect(heart);
		}
		if (CollectibleType == CollectibleTypes.Gem) {

			GM.CollectExpiriens(gem);
		}

		Destroy(gameObject, 0.2f);

	}
}
