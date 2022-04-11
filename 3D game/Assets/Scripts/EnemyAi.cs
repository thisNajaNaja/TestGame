using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public float Distance;
    [SerializeField] private float _detectionDistance;
    [SerializeField] private float _damage;
    [SerializeField] private float _rechargeTime;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform[] _points;

    private NavMeshAgent _navMeshAgent;
    private Transform _transform;
    private float _recharge;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        if (_points != null)
            _navMeshAgent.SetDestination(_points[0].position);

    }

    [System.Obsolete]
    private void Update()
    {
        _recharge = _recharge - Time.deltaTime;
        Detection();

    }

    [System.Obsolete]
    private void Attack(RaycastHit hit)
    {

        if (_navMeshAgent.remainingDistance < 1f)
        {
            _animator.SetTrigger("Attack");
            hit.collider.gameObject.TryGetComponent<Health>(out Health playerHealth);
            playerHealth.Decrease(_damage);
            _recharge = _rechargeTime;
        }
    }

    [System.Obsolete]
    private void Detection()
    {

        Vector3 playerPositionSight = new Vector3(_player.position.x - _transform.position.x, _player.position.y - _transform.position.y, _player.position.z - _transform.position.z);

        RaycastHit sightHit;
        Physics.Raycast(_transform.position, playerPositionSight, out sightHit);

        float distance = Vector3.Distance(_transform.position, _player.position);

        Distance = distance;

        if (sightHit.collider.gameObject.GetComponent<PlayerClickMovement>() &&
            distance < 4f)
        {
            Debug.DrawRay(_transform.position, playerPositionSight, Color.green);
            _navMeshAgent.SetDestination(_player.transform.position);
        }
        else 
        {
            Debug.DrawRay(_transform.position, playerPositionSight, Color.red);
            _navMeshAgent.SetDestination(_points[Random.RandomRange(0, _points.Length)].position);
        }

        if (!_navMeshAgent.SetDestination(_player.transform.position))
        {
            
            if (_navMeshAgent.remainingDistance < 1f)
            {
                _navMeshAgent.SetDestination(_points[Random.RandomRange(0, _points.Length)].position);
            }
        }

        if (_recharge <= 0 && distance < 1.5f && _navMeshAgent.SetDestination(_player.transform.position))
        {
            Attack(sightHit);
        }




    }
}
