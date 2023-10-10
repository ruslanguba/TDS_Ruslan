using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManagerClass;
    [SerializeField] private PlaerLevel playerLevelClass;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemey;
    [SerializeField] private float startWaweEnemyeCount = 5;
    [SerializeField] private float startTameToNextVave;
    [SerializeField] private float enemiesSpownCount;
    [SerializeField] private float currentWaweEnemyeCount;
    [SerializeField] private float currentTimeToNextVave;
    [SerializeField] private float timeToNextEnemeyLVL;
    [SerializeField] private int randomEnemeyType;
    [SerializeField] private int gameDifficulty;
    [SerializeField] private float minSpownDistance;
    [SerializeField] private float maxSpownDistance;
    [SerializeField] private Transform spownPoint;
    private bool isBigVave = false;
    private float enemiesLVL = 1;

    private void Start()
    {
        currentWaweEnemyeCount = startWaweEnemyeCount;
        currentTimeToNextVave = startTameToNextVave;
        StartCoroutine(EnemiesLevelIncrace());
        StartCoroutine(EnemeySpawn());
    }

    private void CheckEnemeType()
    {
        int randomVelue = Random.Range(0, 100);
        if (randomVelue < 4)
        {
            randomEnemeyType = 3; ;
        }
        else if (randomVelue < 10)
        {
            randomEnemeyType = 2;
        }
        else if (randomVelue < 15)
        {
            randomEnemeyType = 1;
        }
        else if (randomVelue < 100)
        {
            randomEnemeyType = 0;
        }
    }

    IEnumerator EnemeySpawn()
    {
        if (isBigVave)
            enemiesSpownCount = currentWaweEnemyeCount * 2;
        else
        enemiesSpownCount = currentWaweEnemyeCount;
        while (enemiesSpownCount > 0)
        {
            float radius = Random.Range(minSpownDistance, maxSpownDistance);
            Vector3 randomPoint = transform.position + new Vector3(Random.value - 0.5f, transform.position.y, Random.value - 0.5f).normalized * radius;
            spownPoint.position = new Vector3(randomPoint.x, transform.position.y, randomPoint.z);
            yield return new WaitForFixedUpdate();
            CheckEnemeType();
            if (Physics.OverlapSphere(spownPoint.position, 0.2f).Length > 0 && spownPoint.position.y <1)
            {
                GameObject cube = Instantiate(enemey[randomEnemeyType], spownPoint.position, Quaternion.identity);
                cube.GetComponent<EnemeyHealth>().SetStarStats(enemiesLVL);
                cube.GetComponent<EnemeyHealth>().SetGameManager(gameManagerClass);
                cube.GetComponent<EnemeyHealth>().SetPlayerLevel(playerLevelClass);
                cube.GetComponent<EnemeyMovement>().SetStartSpeed(enemiesLVL);
                cube.GetComponent<EnemeyAttack>().SetStartDamage(enemiesLVL);
                cube.GetComponent<EnemeyMovement>().SetPlayerObject(player);
                enemiesSpownCount--;
                if (enemiesSpownCount == 1)
                    isBigVave = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(currentTimeToNextVave);
        StartCoroutine(EnemeySpawn());
    }

    IEnumerator EnemiesLevelIncrace()
    {
        yield return new WaitForSeconds(timeToNextEnemeyLVL);
        gameDifficulty++;
        currentWaweEnemyeCount = currentWaweEnemyeCount * 1.4f;
        currentTimeToNextVave = currentTimeToNextVave * 0.95f;
        if (gameDifficulty == 3) 
        {
            enemiesLVL++;
            gameDifficulty = 0;
            isBigVave = true;
            Debug.Log("BigWave");
        }
        StartCoroutine(EnemiesLevelIncrace());
    }
}
