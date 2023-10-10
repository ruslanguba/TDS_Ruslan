using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaerLevel : MonoBehaviour
{
    [SerializeField] private Text expirienceText;
    [SerializeField] private Text scoreBarLevelText;
    [SerializeField] private Text shopPanelAbilityPointsText;
    [SerializeField] private float expiriensToNextLVL;
    [SerializeField] private int abilityPoints;
    [SerializeField] private Button[] abilitesButton;
    [SerializeField] private ParticleSystem levelUpEffect;
    [SerializeField] private GameObject lvlUpStar;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AbilityUnlocker abilityUnlocker;
    private int currentLVL;
    private float currentExpiriens;

    void Start()
    {
        CheckAbilitiPoints();
        expirienceText.text = currentExpiriens.ToString();
        lvlUpStar.SetActive(false);
        currentLVL = 1;
        scoreBarLevelText.text = currentLVL.ToString();
    }

    public void CollectExpiriens(float expiriens)
    {
        currentExpiriens += expiriens;
        expirienceText.text = currentExpiriens.ToString();
        if (currentExpiriens >= expiriensToNextLVL)
        {
            currentLVL++;
            abilityPoints++;
            expiriensToNextLVL = 100 + expiriensToNextLVL * 1.05f;
            levelUpEffect.Play();
            audioSource.Play();
            lvlUpStar.SetActive(true);
            abilityUnlocker.UnlockAbility();
            scoreBarLevelText.text = currentLVL.ToString();
            CheckAbilitiPoints();
        }
    }

    public void UseAbilityPoint()
    {
        abilityPoints--;
        CheckAbilitiPoints();
    }

    public void CheckAbilitiPoints()
    {
        if (abilityPoints < 1)
            for (int i = 0; i < abilitesButton.Length; i++)
            {
                abilitesButton[i].interactable = false;
            }
        else
            for (int i = 0; i < abilitesButton.Length; i++)
            {
                abilitesButton[i].interactable = true;
            }
        shopPanelAbilityPointsText.text = abilityPoints.ToString();
        if (abilityPoints > 0)
            lvlUpStar.SetActive(true);
        else lvlUpStar.SetActive(false);
    }
}
