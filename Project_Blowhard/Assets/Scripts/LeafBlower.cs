using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class LeafBlower : MonoBehaviour
{

    public Transform BlowerNozzle;
    public float windForce = 10f;
    bool isBlowing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E)) { //GetKey for holding down button
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
        float blowRadius = 5f;
        Vector3 blowDirection = transform.forward;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, blowRadius);

        foreach (var hit in hitColliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
                Vector3 directionToLeaf = (hit.transform.position - transform.position).normalized;
                float forceAmount = Mathf.Max(0, Vector3.Dot(directionToLeaf, blowDirection)) * windForce;
                rb.AddForce(blowDirection * forceAmount, ForceMode.Impulse);
            }
        }
    }



}
