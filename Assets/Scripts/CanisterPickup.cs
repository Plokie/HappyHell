using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanisterPickup : MonoBehaviour, IPickup
{
    [SerializeField] Transform modelTransform;
    [SerializeField] float spinSpeed = 15f;
    void Update() {
        modelTransform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.Self);
    }
    void IPickup.OnPickup() {
        print("Pickup canister");
        Destroy(gameObject);
    }
}
