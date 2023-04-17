using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossArea : MonoBehaviour
{
    public GameObject bossHPUI;
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            bossHPUI.SetActive(true);
        }
    }
}
