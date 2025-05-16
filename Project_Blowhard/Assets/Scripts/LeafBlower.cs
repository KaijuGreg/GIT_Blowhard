using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class LeafBlower : MonoBehaviour
{
    [SerializeField] private float blowRadius = 5f;
    //[SerializeField] float windForce = 10f;
    [SerializeField] float windStrength = 10f;
    [SerializeField] bool isBlowing = false;

    public Transform blowerNozzle;
       

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.E)) { //GetKey for holding down button. Turns on the Blower.
            Debug.Log("button down");
            isBlowing = true;
        }
        else {
            isBlowing = false;
        }

    }

    private void FixedUpdate() {
        if (isBlowing) {

            BlowWind();
        }
    }

    void BlowWind() {
        Vector3 blowDirection = transform.forward;

        Collider[] hits = Physics.OverlapSphere(transform.position, blowRadius);
        foreach (Collider hit in hits) {
            LeafBlowable leaf = hit.GetComponent<LeafBlowable>();
            if (leaf != null) {
                leaf.ApplyWindForce(blowDirection, windStrength);
            }
        }
    }


    /*
    void BlowWind() {
        
        Vector3 blowDirection = transform.forward;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, blowRadius);

        foreach (var hit in hitColliders) {
            LeafBlowable leaf = hit.GetComponent<LeafBlowable>();
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
                Vector3 directionToLeaf = (hit.transform.position - transform.position).normalized; //Finds the direction from the blower to the leaf
                float forceAmount = Mathf.Max(0, Vector3.Dot(directionToLeaf, blowDirection)) * windForce;// Finds how much the object is in front of the blower
                rb.AddForce(blowDirection * forceAmount, ForceMode.Force);
                leaf.ApplyWindForce (blowDirection, forceAmount);
            }
        }


    }
    */

    void OnDrawGizmosSelected() {
        
        Vector3 blowDirection = transform.forward;

        // Draw the detection radius
        Gizmos.color = new Color(0f, 0.8f, 1f, 0.3f); // Light blue
        Gizmos.DrawSphere(transform.position, blowRadius);

        // Draw the wind direction arrow
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, blowDirection * blowRadius);

        // Optional: cone visualization
        float coneAngle = 45f;
        int segments = 16;
        Quaternion rotation = Quaternion.LookRotation(blowDirection);
        Vector3 origin = transform.position;

        Gizmos.color = new Color(0f, 1f, 1f, 0.15f); // Faint cyan

        for (int i = 0; i < segments; i++) {
            float angle = (360f / segments) * i;
            Quaternion segRot = rotation * Quaternion.Euler(0, angle, 0);
            Vector3 dir = segRot * Quaternion.Euler(coneAngle, 0, 0) * Vector3.forward;
            Gizmos.DrawRay(origin, dir.normalized * blowRadius);
        }
    }




}
