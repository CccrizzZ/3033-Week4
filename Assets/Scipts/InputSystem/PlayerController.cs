using UnityEngine;
using Character.UI;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        public CrosshairScript chs => CrossHairComponent;
        [SerializeField] private CrosshairScript CrossHairComponent;

        public bool IsFiring;
        public bool IsReloading;
        public bool IsJumping;
        public bool IsRunning;

        


    }
}