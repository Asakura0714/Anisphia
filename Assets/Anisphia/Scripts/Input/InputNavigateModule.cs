using Anis.Input;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputNavigateModule : UIInputModuleBase, IDirectionalInput
{
    public InputNavigateModule(InputAction inputAction) : base(inputAction)
    {
        //ˆ—‚È‚µ
    }

    public override bool IsPressed()
    {
        Vector2 value = InputAction.ReadValue<Vector2>();

        // “ü—Í‚ª’´‚¦‚Ä‚¢‚ê‚Î
        return value.magnitude > AnisphiaDefine.Input.DEADZONE;
    }

    /// <summary>
    /// “ü—Í‚µ‚½•ûŒü‚ğæ“¾
    /// </summary>
    /// <returns></returns>
    public EUIDirectionalType GetDirectionalType()
    {
        Vector2 value = InputAction.ReadValue<Vector2>();

        // ƒfƒbƒhƒ][ƒ“‚ğ’´‚¦‚Ä‚¢‚é‚©
        if (value.magnitude < AnisphiaDefine.Input.DEADZONE)
        {
            return EUIDirectionalType.None;
        }

        // “|‚ê‚Ä‚¢‚é²‚Ì”äŠr
        if (Mathf.Abs(value.x) > Mathf.Abs(value.y))
        {
            // ‰¡•ûŒü‚ª‹­‚¢
            return value.x > 0 ? EUIDirectionalType.Right : EUIDirectionalType.Left;
        }
        else
        {
            // c•ûŒü‚ª‹­‚¢
            return value.y > 0 ? EUIDirectionalType.Up : EUIDirectionalType.Down;
        }
    }
}
