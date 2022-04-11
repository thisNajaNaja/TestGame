using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMove : MonoBehaviour
{
    [SerializeField] private Animation _animationMove;
    private Animator _animator;

    //параметры движения
    [SerializeField] private float _speedMove = 9f;
    [SerializeField] private float _jumpPower = 10f;
    [SerializeField] private float _rotatePower = 0.01f;

    //параметры гравитации
    private float _gravityForce;
    private Vector3 _moveVector;


    //кнопки управления на экране
    [SerializeField] private FixedJoystick _fixedJoystick;

    //определение контроллера
    private CharacterController _chСontroller;

    void Start()
    {
        _chСontroller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    void Update()
    {
        CharacterMove();
        GameGravity();
  

    }

    //метод движения
    private void CharacterMove()
    {

        _moveVector = Vector3.zero;
        _moveVector.x = _fixedJoystick.Direction.x * _speedMove;

        if (_moveVector.x > 0f) _animator.enabled = true;
        if (_moveVector.x < 0f) _animator.enabled = true;

        if (_moveVector.x == 0f) _animator.enabled = false;
       

        //разворот персонажа в направлении движения
        if (Vector3.Angle(Vector3.forward, _moveVector) > 1f || Vector3.Angle(Vector3.forward, _moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, _speedMove*_rotatePower, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
            
        }


        _moveVector.y = _gravityForce;
        _chСontroller.Move(_moveVector * Time.deltaTime);
    }

    //метод гравитации в игре для персонажа
    private void GameGravity()
    {
        if (!_chСontroller.isGrounded)
        {
            _gravityForce -= 20f * Time.deltaTime;
          
        }
        else
        {
            _gravityForce = -2;
        }
    }

   
}