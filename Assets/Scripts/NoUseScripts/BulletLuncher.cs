using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival.Inputs {
    public class BulletLuncher : MonoBehaviour
    {
        private PlayerInput PI;

        [SerializeField] Camera _camera;
        [SerializeField] private Transform firePoint, fireTarget;
        [SerializeField] private GameObject bullet;
        [SerializeField] private float fireSpeed = 1;
        private AudioSource audioSource;
        [SerializeField] private AudioClip stepSound, shootSound;
            

        private Transform FireTarget;
        private Vector3 fireTargetVector;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            PI = GetComponentInParent<PlayerInput>();
            _camera = Camera.main;
        }

        void Update()
        {
            FireTargetControl();
        }
        private void FireTargetControl()
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
                fireTargetVector = hit.point;
                fireTargetVector.y = firePoint.position.y;
        }

        public void LunchBullet()
        {
            Vector3 direction = fireTargetVector - firePoint.transform.position;
            GameObject currentArrow = Instantiate(bullet, firePoint.position, Quaternion.identity);
            currentArrow.transform.LookAt(fireTarget);
            Rigidbody currentArrowVelocity = currentArrow.GetComponent<Rigidbody>();
            currentArrow.GetComponent<Rigidbody>().AddForce(fireSpeed * direction.normalized, ForceMode.Impulse);
            audioSource.PlayOneShot(shootSound);
            Destroy(currentArrow, 2f);
        }
        public void StepSound()
        {
            audioSource.PlayOneShot(stepSound);
        }
    }
}
