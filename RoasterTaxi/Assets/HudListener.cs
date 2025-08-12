using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudListener : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI boostCounterText;

    private void OnEnable()
    {
        EventManager.uiEvents.OnBoostChange += UpdateBoostCount;
    }

    private void OnDisable()
    {
        EventManager.uiEvents.OnBoostChange -= UpdateBoostCount;
    }

    private void UpdateBoostCount(Component sender, float boost)
    {
        boostCounterText.text = boost.ToString();

    }
}
