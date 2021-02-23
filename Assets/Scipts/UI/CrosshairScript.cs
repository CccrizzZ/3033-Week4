using UnityEngine;
using UnityEngine.InputSystem;
using InputMonoBehaviorParent;

namespace Character.UI
{
    public class CrosshairScript : InputMonoBehavior
    {


        public Vector2 MouseSensitivity;
        public bool Inverted = false;

        public Vector2 CurrentAimPosition {get; private set;}




        // horizontal constrain
        [SerializeField, Range(0,1)]
        private float CrosshairHorizontalPercentage = 0.25f;
        private float HorizontalOffset;
        private float MaxHorizontalDeltaConstrain;
        private float MinHorizontalDeltaConstrain;
        



        // vertical constrain
        [SerializeField, Range(0,1)]
        private float CrosshairVerticalPercentage = 0.25f;
        private float VerticalOffset;
        private float MaxVerticalDeltaConstrain;
        private float MinVerticalDeltaConstrain;
        




        private Vector2 CrosshairStartPosition;
        private Vector2 CurrentLookDeltas;



        void Start()
        {
            if (GameManager.Instance.CursorActive)
            {
                AppEvent.InvokeMouseCursorEnable(false);
            }

            CrosshairStartPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);

            // set horizontal offset
            HorizontalOffset = (Screen.width * CrosshairHorizontalPercentage) / 2f;
            MinHorizontalDeltaConstrain = -(Screen.width / 2f) + HorizontalOffset;
            MaxHorizontalDeltaConstrain = (Screen.width / 2f) - HorizontalOffset;


            // set vertical offset
            VerticalOffset = (Screen.height * CrosshairVerticalPercentage) / 2f;
            MinVerticalDeltaConstrain = -(Screen.height / 2f) + VerticalOffset;
            MaxVerticalDeltaConstrain = (Screen.height / 2f) - VerticalOffset;


            
        }


        private void OnLook(InputAction.CallbackContext delta)
        {
            // print(delta.ReadValue<Vector2>());

            // make new vector2
            Vector2 mouseDelta = delta.ReadValue<Vector2>();
           
           
            // apply mouse movement
            CurrentLookDeltas.x += mouseDelta.x * MouseSensitivity.x;
            // clamp mouse x
            if (CurrentLookDeltas.x >= MaxHorizontalDeltaConstrain || CurrentLookDeltas.x <= MinHorizontalDeltaConstrain)
            {
                CurrentLookDeltas.x -= mouseDelta.x * MouseSensitivity.x;
            }

            

            // apply mouse movement
            CurrentLookDeltas.y += mouseDelta.y * MouseSensitivity.y;
            // clamp mouse x
            if (CurrentLookDeltas.y >= MaxVerticalDeltaConstrain || CurrentLookDeltas.y <= MinVerticalDeltaConstrain)
            {
                CurrentLookDeltas.y -= mouseDelta.y * MouseSensitivity.y;
            }

        }


        private void Update() 
        {

            // make new crosshair x and y
            float crosshairXPosition = CrosshairStartPosition.x + CurrentLookDeltas.x;

            // determine if mouse inverted
            float crosshairYPosition = Inverted 
            ? CrosshairStartPosition.y - CurrentLookDeltas.y 
            : CrosshairStartPosition.y + CurrentLookDeltas.y;
            
            
            
            // set to member variable
            CurrentAimPosition = new Vector2(crosshairXPosition, crosshairYPosition);
            // print(CurrentAimPosition);
           
            // apply crosshair position x and y
            transform.position = CurrentAimPosition;
        
        }



        private new void OnEnable()
        {
            base.OnEnable();
            GameInput.PlayerActionMap.Look.performed += OnLook;
        }


        private new void OnDisable()
        {
            base.OnDisable();
            GameInput.PlayerActionMap.Look.performed -= OnLook;
        }






    }
}
