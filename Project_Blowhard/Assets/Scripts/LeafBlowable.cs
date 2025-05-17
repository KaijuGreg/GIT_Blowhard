using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBlowable : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float forwardForceMin = 3f;
    [SerializeField] private float forwardForceMax = 6f;

    [SerializeField] private float liftForceMin = 0.5f;
    [SerializeField] private float liftForceMax = 0.7f;
    [SerializeField] private float torqueForceMin = 0.7f;
    [SerializeField] private float torqueForceMax = 0.7f;


 
    [SerializeField] private float torqueStrength = 2f;
    [SerializeField] private float torqueRandomness = 1f; // optional: adds variation
    [SerializeField] private float blowCooldown = 0.5f; // seconds
    private float lastBlownTime = -Mathf.Infinity;




    //-------------------------------------------------------------------------------------------------------

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    // This gets called externally when wind hits the leaf

    public void ApplyWindForce(Vector3 blowDirection, float globalWindStrength) {
        if (rb == null) return;

        // Lift
        Vector3 lift = Vector3.up * Random.Range(liftForceMin, liftForceMax);

        // Forward force range — random but constrained
        //float forwardForce = Random.Range(forwardForceMin, forwardForceMax);

        float forwardForce = forwardForceMax; // No randomness


        // Combine forces
        Vector3 windForce = (blowDirection * forwardForce) + lift;

        rb.AddForce(windForce, ForceMode.Impulse);

        // Torque (still randomized)
        Vector3 randomTorque = new Vector3(
            Random.Range(-torqueForceMin, torqueForceMax),
            Random.Range(-torqueForceMin, torqueForceMax),
            Random.Range(-torqueForceMin, torqueForceMax)
        ).normalized * torqueStrength;

        rb.AddTorque(randomTorque, ForceMode.Impulse);
    }






}
