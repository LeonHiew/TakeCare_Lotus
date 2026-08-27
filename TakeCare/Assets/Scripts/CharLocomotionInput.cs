using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace TakeCare.CharController
{
    public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
    {
        public MainController MainController { get; private set ;}MainController
        public Vector2 MovementInput { get; private set; }

        private void OnEnable()
        {
            MainController = new MainController();
            MainController.Enable();

            MainController.PlayerLocomotionMap.Enable();
            MainController.PlayerLocomotionMap.SetCallbacks(this);
        }

        private void OnDisable()
        {
            MainController.PlayerLocomotionMap.Disable();
            MainController.PlayerLocomotionMap.RemoveCallbacks(this);
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
            print(MovementInput);
        }
    }
}


