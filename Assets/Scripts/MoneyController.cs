using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private Text gamePanelMoneyText;
    [SerializeField] private Text shopMoneyText;
    [SerializeField] private Text upDamagePriceText;
    [SerializeField] private Text upAmmoPriceText;
    [SerializeField] private Text upAttackSpeedPriceText;
    [SerializeField] private Button upDamageButton;
    [SerializeField] private Button upAmmoButton;
    [SerializeField] private Button upAttackSpeedButton;
    [SerializeField] private int upDamagePrice;
    [SerializeField] private int upAmmoPrice;
    [SerializeField] private int upAttackSpeedPrice;
    [SerializeField] private PlayerShooter playerShooter;
    private int currentMoney;

    private void Start()
    {
        CheckCurrentMoney();
    }

    public void CollectMoney(int money)
    {
        currentMoney += money;
        CheckCurrentMoney();
    }

    private void CheckCurrentMoney()
    {
        gamePanelMoneyText.text = currentMoney.ToString();
        if (currentMoney >= upDamagePrice)
            upDamageButton.interactable = true;
        else upDamageButton.interactable = false;

        if (currentMoney >= upAmmoPrice)
            upAmmoButton.interactable = true;
        else upAmmoButton.interactable = false;

        if (currentMoney >= upAttackSpeedPrice)
            upAttackSpeedButton.interactable = true;
        else upAttackSpeedButton.interactable = false;
        shopMoneyText.text = currentMoney.ToString();
        upDamagePriceText.text = upDamagePrice.ToString();
        upAmmoPriceText.text = upAmmoPrice.ToString();
        upAttackSpeedPriceText.text = upAttackSpeedPrice.ToString();
    }

    public void IncreaseAttackSpeed()
    {
        playerShooter.IncreaseAttackSpeed();
        currentMoney = currentMoney - upAttackSpeedPrice;
        upAttackSpeedPrice = (int)(upAttackSpeedPrice * 1.2f);
        CheckCurrentMoney();
    }

    public void IncreaseAttackDamage()
    {
        playerShooter.IncreaseAttackDamage();
        currentMoney = currentMoney - upDamagePrice;
        upDamagePrice = (int)(upDamagePrice * 1.2f);
        CheckCurrentMoney();
    }

    public void IncreaseAmmo()
    {
        playerShooter.IncreaseAmmo();
        currentMoney = currentMoney - upAmmoPrice;
        upAmmoPrice = (int)(upAmmoPrice * 1.2f);
        CheckCurrentMoney();
    }

}
