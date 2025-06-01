using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;


    private void Awake() {

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

    }

    public Vector2 GetMovementVectorNormalized() {

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

      
        inputVector = inputVector.normalized; // this gives us pure direction for the Vector2, if two directions are input i.e. diagonals


        return inputVector;

    }



}
