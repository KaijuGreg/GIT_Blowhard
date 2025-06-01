using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;  // Here we assign a GameInput class
    [SerializeField] private LayerMask collisionMask;

    public Transform leafBlowerPrefab;
    public Transform handTransform;
    
    private bool isWalking;
    private Vector3 lastInteractDir; // this stores the last moveDir for HandleInteractions()

   
    private void Awake() {

       
    }


    private void Start() {

      

        // Instantiate the leaf blower at the hand's position and rotation
        Transform leafBlower = Instantiate(leafBlowerPrefab, handTransform.position, handTransform.rotation);

        // Make it a child of the hand so it follows movement
        leafBlower.transform.SetParent(handTransform);

        // Reset local transform to make sure it's not skewed, although seems to work withou these...
        leafBlower.transform.localPosition = Vector3.zero;
        leafBlower.transform.localRotation = Quaternion.identity;
        leafBlower.transform.localScale = Vector3.one;
   


    }

    private void Update() {

        HandleMovement();
        HandleInteractions();   

    }


    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) { // this is for ensuring that Interactions are still working even we are not moving

            lastInteractDir = moveDir; // this saves into lastInteractDir to be used when not moving below

        }

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance)) {

            Debug.Log(raycastHit.transform);

        }
        else {
            Debug.Log("_");
        }

    }


    private void HandleMovement() {


        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); // here we are creating a Vector3 from our Vector2 inputVector, and also making the that control the XZ plane

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        // Capsule cast test is a bool that returns true, so when it is NOT (!) true we CANMOVE
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance, collisionMask); // detecting for player collision


        // This below solves the problem of Moving Diagonally on flat wall, you expect to be able to move across it.
        //If we can not move

        if (!canMove) {
            // cannot move towards moveDir


            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance, collisionMask);

            // If we can move ... 
            if (canMove) {
                //...then can move only on the X
                moveDir = moveDirX;
            }
            else {
                //Cannot move only on the X

                //Attempt only Z movement

                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance, collisionMask);

                if (canMove) {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                }
                else {
                    // Cannot move in any direction
                }
            }

        }

        if (canMove) {
            transform.position += moveDir * moveSpeed * Time.deltaTime; // Time.deltaTime means that it runs based on time irrespective of framerate, but on Time.
        }

        isWalking = moveDir != Vector3.zero; //sets the boolean to show state of walking for animation

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); //provides rotation of Player, Slerp provides smooth rotation.


    }






    // -------------------------------------------------

    private void OnDrawGizmosSelected() {
        
        //Player Gizmos colour
        Gizmos.color = Color.red;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        float distance = 1f;

        Gizmos.DrawRay(origin, direction * distance);


    }

}

