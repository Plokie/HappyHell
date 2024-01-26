using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Gun", order = 1)]
public class GunScriptableObject : ScriptableObject
{
    public float spread;
    public float damage;
    public GunPrefab happyPrefab;
    public GunPrefab hellPrefab;
    public GameObject particleSystemPrefab;
}
