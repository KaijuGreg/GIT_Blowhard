using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 7f;

   private bool isWalking;

    private void Update() {

        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized; // this gives us pure direction for the Vector2, if two directions are input i.e. diagonals
        
        Vector3 moveDir = new Vector3 (inputVector.x, 0f, inputVector.y); // here we are creating a Vector3 from our Vector2 inputVector, and also making the that control the XZ plane
        transform.position += moveDir * moveSpeed * Time.deltaTime; // Time.deltaTime means that it runs based on time irrespective of framerate, but on Time.

        isWalking = moveDir != Vector3.zero; //sets the boolean to show state of walking for animation

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); //provides rotation of Player, Slerp provides smooth rotation.


    }


    public bool IsWalking() {
        return isWalking;
    }


}

