using UnityEngine.InputSystem;

namespace Anis.Input
{
    public abstract class UIInputModuleBase
    {
        public InputAction InputAction { get; private set; }
        public UIInputModuleBase(InputAction inputAction)
        {
            InputAction = inputAction;
        }
        public void SetInputAction(InputAction inInputAction)
        {
            InputAction = inInputAction;
        }
        public void DeleteInputAction()
        {
            if (InputAction != null)
            {
                InputAction = null;
            }
        }

        public virtual void Update()
        {
        }

        public virtual bool IsPressed()
        {
            return InputAction.IsPressed();
        }
        public virtual bool IsReleased()
        {
            return InputAction.WasReleasedThisFrame();
        }
        public virtual bool IsHold()
        {
            return false;
        }
    }
}
    