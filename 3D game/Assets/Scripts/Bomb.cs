using UnityEngine;
using UnityEngine.Events;


public class Bomb : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private GameObject[] _shockWaves;

    private bool _isUsed = false;
    public UnityEvent _bang;
    private Animator _animator;

    public bool IsUsed => _isUsed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Planting()
    {
        if (_isUsed == false)
        {
            _animator.Play("Detonation");
            _isUsed = true;
        }
       
    }
    public void Detonation() 
    {
        foreach (var item in _shockWaves)
        {
            item.SetActive(true);
        }
        
    }

    public void Bang()
    {
        foreach (var item in _shockWaves)
        {
            item.SetActive(false);
        }
        gameObject.SetActive(false);
        _isUsed = false;


    }
}
