using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GunScriptableObject currentGun;
    [SerializeField] MicrophoneManager micMgr;
    [SerializeField] Transform playerLook;

    float ammo = 1f;

    GunPrefab currentHappyGunPrefab;
    GunPrefab currentHellGunPrefab;
    ParticleSystem currentHappyParticleSystem;
    ParticleSystem currentHellParticleSystem;

    List<GameObject> _instantiatedObjects = new List<GameObject>();

    public void SetGun(GunScriptableObject newGun) {
        currentGun = newGun;
        Setup();
    }

    void ClearInstantiatedObjects()
    {
        if(currentHappyGunPrefab) Destroy(currentHappyGunPrefab.gameObject);
        if(currentHellGunPrefab) Destroy(currentHellGunPrefab.gameObject);

        foreach(GameObject obj in _instantiatedObjects) {
            Destroy(obj);
        }
        _instantiatedObjects.Clear();
    }

    void Start()
    {
        Setup();
    }

    void Setup() {
        ClearInstantiatedObjects();

        // GunPrefab gunPrefab = isHappy ? currentGun.happyPrefab : currentGun.hellPrefab;
        currentHappyGunPrefab = Instantiate(currentGun.happyPrefab, transform);
        currentHellGunPrefab = Instantiate(currentGun.hellPrefab, transform);

        currentHappyParticleSystem = Instantiate(currentGun.particleSysHappy, currentHappyGunPrefab.firePoint).GetComponent<ParticleSystem>();
        currentHellParticleSystem = Instantiate(currentGun.particleSysHell, currentHellGunPrefab.firePoint).GetComponent<ParticleSystem>();

        // layers
        // 6 = Happy
        // 7 = Hell
        currentHappyGunPrefab.gameObject.SetLayerRecursively(6);
        currentHellGunPrefab.gameObject.SetLayerRecursively(7);
    }

    void Fire()
    {
        currentHellParticleSystem.Emit(4);
        currentHappyParticleSystem.Emit(1);

        ParticleSystem[] particleChildren = currentHappyParticleSystem.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem sys in particleChildren) {
            sys.Emit(1);
        }
    }

    void FixedUpdate()
    {
        Vector3 targetDir = currentHellGunPrefab.firePoint.forward;

        RaycastHit hit;
        if(Physics.Raycast(playerLook.position, playerLook.forward, out hit, 100f)) {
            Vector3 dirToLookPos = (hit.point - currentHellGunPrefab.firePoint.position).normalized;
            
            float dotDiff = Vector3.Dot(dirToLookPos, currentHellGunPrefab.firePoint.forward);

            if(dotDiff > 0.5f) {
                targetDir = dirToLookPos;
            }
        }

        currentHellParticleSystem.transform.forward = targetDir;
        currentHappyParticleSystem.transform.forward = targetDir;

        if(Input.GetKey(KeyCode.Mouse0))
        {
            Fire();
        }
        
    }
}
