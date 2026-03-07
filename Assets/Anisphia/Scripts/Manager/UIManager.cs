using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase
{
    private const string SCENE_CANVAS_TAG_NAME = "SceneCanvas";
    private const string DIALOG_CANVAS_TAG_NAME = "DialogCanvas";

    private Canvas _sceneCanvas;
    private Canvas _dialoagCanvas;

    private TransitionController _transitionController;

    private Stack<AnisphiaUIWindowBase> _screenStack = new();


    public override void Setup()
    {
        //Instantiate()

        //FindCanvas();

        //LoadTransition().Forget();
    }
    public override void OnUpdate()
    {
        //Debug.Log($"現在のストック数 : {_screenStack.Count}");
    }
    public override void OnDelete()
    {
        if (_sceneCanvas != null)
        {
            _sceneCanvas = null;
        }

        if (_dialoagCanvas != null)
        {
            _dialoagCanvas = null;
        }
    }
}
