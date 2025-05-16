using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBlowable : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float torqueStrength = 5f;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    // This gets called externally when wind hits the leaf
    public void ApplyWindForce(Vector3 blowDirection, float windStrength) {

        if (rb == null) return;

        Vector3 lift = Vector3.up * Random.Range(0.1f, 0.5f);
        Vector3 windForce = (blowDirection + lift).normalized * windStrength;

        rb.AddForce(windForce, ForceMode.Impulse);

        Vector3 randomTorque = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized * torqueStrength;

        rb.AddTorque(randomTorque, ForceMode.Impulse);
    }


}
