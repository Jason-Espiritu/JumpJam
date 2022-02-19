using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _targetToFollow;
    [SerializeField] private Vector3 _cameraOffsetToTarget;
    [SerializeField] private float _cameraSmoothness;

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 newTargetPosition = _targetToFollow.position + _cameraOffsetToTarget;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, newTargetPosition, _cameraSmoothness * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
