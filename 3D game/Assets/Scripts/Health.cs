using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
    [SerializeField] private float _units;
    [SerializeField] private SkinnedMeshRenderer _model;
    public float Units => _units;


    public void Decrease(float amount)
    {
        _units = _units - amount;
        _model.material.DOColor(Color.red, 0.5f).From();

        if (_units <= 0)
            Death();
    }

    public void Increase(float amount)
    {
        _units = _units + amount;
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }
}
