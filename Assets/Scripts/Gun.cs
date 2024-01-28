using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GunScriptableObject currentGun;
    [SerializeField] MicrophoneManager micMgr;

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
        currentHellParticleSystem.Emit(1);
        currentHappyParticleSystem.Emit(1);

        ParticleSystem[] particleChildren = currentHappyParticleSystem.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem sys in particleChildren) {
            sys.Emit(1);
        }
    }

    void Update()
    {

        if(Input.GetKey(KeyCode.Mouse0))
        {
            Fire();
        }
        
    }
}
