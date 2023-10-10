using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceRainController : MonoBehaviour
{
    [SerializeField] private float startFreezeForce = 0.9f;
    [SerializeField] private GameObject iceRainEffect;
    [SerializeField] private float startIceRainActiveTime;
    [SerializeField] private float iceRainChargeTime;
    private float iceRainCurrentTime;
    private float currentIceRainActiveTime;
    private float currentFreezeForce;
    private float freezeForceLVL = 1;
    private float iceRainScaleLVl = 1;
    private float iceRainTimeLVl = 1;
    private SphereCollider iceRainCollider;
    private bool isFreasing;
    private int iceRainLVL; // в будущем додавить урон в секунду при высоком уровне

    private void Awake()
    {
        currentFreezeForce = startFreezeForce;
        currentIceRainActiveTime = startIceRainActiveTime;
        iceRainCollider = GetComponent<SphereCollider>(); 
    }

    public void OnClickActivateIceRain()
    {
        StartCoroutine(IceRainTimer());
    }

    IEnumerator IceRainTimer()
    {
        iceRainEffect.SetActive(true);
        isFreasing = true;
        iceRainCollider.enabled = true;
        iceRainCurrentTime += Time.deltaTime;
        yield return new WaitForSeconds(currentIceRainActiveTime);
        iceRainEffect.SetActive(false);
        isFreasing = false;
        iceRainCollider.enabled = false;
        iceRainCurrentTime = 0;
        yield return new WaitForSeconds(iceRainChargeTime - currentIceRainActiveTime);
        StartCoroutine(IceRainTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemey") && isFreasing == true)
        {
            other.GetComponent<EnemeyMovement>().OnIce(currentFreezeForce);
            other.GetComponent<EnemeyMovement>().Invoke("OffIce", currentIceRainActiveTime - iceRainCurrentTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemey"))
        {
            other.GetComponent<EnemeyMovement>().OffIce();
        }
    }

    public void IncreaseIceRainForceLVL()
    {
        freezeForceLVL++;
        currentFreezeForce = startFreezeForce * (1 - freezeForceLVL / 10);
        IncreaseIceRainLVL();
    }

    public void IncreaseIceRainScale()
    {
        iceRainScaleLVl++;
        transform.localScale = transform.localScale * (1 + iceRainScaleLVl / 15);
        IncreaseIceRainLVL();
    }

    public void IncreaseIceRainTime()
    {
        iceRainTimeLVl++;
        currentIceRainActiveTime = currentIceRainActiveTime + iceRainTimeLVl * 1.5f;
        IncreaseIceRainLVL();
    }

    public void IncreaseIceRainLVL()
    {
        iceRainLVL++;
    }
}
