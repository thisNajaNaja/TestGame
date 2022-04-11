using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.Decrease(_damage);
            Debug.Log(collision.name);
        }
    }
}
