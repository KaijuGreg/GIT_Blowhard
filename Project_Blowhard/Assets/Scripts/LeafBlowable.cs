using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBlowable : MonoBehaviour
{
    private Rigidbody rb;

   
    //[SerializeField] private float liftForceMin = 0.5f;
    //[SerializeField] private float liftForceMax = 0.7f;
    //[SerializeField] private float torqueForceMin = 0.7f;
    //[SerializeField] private float torqueForceMax = 0.7f;


    [SerializeField] private float upwardLiftStrength = 0.5f; // More upward force
    [SerializeField] private float forwardStrength = 0.5f; // Wind pushes in blowDirection
    [SerializeField] private float torqueStrength = 2f;
    [SerializeField] private float torqueRandomness = 1f; // optional: adds variation
    [SerializeField] private float blowCooldown = 0.5f; // seconds
    private float lastBlownTime = -Mathf.Infinity;




    //-------------------------------------------------------------------------------------------------------

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    // This gets called externally when wind hits the leaf

    public void ApplyWindForce(Vector3 blowDirection, float blowForce) {
        if (rb == null) return;

        //If still in cooldown, skip applying force

        if (Time.time - lastBlownTime < blowCooldown)
            return;

        lastBlownTime = Time.time; // reset cooldown timer


        // --- Calculate Force ---
        Vector3 upwardLift = Vector3.up * upwardLiftStrength;
        Vector3 directionalForce = blowDirection.normalized * forwardStrength;

        Vector3 totalForce = (upwardLift + directionalForce).normalized * blowForce;

        rb.AddForce(totalForce, ForceMode.Impulse);

        // --- Add Random Torque ---
        Vector3 randomTorque = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized * torqueStrength * torqueRandomness;

        rb.AddTorque(randomTorque, ForceMode.Impulse);
    }






}
