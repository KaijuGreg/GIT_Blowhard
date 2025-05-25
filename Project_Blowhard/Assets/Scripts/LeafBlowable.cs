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
    private float lastBlownTime = -999f;


    //-------------------------------------------------------------------------------------------------------

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    // This gets called externally when wind hits the leaf

    public void ApplyWindForce(Vector3 blowDirection, float blowForce) {
        if (Time.time - lastBlownTime < blowCooldown)
           
        return;

        lastBlownTime = Time.time;

        // Lift
        Vector3 forwardForce = blowDirection.normalized * Random.Range (forwardForceMin,forwardForceMax)* blowForce;
        Vector3 liftForce = Vector3.up * Random.Range(liftForceMin, liftForceMax);

        Vector3 windForce = forwardForce + liftForce;

        rb.AddForce(windForce, ForceMode.Impulse);

        Vector3 randomTorque = new Vector3(
            Random.Range(-torqueForceMin, torqueForceMax),
            Random.Range(-torqueForceMin, torqueForceMax),
            Random.Range(-torqueForceMin, torqueForceMax)
        ).normalized * torqueStrength;

        rb.AddTorque(randomTorque, ForceMode.Impulse);

   
    }
       

}
