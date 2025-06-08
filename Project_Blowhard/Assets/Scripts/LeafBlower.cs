using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using System;

public class LeafBlower : MonoBehaviour
{
    [SerializeField] private float blowRadius = 5f;
    [SerializeField] float blowForce = 0.05f;
    [SerializeField] bool isBlowing = false;

    [SerializeField] private GameInput gameInput;

    public Transform blowerNozzle;


    private void Awake()
    {
        gameInput = GameInput.Instance;

    }

    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnFireStarted += StartBlowing;
        gameInput.OnFireCancelled += StopBlowing;

    }

    
    void Update()
    {
     

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


    private void StartBlowing(object sender, EventArgs e)
    {
        isBlowing = true;
        Debug.Log("Fire was pressed here");
    }

    private void StopBlowing (object sender, EventArgs e)
    {
        isBlowing = false;
    }


    private void OnDestroy()
    {
        gameInput.OnFireStarted -= StartBlowing;
        gameInput.OnFireCancelled -= StopBlowing;
    }



    //-----------------------------------------------------------------------------------------------------------------------


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
