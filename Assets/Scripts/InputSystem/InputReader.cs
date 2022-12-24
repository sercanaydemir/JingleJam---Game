using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class InputReader : InputActions.IPlayerActions
    {
        private InputActions _inputActions;
        public bool MovementCanceled;

        public Action meleeAttackLight;
        public Action meleeAttackHeavy;
        public InputReader()
        {
            _inputActions = new InputActions();
            _inputActions.Player.SetCallbacks(this);
            _inputActions.Player.Enable();
        }

        ~InputReader()
        {
            _inputActions.Player.Disable();
        }
        
        public Vector2 MovementVector { get; private set; }
        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementCanceled = context.canceled;
            MovementVector = context.ReadValue<Vector2>();
        }

        public void OnMeleeAttackLight(InputAction.CallbackContext context)
        {
            meleeAttackLight?.Invoke();
        }

        public void OnMeleeAttackHeavy(InputAction.CallbackContext context)
        {
            meleeAttackHeavy?.Invoke();
        }
    }
}