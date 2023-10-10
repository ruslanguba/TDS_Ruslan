using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TornadoController : MonoBehaviour
{
    [SerializeField] private GameObject tornadoEfect;
    [SerializeField] private float speed;
    [SerializeField] private float startMaxFlyTime;
    [SerializeField] private float startTornadoActiveTime;
    [SerializeField] private float tornadoChargeTime;
    [SerializeField] private Transform player;
    private Vector3 targetVector;
    private float maxFlyTime;
    private float currentTornadoActiveTime;
    private float tornadoLVL = 1;
    private float tornadoScaleLVl = 1;
    private float tornadoTimeLVl = 1;
    private float tornadoForceLVL = 1;
    private CapsuleCollider tornadoCollider;

    void Start()
    {
        startMaxFlyTime = 3;
        maxFlyTime = startMaxFlyTime;
        targetVector = new Vector3(transform.position.x + Random.Range(-6, 6), transform.position.y, transform.position.z + Random.Range(-6, 6));
        tornadoCollider = GetComponent<CapsuleCollider>();
        currentTornadoActiveTime = startTornadoActiveTime;
    }

    private void Update()
    {
        TornadoMove();
    }

    public void ActivateTornado()
    {
        StartCoroutine(TornadoTimer());
    }

    IEnumerator TornadoTimer()
    {
        tornadoEfect.SetActive(true);
        tornadoCollider.enabled = true;
        Vector3 tornadoStartPoint = new Vector3(player.position.x + Random.Range(-10, 10), transform.position.y, player.position.z + Random.Range(-10, 10));
        transform.position = tornadoStartPoint;
        yield return new WaitForSeconds(currentTornadoActiveTime);
        tornadoEfect.SetActive(false);
        tornadoCollider.enabled = false;
        yield return new WaitForSeconds(tornadoChargeTime - currentTornadoActiveTime);
        StartCoroutine(TornadoTimer());
    }

    private void TornadoMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetVector, Time.deltaTime * speed);
        float dist = Vector3.Distance(transform.position, targetVector);
        if (dist < 0.5f)
            targetVector = new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y, transform.position.z + Random.Range(-3, 3));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemey"))
            other.GetComponent<EnemeyMovement>().OnTornado(maxFlyTime);
    }

    public void IncreaseTornadoForceLVL()
    {
        ++tornadoForceLVL;
        maxFlyTime = startMaxFlyTime + tornadoForceLVL;
    }
    public void IncreaseTornadoScaleLVL()
    {
        tornadoScaleLVl++;
        transform.localScale = transform.localScale * (1 + tornadoScaleLVl / 15);
        IncreaseLVL();
    }

    public void IncreaseTornadoTimeLVL()
    {
        tornadoTimeLVl++;
        currentTornadoActiveTime = startTornadoActiveTime + tornadoTimeLVl * 2;
        IncreaseLVL();
    }

    public void IncreaseLVL()
    {
        tornadoLVL++;
    }
}
