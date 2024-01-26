using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, ISwitchable
{
    [SerializeField] GunScriptableObject currentGun;
    [SerializeField] bool isHappy = true;

    float ammo = 1f;

    GunPrefab currentGunPrefab;
    ParticleSystem currentParticleSystem;

    List<GameObject> _instantiatedObjects = new List<GameObject>();

    public void SetGun(GunScriptableObject newGun) {
        currentGun = newGun;
        Setup();
    }

    void ClearInstantiatedObjects()
    {
        if(currentGunPrefab) Destroy(currentGunPrefab.gameObject);

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

        GunPrefab gunPrefab = isHappy ? currentGun.happyPrefab : currentGun.hellPrefab;
        currentGunPrefab = Instantiate(gunPrefab, transform);

        currentParticleSystem = Instantiate(currentGun.particleSystemPrefab, currentGunPrefab.firePoint).GetComponent<ParticleSystem>();
    }

    public void SetHell() {
        isHappy = false;
        Setup();
    }

    public void SetHappy() {
        isHappy = true;
        Setup();
    }

    void Fire()
    {
        currentParticleSystem.Emit(1);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            //debug
            isHappy = !isHappy;
            Setup();
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            Fire();
        }


        
    }
}
