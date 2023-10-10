using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField, Range(0, 2)] private float StartMoveSpeed = 0.8f; /*currentShootSpeedRate = 1f;*/
    private float currentMoveSpeed;
    [SerializeField] private GameObject bullet;
    private Vector3 movement, LookTarget;
    private Animator anim;
    private Rigidbody playerRigitbody;
    //private float ShootSpeedRate = 2, fireSpeed = 10, currentChargeTime;
    [SerializeField] float ChargeTime = 6, bulletDamage;
    [SerializeField] private int MaxBullets = 20;
    private int currrentBulets;
    public Text currenBulletsText;
    [SerializeField] private Transform firePoint;
    private bool isAmmo;
    private bool freez;
    private Camera _camera;
    [SerializeField] DamageDealier damageDealier;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound, shootNoAmmo, rechrgeAmmoSound;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerRigitbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        _camera = Camera.main;
        currrentBulets = MaxBullets;
        currenBulletsText.text = currrentBulets.ToString();
        isAmmo = true;
        damageDealier.SetDamage(bulletDamage);
        currentMoveSpeed = StartMoveSpeed;
        freez = false;
    }

    void Update()
    {
        PlayerInputAxeises();
        //LookTargetControl();
        //LunchBullet();
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    private void PlayerInputAxeises()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical).normalized;
    }

    public void MoveCharacter(Vector3 movement)
    {
        playerRigitbody.AddForce(movement * currentMoveSpeed, ForceMode.VelocityChange);
        anim.SetFloat("Velocity", movement.magnitude);
    }

    //private void LookTargetControl()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
    //        LookTarget = hit.point;
    //    LookTarget.y = transform.position.y;
    //    transform.LookAt(LookTarget);
    //}

    //private void LunchBullet()
    //{
    //    if (ShootSpeedRate < currentShootSpeedRate)
    //        ShootSpeedRate += Time.deltaTime;
    //    if (isAmmo == false)
    //        currentChargeTime += Time.deltaTime;
    //    if (currentChargeTime >= ChargeTime)
    //    {
    //        isAmmo = true;
    //        currrentBulets = MaxBullets;
    //        currentChargeTime = 0;
    //        audioSource.PlayOneShot(rechrgeAmmoSound);
    //        currenBulletsText.text = currrentBulets.ToString();
    //    }

    //    if (Input.GetMouseButton(0) & ShootSpeedRate > currentShootSpeedRate & isAmmo == true)
    //    {
    //        audioSource.pitch = Random.Range(0.8f, 1.2f);
    //        audioSource.PlayOneShot(shootSound);
    //        currrentBulets--;
    //        ShootSpeedRate = 0;
    //        anim.SetTrigger("shoot");
    //        Vector3 direction = LookTarget - firePoint.transform.position;
    //        GameObject currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
    //        currentBullet.transform.LookAt(LookTarget);
    //        Rigidbody currentArrowVelocity = currentBullet.GetComponent<Rigidbody>();
    //        currentBullet.GetComponent<Rigidbody>().AddForce(fireSpeed * direction.normalized, ForceMode.Impulse);
    //        Destroy(currentBullet, 2f);
    //        currenBulletsText.text = currrentBulets.ToString();
    //        if (currrentBulets == 0)
    //        {
    //            isAmmo = false;
    //            currentChargeTime = 0;
    //            audioSource.PlayOneShot(shootNoAmmo);
    //        }
    //    }
    //    if(Input.GetMouseButtonDown(0) & ShootSpeedRate > currentShootSpeedRate & isAmmo == false)
    //    {
    //        audioSource.PlayOneShot(shootNoAmmo);
    //    }
    //}

    //public void IncreaseAttackSpeed()
    //{
    //    currentShootSpeedRate = currentShootSpeedRate - 0.05f;
    //    fireSpeed = fireSpeed * 1.1f;
    //}

    //public void IncreaseAttackDamage()
    //{
    //    bulletDamage = bulletDamage * 1.1f;
    //    damageDealier.SetDamage(bulletDamage);
    //}

    //public void IncreaseAmmo()
    //{
    //    MaxBullets = MaxBullets + 10;
    //    ChargeTime = ChargeTime * 0.95f;
    //}

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
        currentMoveSpeed = StartMoveSpeed;
        freez = false;
    }
}



