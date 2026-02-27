using UnityEngine;
using System;

public class TankTurretBase : MonoBehaviour
{
    [SerializeField] private Transform _firePos = default;
    [SerializeField] private GameObject _tama;

    public Action OnFireAction;

    public void SetLookAtCursol(Vector3 worldPos)
    {
        transform.LookAt(worldPos);
    }

    public void OnFire()
    {
        Instantiate(_tama, _firePos.position,_firePos.rotation);

        //_firePos.transform.SetParent(null);

        //_firePos.forward;
    }
}
