using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private float _speed;

    private Transform _transform;
    private Vector3 _movementDirection;
    private Vector3 _destination;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        _destination = _transform.position;
    }

    private void Update()
    {
        Flip();
        Move();

    }

    private void Flip()
    {
        if (Vector3.Angle(Vector3.forward, _movementDirection) > 1f || Vector3.Angle(Vector3.forward, _movementDirection) == 0f)
        {
            Vector3 direct = Vector3.RotateTowards(_transform.forward, _movementDirection, _speed * 5f, 0.0f);
            _transform.rotation = Quaternion.LookRotation(direct);
        }
    }

    private void Move() 
    {
        if (Mathf.Abs(_fixedJoystick.Direction.x) > 0.5f)
            _movementDirection.Set(Mathf.Abs(_fixedJoystick.Direction.x) / _fixedJoystick.Direction.x, 0f, 0f);
        if (Mathf.Abs(_fixedJoystick.Direction.y) > 0.5f)
            _movementDirection.Set(0f, 0f, Mathf.Abs(_fixedJoystick.Direction.y) / _fixedJoystick.Direction.y);
        if (Mathf.Abs(_fixedJoystick.Direction.x) == 0f && Mathf.Abs(_fixedJoystick.Direction.y) == 0f)
            _movementDirection = Vector3.zero;

        RaycastHit hit;
        Physics.Raycast(new Ray(_transform.position, _movementDirection), out hit, 1f);
        Debug.DrawRay(_transform.position, _transform.forward, Color.red);

        if (_destination == _transform.position && hit.collider == null)
        {
            _destination = _transform.position + _movementDirection;
        }

        _transform.position = Vector3.MoveTowards(_transform.position, _destination, _speed * Time.deltaTime);
    }
}
