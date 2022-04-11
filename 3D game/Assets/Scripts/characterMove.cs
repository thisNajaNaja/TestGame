using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMove : MonoBehaviour
{
    [SerializeField] private Animation _animationMove;
    private Animator _animator;

    //��������� ��������
    [SerializeField] private float _speedMove = 9f;
    [SerializeField] private float _jumpPower = 10f;
    [SerializeField] private float _rotatePower = 0.01f;

    //��������� ����������
    private float _gravityForce;
    private Vector3 _moveVector;


    //������ ���������� �� ������
    [SerializeField] private FixedJoystick _fixedJoystick;

    //����������� �����������
    private CharacterController _ch�ontroller;

    void Start()
    {
        _ch�ontroller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    void Update()
    {
        CharacterMove();
        GameGravity();
  

    }

    //����� ��������
    private void CharacterMove()
    {

        _moveVector = Vector3.zero;
        _moveVector.x = _fixedJoystick.Direction.x * _speedMove;

        if (_moveVector.x > 0f) _animator.enabled = true;
        if (_moveVector.x < 0f) _animator.enabled = true;

        if (_moveVector.x == 0f) _animator.enabled = false;
       

        //�������� ��������� � ����������� ��������
        if (Vector3.Angle(Vector3.forward, _moveVector) > 1f || Vector3.Angle(Vector3.forward, _moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, _speedMove*_rotatePower, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
            
        }


        _moveVector.y = _gravityForce;
        _ch�ontroller.Move(_moveVector * Time.deltaTime);
    }

    //����� ���������� � ���� ��� ���������
    private void GameGravity()
    {
        if (!_ch�ontroller.isGrounded)
        {
            _gravityForce -= 20f * Time.deltaTime;
          
        }
        else
        {
            _gravityForce = -2;
        }
    }

   
}