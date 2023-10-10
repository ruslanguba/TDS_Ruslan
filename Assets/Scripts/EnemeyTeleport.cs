using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyTeleport : MonoBehaviour
{
    [SerializeField] private float minTeleportDistance = 10;
    [SerializeField] private float maxTeleportDistance = 15;
    [SerializeField] private float telerportTime = 10;
    [SerializeField] private float maxDistanceToPlayer = 25;
    [SerializeField] private Transform teleportPoint;
    private Transform playerObject;

    void Start()
    {
        StartCoroutine(TeleportToPlayer());
        teleportPoint.transform.parent = null;
        playerObject = GetComponent<EnemeyMovement>().playerObject.transform;
    }

    IEnumerator TeleportToPlayer()
    {
        yield return new WaitForSeconds(telerportTime);
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (distance > maxDistanceToPlayer)
        {
            Debug.Log("Teleport");
            float radius = Random.Range(minTeleportDistance, maxTeleportDistance);
            Vector3 randomPoint = playerObject.transform.position + new Vector3(Random.value - 0.5f, teleportPoint.position.y, Random.value - 0.5f).normalized * radius;
            teleportPoint.position = new Vector3(randomPoint.x, playerObject.transform.position.y, randomPoint.z);
            yield return new WaitForFixedUpdate();
            if (Physics.OverlapSphere(teleportPoint.position, 0.2f).Length > 0 && teleportPoint.position.y < 1)
            {
                transform.position = teleportPoint.position;
                if (Physics.OverlapSphere(transform.position, 0.2f).Length < 1)
                {
                    Destroy(gameObject);
                }
            }
        }
        StartCoroutine(TeleportToPlayer());
    }

    public void DestroyTeleportPoint()
    {
        Destroy(teleportPoint.gameObject, 3f);
    }
}
