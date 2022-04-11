using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject _bomb;
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    public void Planting()
    {
        Bomb bomb = _bomb.GetComponent<Bomb>();
  
        if (bomb.IsUsed == false)
        {
            _bomb.transform.position = new Vector3(_transform.position.x, 0.3f, _transform.position.z);
            _bomb.SetActive(true);
            bomb.Planting();
        }
        
    }


}
