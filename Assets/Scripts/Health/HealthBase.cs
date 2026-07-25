using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    [SerializeField] private float _currentLife;
    public bool destroyOnKill = false;
    public float startLife = 10f;

    public bool IsDead { get; private set; } // TESTE, Se nao funcionar, retirar e manter script original

    public Action<HealthBase> OnDamaged; // sao variáveis de controle de feedback
    public Action<HealthBase> OnKilled;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        IsDead = false; // TESTE, Se nao funcionar, retirar e manter script original
        ResetLife();
    
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        if (IsDead) return; // TESTE, Se nao funcionar, retirar e manter script original
        IsDead = true; // TESTE, Se nao funcionar, retirar e manter script original

        if (destroyOnKill)
        {
            Destroy(gameObject, 1.2f);
        }
        OnKilled?.Invoke(this);
    }

    [NaughtyAttributes.Button] // debug de dano
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(float damage)
    {
        if (IsDead) return; // TESTE, Se nao funcionar, retirar e manter script original

        _currentLife -= damage;
        OnDamaged?.Invoke(this);

        if (_currentLife <= 0)
        {
            Kill();
        }
    }
    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    public void OnDamage(float damage)
    {
        Damage(damage);
    }
}
