using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanisterPickup : MonoBehaviour, IPickup
{
    [SerializeField] Collider triggerCollider;
    [SerializeField] Transform modelTransform;
    [SerializeField] float spinSpeed = 15f;
    [SerializeField] float levitateFrequency = 1f;
    [SerializeField] float levitateAmplitude = 0.1f;
    void Update() {
        modelTransform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.Self);
        modelTransform.localPosition = new Vector3(modelTransform.localPosition.x, 0.45f + (Mathf.Sin(Time.time * levitateFrequency) * levitateAmplitude), modelTransform.localPosition.z);

        triggerCollider.enabled = Sanity.Instance.IsSane;
        modelTransform.gameObject.SetActive(Sanity.Instance.IsSane);
    }
    void IPickup.OnPickup() {
        print("Pickup canister");
        Destroy(gameObject);

        GameObject.FindGameObjectWithTag("PlayerCamTransform").GetComponentInChildren<Gun>().ReplenishAmmo();
    }
}
