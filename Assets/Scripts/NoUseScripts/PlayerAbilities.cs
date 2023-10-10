using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] GameObject IceRainObject, IceRainEffect;
    [SerializeField] float StartIceRainActiveTime, IceRainChargeTime;
    private float IceRainCurrentTime, currentIceRainActiveTime;
    private IceRain iceRain;
    private float IceRainScaleLVl = 1, IceRainTimeLVl = 1;
    private CapsuleCollider IceRainCollider;
    private bool IceRainEnable = false;

    [SerializeField] GameObject TornadoObject, TornadoEfect;
    [SerializeField] private float StartTornadoActiveTime, TornadoChargeTime;
    private float TornadoCurrentTime, currentTornadoActiveTime;
    private Tornado tornado;
    private float TornadoScaleLVl = 1, TornadoTimeLVl =1;
    private CapsuleCollider tornadoCollider;
    private bool TornadoEnable = false;

    [SerializeField] GameObject Rotator;
    private MetalStarRotator metalStarRotator;
    [SerializeField] float StartRotatorActiveTime, RotatorChargeTime;
    private float RotatorCurrentTime, currentRotatorActiveTime;
    private int RotatorTimeLVL = 1;
    private bool RotatorEnable = false;

    [SerializeField] Transform GranadeSpown;
    [SerializeField] GameObject _Granade;
    [SerializeField] GranadeExplosion granadeExplosion;
    [SerializeField] private float GranadechargeTimeStart;
    private float AngleInDegrees = 45, GranadechargeTimeCurrrent, currentTimeToChargeGranade;
    public int granadeCurrentLVL = 1, granadeTimeLVL = 0, granadeScaleLVL = 1, granadeDamageLVL = 1;
    private bool GranadeSpownEnable = false;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip IceUnlockSound, RotatorUnlockSound, GranadeUnlockSound, TornadoUnlockSound;
    public bool IceRainAvaliuble;

    void Start()
    {
        iceRain = IceRainObject.GetComponent<IceRain>();
        currentIceRainActiveTime = StartIceRainActiveTime;
        IceRainCollider = IceRainObject.GetComponent<CapsuleCollider>(); ;

        tornado = TornadoObject.GetComponent<Tornado>();
        tornadoCollider = TornadoObject.GetComponent<CapsuleCollider>();
        currentTornadoActiveTime = StartTornadoActiveTime;

        metalStarRotator = Rotator.GetComponent<MetalStarRotator>();
        currentRotatorActiveTime = StartRotatorActiveTime;

        granadeExplosion = _Granade.GetComponent<GranadeExplosion>();
        GranadechargeTimeCurrrent = GranadechargeTimeStart;
    }

    void Update()
    {
        ActiveIceRain();
        ActiveTornado();
        ActiveRotator();
        GranadeLuncher();
    }

    #region IceRain;
    private void ActiveIceRain()
    {
        if (IceRainEnable == true)
        {
            if (IceRainCurrentTime <= IceRainChargeTime)
                IceRainCurrentTime += Time.deltaTime;
            if (IceRainCurrentTime >= currentIceRainActiveTime)
            {
                IceRainObject.SetActive(false);
            }
            if (IceRainCurrentTime >= IceRainChargeTime)
            {
                IceRainObject.SetActive(true);
                IceRainCurrentTime = 0;
            }
        }
    }

    public void IncreaseIceRainScale()
    {
        IceRainScaleLVl++;
        IceRainObject.transform.localScale = IceRainObject.transform.localScale * (1 + IceRainScaleLVl / 15);
    }
    public void IncreaseIceRainTime()
    {
        IceRainTimeLVl++;
        currentIceRainActiveTime = currentIceRainActiveTime + IceRainTimeLVl * 1.5f;
        iceRain.IncreaseIceRainLVL();
    }
    #endregion;
    #region Tornado;
    
    private void ActiveTornado()
    {
        if (TornadoEnable == true)
        {
            if (TornadoCurrentTime <= TornadoChargeTime)
                TornadoCurrentTime += Time.deltaTime;
            if (TornadoCurrentTime >= currentTornadoActiveTime)
            {
                TornadoEfect.SetActive(false);
                tornadoCollider.enabled = false;
            }
            if (TornadoCurrentTime >= TornadoChargeTime)
            {
                TornadoEfect.SetActive(true);
                tornadoCollider.enabled = true;
                TornadoCurrentTime = 0;
                Vector3 tornadoStartPoint = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10));
                TornadoObject.transform.position = tornadoStartPoint;
            }
        }
    }

    public void IncreaseTornadoScaleLVL()
    {
        TornadoScaleLVl++;
        TornadoObject.transform.localScale = TornadoObject.transform.localScale * (1 + TornadoScaleLVl / 15);
        tornado.IncreaseLVL();
    }

    public void IncreaseTornadoTimeLVL()
    {
        TornadoTimeLVl++;
        currentTornadoActiveTime = StartTornadoActiveTime + TornadoTimeLVl * 2;
        tornado.IncreaseLVL();
    }
    
    #endregion;
    #region MetalStars;

    private void ActiveRotator()
    {
        if (RotatorEnable == true)
        {
            if (RotatorCurrentTime <= RotatorChargeTime)
                RotatorCurrentTime += Time.deltaTime;
            if (RotatorCurrentTime >= currentRotatorActiveTime)
            {
                Rotator.SetActive(false);
            }
            if (RotatorCurrentTime >= RotatorChargeTime)
            {
                Rotator.SetActive(true);
                RotatorCurrentTime = 0;
            }
        }
    }

    public void IncreaseRotatorTimeLVL()
    {
        RotatorTimeLVL++;
        currentRotatorActiveTime = currentRotatorActiveTime + RotatorTimeLVL;
    }

    #endregion;

    private void GranadeLuncher()
    {
        if (GranadeSpownEnable == true)
        {
            if (currentTimeToChargeGranade <= GranadechargeTimeCurrrent)
                currentTimeToChargeGranade += Time.deltaTime;
                if (currentTimeToChargeGranade >= GranadechargeTimeCurrrent)
                {
                float radius = Random.Range(2, 10);
                Vector3 GranadeTargetPoint = transform.position + new Vector3(Random.value - 0.5f, transform.position.y, Random.value - 0.5f).normalized * radius;
                Vector3 fromTo = GranadeTargetPoint - transform.position;
                    Vector3 fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);

                    transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

                    float x = fromToXZ.magnitude;
                    float y = fromTo.y;
                    float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

                    float v2 = (9.8f * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
                    float v = Mathf.Sqrt(Mathf.Abs(v2));

                    GameObject currentAirstrikeGranade = Instantiate(_Granade, GranadeSpown.position, Quaternion.identity);
                    currentAirstrikeGranade.GetComponent<Rigidbody>().velocity = GranadeSpown.forward * v;
                    currentTimeToChargeGranade = 0;
                }
        }
    }
        public void IncreaseGranadeTimeLVL()
    {
        granadeTimeLVL++;
        GranadechargeTimeCurrrent = GranadechargeTimeCurrrent - 1;
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

    public void EnableIceRain()
    {
        IceRainEnable = true;
        audioSource.PlayOneShot(IceUnlockSound);
    }

    public void EnableRotator()
    {
        RotatorEnable = true;
        audioSource.PlayOneShot(RotatorUnlockSound);
    }

    public void GranadeEnable()
    {
        GranadeSpownEnable = true;
        audioSource.PlayOneShot(GranadeUnlockSound);
    }

    public void EnableTornado()
    {
        TornadoEnable = true;
        audioSource.PlayOneShot(TornadoUnlockSound);
    }
}
