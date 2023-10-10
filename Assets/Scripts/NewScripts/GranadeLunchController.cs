using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GranadeLunchController : MonoBehaviour
{
    [SerializeField] private int granadeCurrentLVL = 1;
    [SerializeField] private int granadeTimeLVL = 0;
    [SerializeField] private int granadeScaleLVL = 1;
    [SerializeField] private int granadeDamageLVL = 1;
    [SerializeField] Transform granadeSpownPoint;
    [SerializeField] GameObject granade;
    [SerializeField] private float granadeChargeTimeStart;
    private float AngleInDegrees = 45;
    private float currentTimeChargeGranade;

    private void Start()
    {
        currentTimeChargeGranade = granadeChargeTimeStart;
    }

    public void ActivateGranade()
    {
        StartCoroutine(GranadeTimer());
    }

    IEnumerator GranadeTimer()
    {
        yield return new WaitForSeconds(currentTimeChargeGranade);
        GranadeLuncher();
        StartCoroutine(GranadeTimer());
    }

    private void GranadeLuncher()
    {
        float radius = Random.Range(2, 10);
        Vector3 GranadeTargetPoint = transform.position + new Vector3(Random.value - 0.5f, transform.position.y, Random.value - 0.5f).normalized * radius;
        Vector3 fromTo = GranadeTargetPoint - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);
        Debug.Log(GranadeTargetPoint);
        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;
        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

        float v2 = (9.8f * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));
        Debug.Log(v);
        GameObject currentAirstrikeGranade = Instantiate(granade, granadeSpownPoint.position, Quaternion.identity);
        currentAirstrikeGranade.GetComponent<GranadeExplosion>().SetGranadeStats(granadeCurrentLVL, granadeDamageLVL, granadeScaleLVL);
        currentAirstrikeGranade.GetComponent<Rigidbody>().velocity = granadeSpownPoint.forward * v;
    }

    public void IncreaseGranadeTimeLVL()
    {
        granadeTimeLVL++;
        currentTimeChargeGranade = currentTimeChargeGranade - 1;
    }

    public void IncreaseGranadeDamageLVL()
    {
        granadeDamageLVL++;
        granadeCurrentLVL++;
    }

    public void IncreaseGranadeScaleLVL()
    {
        granadeScaleLVL++;
    }
}
