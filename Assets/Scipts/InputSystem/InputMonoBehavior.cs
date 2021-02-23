using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace InputMonoBehaviorParent
{
    public class InputMonoBehavior : MonoBehaviour
    {

        protected PlayerInputActions GameInput;


        protected void Awake()
        {
            GameInput = new PlayerInputActions();
        }


        protected void OnEnable()
        {
            GameInput.Enable();
        }


        protected void OnDisable()
        {
            GameInput.Disable();
        }


    }
    
}
