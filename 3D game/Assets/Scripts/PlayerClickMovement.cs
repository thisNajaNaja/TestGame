using UnityEngine;
using UnityEngine.AI;

public class PlayerClickMovement : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Animator _animator; 

    private Camera _mainCamera;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent<Platform>(out Platform platform))
                {
                    _target.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                    _target.SetActive(true);

                    _navMeshAgent.SetDestination(_target.transform.position);
                }

                if (hit.collider.gameObject.TryGetComponent<Attack>(out Attack attack))
                {
                    attack.Planting();
                }
            }
  
        }

        _animator.SetFloat("distance", _navMeshAgent.remainingDistance);
    }
}
