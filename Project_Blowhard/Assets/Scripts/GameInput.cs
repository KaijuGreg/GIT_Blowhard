using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // this allows for Events to work

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction; //this is the delegate that is called when the event occurs
    public event EventHandler OnFireStarted;
    public event EventHandler OnFireCancelled;

    private PlayerInputActions playerInputActions;


    private void Awake() {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;

        playerInputActions.Player.Fire.performed += Fire_performed;
        playerInputActions.Player.Fire.canceled += Fire_cancelled;

    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){

        OnFireStarted?.Invoke(this, EventArgs.Empty);
        

    }

    private void Fire_cancelled (UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnFireCancelled?.Invoke(this, EventArgs.Empty);

    }


    private void OnDestroy()
    {
        playerInputActions.Player.Fire.performed -= Fire_performed;
        playerInputActions.Player.Fire.canceled -= Fire_cancelled;
    
    }



    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
       
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        
       
    }


    public Vector2 GetMovementVectorNormalized() {

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

      
        inputVector = inputVector.normalized; // this gives us pure direction for the Vector2, if two directions are input i.e. diagonals


        return inputVector;

    }





}
