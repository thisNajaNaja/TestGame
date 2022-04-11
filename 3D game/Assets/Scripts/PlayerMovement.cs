using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _jumpForce;
    //[SerializeField] private Animator _animator;

    private float _gravityForce;
    private Vector3 _moveVector;
    private Transform _transform;
    

    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
        Gravity();

        //_animator.SetFloat("HorizontalMove", Mathf.Abs(_fixedJoystick.Direction.x));
        //_animator.SetFloat("VerticalMove", Mathf.Abs(_fixedJoystick.Direction.y));

    }

    private void Move()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = Input.GetAxis("Horizontal") + _fixedJoystick.Direction.x * _speedMove;
        _moveVector.z = Input.GetAxis("Vertical") + _fixedJoystick.Direction.y * _speedMove;

        if (Vector3.Angle(Vector3.forward, _moveVector)>1f || Vector3.Angle(Vector3.forward, _moveVector) == 0f)
        {
            Vector3 direct = Vector3.RotateTowards(_transform.forward, _moveVector, _speedMove, 0.0f);
            _transform.rotation = Quaternion.LookRotation(direct);
        }

        _moveVector.y = _gravityForce;


        _characterController.Move(_moveVector * Time.deltaTime);
    }

    private void Gravity()
    {
        if (!_characterController.isGrounded)
            _gravityForce -= 23f * Time.deltaTime;
        else
            _gravityForce = -1f;

        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
            _gravityForce = _jumpForce;
    }

    public void Jump()
    {
        

        if (_characterController.isGrounded)
        {
            _gravityForce = _jumpForce;
            _characterController.Move(_moveVector * Time.deltaTime);
        }
            
    }

}
