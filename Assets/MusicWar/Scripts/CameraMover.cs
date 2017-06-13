﻿using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CameraMover : MonoBehaviour {
    [SerializeField]
    GameObject target;
    IInputProvider _input;

    Vector3 _center = new Vector3(0, 1.6f, 0);
    const float _radius = 3;
    const float _cameraSpeed = -10;

    private void Awake()
    {
        _input = target.GetComponent<IInputProvider>();
        transform.position = target.transform.position + _center + new Vector3(0, 0, -_radius);
    }

    void Start ()
    {
        _input.CameraMove
            .Where(move => transform.localEulerAngles.z <= 90 && (_cameraSpeed * move + transform.localEulerAngles.x < 90 || 270 < _cameraSpeed * move + transform.localEulerAngles.x) )
            .Subscribe(move =>transform.Rotate(_cameraSpeed*move,0,0));

        _input.CameraMove
            .Select(_ => new Vector3(0, _radius * Mathf.Sin(Mathf.PI - transform.localEulerAngles.x * Mathf.Deg2Rad), _radius * Mathf.Cos(Mathf.PI - transform.localEulerAngles.x * Mathf.Deg2Rad)) )
            .Subscribe(position => transform.localPosition = _center + position);
    }
}