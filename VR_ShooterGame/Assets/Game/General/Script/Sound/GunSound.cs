using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class GunSound : MonoBehaviour
{
    AudioSource audioSource;
    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        PlaySound();
    }
    public void PlaySound()
    {
        audioSource.PlayOneShot(audioSource.clip);

        if(gameObject.GetComponent<RealtimeView>().isOwnedLocallyInHierarchy)
        {
        StartCoroutine(DestroySoundObj());
        }
    }

    private IEnumerator DestroySoundObj()
    {
        yield return new WaitForSeconds(1);
        Realtime.Destroy(gameObject);
    }

}
