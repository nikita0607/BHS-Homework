using UnityEngine;

namespace BHS
{
    public abstract class InputController : ScriptableObject
    {
        public abstract float RetrieveMoveInput();
        public abstract bool RetrieveJumpInput();
        public abstract bool RetrieveDashInput();
        public abstract bool RetrieveUseInput();
    }
}
