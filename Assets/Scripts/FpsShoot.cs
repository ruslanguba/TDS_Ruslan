using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Camera cameraPlayer;
    [SerializeField] Transform spownBullet;

    [SerializeField] private float shootForce;
    [SerializeField] private float spread;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Shoot()
    {
        Ray ray = cameraPlayer.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else 
            targetPoint = ray.GetPoint(200);
        Vector3 dirWithoutSpread = targetPoint - spownBullet.position;
        Debug.Log(hit);
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 dirWithSpread = dirWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, spownBullet.position, Quaternion.identity);
        currentBullet.transform.forward = dirWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(dirWithSpread.normalized * shootForce, ForceMode.Impulse);
    }
}
