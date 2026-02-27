using Anis.Input;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputTabModule : UIInputModuleBase, IDirectionalInput
{
    public InputTabModule(InputAction inputAction) : base(inputAction)
    {
        //ˆ—‚È‚µ
    }

    /// <summary>
    /// “ü—Í‚µ‚½•ûŒü‚ğæ“¾
    /// </summary>
    /// <returns></returns>
    public EUIDirectionalType GetDirectionalType()
    {
        var value = InputAction.ReadValue<float>();

        if (value >= 1f)
        {
            return EUIDirectionalType.Right;
        }
        else if (value <= 0)
        {
            return EUIDirectionalType.Left;
        }
        else
        {
            return EUIDirectionalType.None;
        }
    }
}
