using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiLunchArms : MonoBehaviour
{
    [SerializeField] GameObject feezZone;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject Freez = Instantiate(feezZone, transform.position, Quaternion.AngleAxis(0, Vector3.up));
        Destroy(GetComponent<SphereCollider>());
        Destroy(Freez.gameObject, 4f);
        Destroy(gameObject, 4.1f);
        //на объекте ничего нет только коллайдер с тегом и визуальный эффект
    }
}
