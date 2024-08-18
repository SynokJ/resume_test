using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float _CAMERA_MOVEMENT_SPEED = 3.0f;
    private Transform _targetTr;
    private Vector3 _posOffset = new Vector3(0.0f, 0.0f, -10.0f);

    void Update()
    {
        if (_targetTr == null)
            return;

        Vector3 targetPos = _targetTr.position + _posOffset;
        float speed = _CAMERA_MOVEMENT_SPEED * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, targetPos, speed);
    }

    public void SetTargetTr(Transform targetTr)
        => _targetTr = targetTr;
}
