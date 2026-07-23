using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [SerializeField] private float _currentLife;
    public bool destroyOnKill = false;
    public float startLife = 10f;

    public Action<HealthBase> OnDamage; // sao variáveis de controle de feedback
    public Action<HealthBase> OnKill;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
        {
            Destroy(gameObject, 1.2f);
        }
        OnKill?.Invoke(this);

    }

    [NaughtyAttributes.Button] // debug de dano
    public void Damage()
    {
        Damage(5);
    
    }

    public void Damage(float damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            Kill();
        }
        OnDamage?.Invoke(this);
    }

}
