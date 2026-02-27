using UnityEngine;

namespace Anis.UI.Setting
{
    public class SettingSoundPresenter : MonoBehaviour
    {
        [SerializeField]Å@private SettingSoundView _view;

        private SettingSoundModel _model;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _model = new SettingSoundModel();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
