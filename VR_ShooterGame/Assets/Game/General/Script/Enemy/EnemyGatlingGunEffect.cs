using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
public class EnemyGatlingGunEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlashPrefab;
    [SerializeField] private Transform[] effectOrigin;
    private Realtime _realtime;

    private void Awake() 
    {
         _realtime = GetComponent<Realtime>();
    }

    public void StartPlayEffect()
    {
        StartCoroutine(PlayMuzzleFlash());
    }
    IEnumerator PlayMuzzleFlash()
    {
        var options = new Realtime.InstantiateOptions {
        ownedByClient            = true,    
        preventOwnershipTakeover = true,    
        useInstance              = _realtime 
        };
        
        float time = 0;

        while(time <= 5)
        {
            for(int originPoint = 0; originPoint < effectOrigin.Length; originPoint++)
            {
                Realtime.Instantiate(muzzleFlashPrefab.name, effectOrigin[originPoint].position, effectOrigin[originPoint].rotation,options);
                time += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
