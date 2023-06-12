using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Normal.Realtime;
public class UIPlayerHealthEffect : MonoBehaviour
{
    [SerializeField] private Image splatterImage = null;
    private void Start() 
    {
        GameObject.DontDestroyOnLoad(gameObject);    
    }
    public void RefreshPlayerSplitterUI(int playerHealth, GameObject player)
    {
        if (player.GetComponent<RealtimeView>().isOwnedLocallyInHierarchy)
        {
        Color splatterImageColor = splatterImage.color;
        splatterImageColor.a = 1 - (playerHealth / 100.0f);
        splatterImage.color = splatterImageColor;
        }
    }
}
