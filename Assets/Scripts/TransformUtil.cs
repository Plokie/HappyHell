using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformUtil {
    public static void SetLayerRecursively(this GameObject self, int layer) {
        Transform[] children = self.GetComponentsInChildren<Transform>();

        foreach(Transform child in children) {
            child.gameObject.layer = layer;
        }
    }
}