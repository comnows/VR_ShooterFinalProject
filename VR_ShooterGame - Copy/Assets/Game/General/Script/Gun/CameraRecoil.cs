using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    [SerializeField] private Gun gun;
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] private float adsRecoilX;
    [SerializeField] private float adsRecoilY;
    [SerializeField] private float adsRecoilZ;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void AddRecoil()
    {
        if(gun.isAimingDownSight)
        {
            targetRotation += new Vector3(adsRecoilX, Random.Range(-adsRecoilY, adsRecoilY), Random.Range(-adsRecoilZ, adsRecoilZ));
        }
        else
        {
            targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        }
    }

    private void OnEnable() {
        gun.OnGunShoot += AddRecoil;
    }

    private void OnDisable()
    {
        gun.OnGunShoot -= AddRecoil;
    }
}
