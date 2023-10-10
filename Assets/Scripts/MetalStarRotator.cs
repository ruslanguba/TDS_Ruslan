using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalStarRotator : MonoBehaviour
{
    [SerializeField] private GameObject[] activeStars;
    [SerializeField] private StarRotator[] starRotator;
    private int rotatorScaleLVL = 0;

    public void IncreaseRotatorDamageLVL()
    {
        for (int i = 0; i < starRotator.Length; i++)
        {
            starRotator[i].IncreaseStarLVL();
        }
    }

    public void IncreaseRotatorScaleLVL()
    {
        rotatorScaleLVL++;
        if (rotatorScaleLVL <= activeStars.Length)
        {
            for (int i = 0; i < activeStars.Length; i++)
            {
                activeStars[i].SetActive(false);
            }
            activeStars[rotatorScaleLVL].SetActive(true);
        }
    }
}
