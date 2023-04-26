using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGatlingGunEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlashPrefab;
    [SerializeField] private Transform[] effectOrigin;

    IEnumerator PlayMuzzleFlash()
    {
        float time = 0;

        while(time <= 5)
        {
            for(int originPoint = 0; originPoint < effectOrigin.Length; originPoint++)
            {
                Instantiate(muzzleFlashPrefab, effectOrigin[originPoint].position, effectOrigin[originPoint].rotation);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
