    using UnityEngine;

namespace BHS
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        public override bool RetrieveJumpInput()
        {
            return Input.GetButtonDown("Jump");
        }

        public override float RetrieveMoveInput()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public override bool RetrieveDashInput()
        {
            return Input.GetKeyDown(KeyCode.LeftShift);
        }

        public override bool RetrieveUseInput()
        {
            return Input.GetKeyDown(KeyCode.E);
        }
    }
}
