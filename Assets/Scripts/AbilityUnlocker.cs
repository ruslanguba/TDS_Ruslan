using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUnlocker : MonoBehaviour
{
    [SerializeField] private Button[] unlockButon;
    [SerializeField] private int unlockTapButtonPoints;

    public void UnlockAbility()
    {
        unlockTapButtonPoints++;

        if (unlockTapButtonPoints == 3)
            for (int i = 0; i <= unlockButon.Length - 1; i++)
            {
                if (unlockButon[i] == true)
                    unlockButon[i].interactable = true;
            }
    }

    public void OnClickUnlockAbility()
    {
        if (unlockTapButtonPoints > 1)
            unlockTapButtonPoints = unlockTapButtonPoints - 3;
    }
}
