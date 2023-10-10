using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityLVLUpButton : MonoBehaviour
{
    [SerializeField] int maxAbilityLvl;
    [SerializeField] int currentAbilityLvl;
    [SerializeField] private Text abilityLvlText;
    [SerializeField] private Button button;
    [SerializeField] private PlaerLevel plaerLevel;

    void Start()
    {
        button = GetComponent<Button>();
        currentAbilityLvl = 1;
        abilityLvlText.text = currentAbilityLvl.ToString();
        button.interactable = false;
    }

    public void OnClickIncreaceAbilityLvl()
    {
        button.interactable = true;
        plaerLevel.UseAbilityPoint();
        ++currentAbilityLvl;
        abilityLvlText.text = currentAbilityLvl.ToString();
        if (currentAbilityLvl >= maxAbilityLvl)
        {
            abilityLvlText.text = "MAX".ToString();
            button.enabled = false;
        }
        plaerLevel.CheckAbilitiPoints();
    }
}
