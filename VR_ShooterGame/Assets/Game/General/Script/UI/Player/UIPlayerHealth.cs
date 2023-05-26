using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Normal.Realtime;
public class UIPlayerHealth : MonoBehaviour
{
    public TMP_Text HealthText;

    private void Start() 
    {
        GameObject.DontDestroyOnLoad(gameObject);    
    }
    public void RefreshPlayerHealthUI(int health, GameObject player)
    {
        if (player.GetComponent<RealtimeView>().isOwnedLocallyInHierarchy)
        {
            int playerHealth = health;
        
            if (health < 0)
            {
                playerHealth = 0;
            }

            HealthText.text = playerHealth.ToString() + "%";
        }
    }
}
