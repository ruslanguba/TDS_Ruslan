using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLuncher : MonoBehaviour
{
    [SerializeField] private EnemeyMovement enemeyMovement;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float angleInDegrees = 60;
    [SerializeField] private float lunchDistans;
    [SerializeField] private float chargeTime;
    [SerializeField] private float currentTimeToCharge;
    [SerializeField] private GameObject stone;
    [SerializeField] private GameObject virtualStone;
    private Transform player;
    private Rigidbody rb;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        angleInDegrees = 60;
        rb = GetComponent<Rigidbody>();
        spawnTransform.localEulerAngles = new Vector3(-angleInDegrees, 0, 0);
        player = GetComponent<EnemeyMovement>().playerObject.transform;
    }

    void Update()
    {
        Shot();
    }

    private void Shot()
    {
        float distans = Vector3.Distance(transform.position, player.transform.position);
        if (distans <= lunchDistans)
            transform.LookAt(player.transform.position);
        
        if (currentTimeToCharge <= chargeTime)
            currentTimeToCharge += Time.deltaTime;
        if (currentTimeToCharge >= chargeTime - 3)
        {
            virtualStone.SetActive(true);
            rb.velocity = Vector3.zero;
            anim.SetBool("Walk Forward", false);
        }
           
        if (currentTimeToCharge>= chargeTime && distans <= lunchDistans)
        {
            anim.SetTrigger("Attack 01");
            Vector3 fromTo = player.position - transform.position;
            Vector3 fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);

            transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

            float x = fromToXZ.magnitude;
            float y = fromTo.y;
            float AngleInRadians = angleInDegrees * Mathf.PI / 180;

            float v2 = (9.8f * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
            float v = Mathf.Sqrt(Mathf.Abs(v2));

            GameObject currentAirstrikeGranade = Instantiate(stone, spawnTransform.position, Quaternion.identity);
            currentAirstrikeGranade.GetComponent<Rigidbody>().velocity = spawnTransform.forward * (v-1);
            virtualStone.SetActive(false);
            currentTimeToCharge = 0;
        }
    }
}
