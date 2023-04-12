using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEffect : MonoBehaviour
{
    [SerializeField] private Gun gun;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private TrailRenderer tracerEffect;

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private Transform raycastDestination;

    private Ray ray;
    private RaycastHit hitInfo;

    private void OnEnable()
    {
        gun.OnGunShoot += CastFireEffect;
    }

    private void OnDisable()
    {
        gun.OnGunShoot -= CastFireEffect;
    }

    public void CastFireEffect()
    {
        PlayMuzzleFlash();

        SetupRay();
        
        TrailRenderer tracer = CreateBulletTracer();

        if (Physics.Raycast(ray, out hitInfo))
        {
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Emit(1);
    }

    private void SetupRay()
    {
        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;
    }
    
    private TrailRenderer CreateBulletTracer()
    {
        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        return tracer;
    }

    public void SetRaycastOrigin(Transform newRaycastOrigin)
    {
        raycastOrigin = newRaycastOrigin;
    }

    public void SetGunEffect(ParticleSystem newMuzzleEffect, ParticleSystem newHitEffect)
    {
        muzzleFlash = newMuzzleEffect;
        hitEffect = newHitEffect;
    }
}
