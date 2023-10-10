using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private float currentShootSpeedRate = 1f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootSpeedRate = 2;
    [SerializeField] private float fireSpeed;
    [SerializeField] private float chargeTime = 6;
    [SerializeField] private float bulletDamage;
    [SerializeField] private int maxBullets = 20;
    [SerializeField] private Text currenBulletsText;
    [SerializeField] private Transform firePoint;
    private int currrentBulets;
    private float currentChargeTime;
    private Vector3 lookTarget;
    private bool isAmmo;
    private Animator anim;
    private Camera gameCamera;
    [SerializeField] DamageDealier damageDealier;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound, shootNoAmmo, rechrgeAmmoSound;
    public bool IsShooting = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameCamera = Camera.main;
        currrentBulets = maxBullets;
        currenBulletsText.text = currrentBulets.ToString();
        isAmmo = true;
        damageDealier.SetDamage(bulletDamage);
    }
    private void LookTargetControl()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameCamera.ScreenPointToRay(Input.mousePosition), out hit))
            lookTarget = hit.point;
        lookTarget.y = transform.position.y;
        transform.LookAt(lookTarget);
    }

    void Update()
    {
        LookTargetControl();
        LunchBullet();
    }
    private void LunchBullet()
    {
        if (shootSpeedRate < currentShootSpeedRate)
            shootSpeedRate += Time.deltaTime;

        if (isAmmo == false)
            currentChargeTime += Time.deltaTime;

        if (currentChargeTime >= chargeTime)
        {
            isAmmo = true;
            currrentBulets = maxBullets;
            currentChargeTime = 0;
            audioSource.PlayOneShot(rechrgeAmmoSound);
            currenBulletsText.text = currrentBulets.ToString();
        }

        if (IsShooting & shootSpeedRate > currentShootSpeedRate & isAmmo == true)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(shootSound);
            currrentBulets--;
            shootSpeedRate = 0;
            anim.SetTrigger("shoot");
            Vector3 direction = lookTarget - firePoint.transform.position;
            GameObject currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
            currentBullet.transform.LookAt(lookTarget);
            Rigidbody currentArrowVelocity = currentBullet.GetComponent<Rigidbody>();
            currentArrowVelocity.AddForce(fireSpeed * direction.normalized, ForceMode.Impulse);
            Destroy(currentBullet, 2);
            currenBulletsText.text = currrentBulets.ToString();
            if (currrentBulets == 0)
            {
                isAmmo = false;
                currentChargeTime = 0;
                audioSource.PlayOneShot(shootNoAmmo);
            }
        }
        if (IsShooting & shootSpeedRate > currentShootSpeedRate & isAmmo == false)
        {
            audioSource.PlayOneShot(shootNoAmmo);
        }
    }
    //private void LunchBullet()
    //{
    //    if (shootSpeedRate < currentShootSpeedRate)
    //        shootSpeedRate += Time.deltaTime;

    //    if (isAmmo == false)
    //        currentChargeTime += Time.deltaTime;

    //    if (currentChargeTime >= chargeTime)
    //    {
    //        isAmmo = true;
    //        currrentBulets = maxBullets;
    //        currentChargeTime = 0;
    //        audioSource.PlayOneShot(rechrgeAmmoSound);
    //        currenBulletsText.text = currrentBulets.ToString();
    //    }

    //    if (Input.GetMouseButton(0) & shootSpeedRate > currentShootSpeedRate & isAmmo == true)
    //    {
    //        audioSource.pitch = Random.Range(0.8f, 1.2f);
    //        audioSource.PlayOneShot(shootSound);
    //        currrentBulets--;
    //        shootSpeedRate = 0;
    //        anim.SetTrigger("shoot");
    //        Vector3 direction = lookTarget - firePoint.transform.position;
    //        GameObject currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
    //        currentBullet.transform.LookAt(lookTarget);
    //        Rigidbody currentArrowVelocity = currentBullet.GetComponent<Rigidbody>();
    //        currentArrowVelocity.AddForce(fireSpeed * direction.normalized, ForceMode.Impulse);
    //        Destroy(currentBullet, 2);
    //        currenBulletsText.text = currrentBulets.ToString();
    //        if (currrentBulets == 0)
    //        {
    //            isAmmo = false;
    //            currentChargeTime = 0;
    //            audioSource.PlayOneShot(shootNoAmmo);
    //        }
    //    }
    //    if (Input.GetMouseButtonDown(0) & shootSpeedRate > currentShootSpeedRate & isAmmo == false)
    //    {
    //        audioSource.PlayOneShot(shootNoAmmo);
    //    }
    //}

    public void IncreaseAttackSpeed()
    {
        currentShootSpeedRate = currentShootSpeedRate - 0.05f;
        fireSpeed = fireSpeed * 1.1f;
    }

    public void IncreaseAttackDamage()
    {
        bulletDamage = bulletDamage * 1.1f;
        damageDealier.SetDamage(bulletDamage);
    }

    public void IncreaseAmmo()
    {
        maxBullets = maxBullets + 10;
        chargeTime = chargeTime * 0.95f;
    }
}
