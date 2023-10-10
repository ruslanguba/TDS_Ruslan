using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoreBarTimeText;
    [SerializeField] private Text scoreBarEnemiesText;
    [SerializeField] private float currentTime;
    [SerializeField] private GameObject darkPanel;
    [SerializeField] private PlaerLevel plaerLevel;
    private int enemiesKilled;

    void Start()
    {
        Time.timeScale = 0;
        darkPanel.GetComponent<Animator>().Play("GameDarkPanel");
        Destroy(darkPanel, 10);
        StartCoroutine(TimerCorutine());
    }

    IEnumerator TimerCorutine()
    {
        yield return new WaitForSeconds(1);
        currentTime++;
        timeText.text = Mathf.Round(currentTime).ToString();
        StartCoroutine(TimerCorutine());
    }

    public void CollectExpiriens(float expiriens)
    {
        plaerLevel.CollectExpiriens(expiriens);
    }

    public void onClickPuse()
    {
        Time.timeScale = 0;
        CheckScore();
    }

    public void OnCklickResume()
    {
        Time.timeScale = 1;
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene(0);
    }

    public void EnemeyKilled()
    {
        enemiesKilled++;
        scoreBarEnemiesText.text = enemiesKilled.ToString();
    }

    public void CheckScore()
    {
        scoreBarTimeText.text = Mathf.Round(currentTime).ToString();
    }

}
