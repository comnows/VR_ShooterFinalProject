using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerHealth : MonoBehaviour
{
    public TMP_Text HealthText;

    public void RefreshPlayerHealthUI(int health)
    {
        HealthText.text = health.ToString();
    }
}
