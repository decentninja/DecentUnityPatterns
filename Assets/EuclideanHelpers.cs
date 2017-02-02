using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuclideanHelpers : MonoBehaviour {

    public static GameObject Closest(Vector3 position, IEnumerable<GameObject> collection) {
        float closestRange = float.MaxValue;
        GameObject closest = null;
        foreach (var go in collection) {
            float goRange = Vector3.Distance(position, go.transform.position);
            if (goRange < closestRange) {
                closest = go;
                closestRange = goRange;
            }
        }
        return closest;
    }

    public static Vector3 RandomPositionInBounds(Bounds bounds) {
        return bounds.min + Vector3.Scale(RandomVector3(), bounds.max - bounds.min);
    }

    public static Vector3 RandomVector3() {
        return new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

}
