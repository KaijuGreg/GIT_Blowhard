using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class LeafBlower : MonoBehaviour
{
    [SerializeField] private float blowRadius = 5f;
    [SerializeField] float blowForce = 0.05f;
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

        Vector3 blowDirection = transform.forward; // here is where the direction of the blower is assigned

        Collider[] hits = Physics.OverlapSphere(transform.position, blowRadius);
        foreach (Collider hit in hits) {
            LeafBlowable leaf = hit.GetComponent<LeafBlowable>();
            if (leaf != null) {
                leaf.ApplyWindForce(blowDirection, blowForce);
            }
        }
    }


    // Debug Gizmos for Leaf Blower
    void OnDrawGizmosSelected() {
        
        Vector3 blowDirection = transform.forward;
        Vector3 origin = transform.position;// define origin as the current object's position

        // Draw the detection radius
        Gizmos.color = new Color(0f, 0.8f, 1f, 0.3f); // Light blue
        Gizmos.DrawSphere(transform.position, blowRadius);

        // Draw the wind direction arrow
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, blowDirection * blowRadius);

        

    }




}
