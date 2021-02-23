using UnityEngine;
using UnityEngine.InputSystem;



namespace Character
{
    public class TPS_Movement : MonoBehaviour
    {


        public float WalkSpeed;
        public float RunSpeed;
        public float JumpForce;
        
        
        float CurrentSpeed;



        PlayerController PController;
        Animator PlayerAnimator;
        Rigidbody rbRef;

        Vector2 InputVector;
        Vector3 MoveDirection;




        // animator hashes
        public readonly int MovementXHash = Animator.StringToHash("MovementX");
        public readonly int MovementYHash = Animator.StringToHash("MovementY");
        public readonly int RunHash = Animator.StringToHash("Running");
        public readonly int JumpHash = Animator.StringToHash("Jumping");





        void Start()
        {
            // set ref
            PController = GetComponent<PlayerController>();
            PlayerAnimator = GetComponent<Animator>();
            rbRef = GetComponent<Rigidbody>();

            // WalkSpeed = 3.0f;
            // RunSpeed = 5.0f;
            // JumpForce = 1.0f;

            PController.IsJumping = false;
            PController.IsRunning = false;
        }


        void Update()
        {
            // if is jumping dont move
            if(PController.IsJumping) return;
            // if no input dont move
            if(InputVector.magnitude <= 0) return;


            // determine walking or running 
            if (PController.IsRunning)
            {
                CurrentSpeed = RunSpeed;
            }
            else
            {
                CurrentSpeed = WalkSpeed;
            }



            // determine player direction
            MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;

            // make movement vector
            Vector3 movement = MoveDirection * (CurrentSpeed * Time.deltaTime);

            // apply movement
            transform.position += movement;
            
        }




        // wasd input
        public void OnMovement(InputValue input)
        {

            // get input vector from input value
            InputVector = input.Get<Vector2>();




            // set animation for animator
            PlayerAnimator.SetFloat(MovementXHash, InputVector.x);
            PlayerAnimator.SetFloat(MovementYHash, InputVector.y);


        }





        // sprint input
        public void OnRun(InputValue input)
        {
            if (input.Get().ToString()=="1")
            {
                PController.IsRunning = true;
                PlayerAnimator.SetBool(RunHash, PController.IsRunning);
            }
            else
            {
                PController.IsRunning = false;
                PlayerAnimator.SetBool(RunHash, PController.IsRunning);
            }
        }








        private void OnCollisionEnter(Collision other)
        {

            // do nothing if not jumping
            if (!other.gameObject.CompareTag("Ground") && !PController.IsJumping) return;

            // stop jumping if jumping 
            PController.IsJumping = false;
            PlayerAnimator.SetBool(JumpHash, false);



        }




        public void OnJump(InputValue input)
        {
            print(input.Get());


            PController.IsJumping = input.isPressed;
            PlayerAnimator.SetBool(JumpHash, input.isPressed);

            rbRef.AddForce((transform.up + MoveDirection) * JumpForce, ForceMode.Impulse);
            
        }


    }
}
