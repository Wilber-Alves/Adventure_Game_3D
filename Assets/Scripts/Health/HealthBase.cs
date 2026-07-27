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

    public UILifeUpdater uiLifeUpdater;

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
        // MODIFICAÇÃO: Adicionado UpdateUI(); aqui.
        // MOTIVO: Garante que a barra de vida comece cheia (100%) logo no início do jogo, antes mesmo de sofrer dano.
        UpdateUI();
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

        // MODIFICAÇÃO ANTERIOR: _currentLife -= damage;
        // NOVA MODIFICAÇÃO: Uso do Mathf.Clamp para limitar o valor entre 0 e startLife.
        // MOTIVO: Evita que a vida fique negativa (ex: -5), o que quebraria a lógica visual de preenchimento (fillAmount) da UI.
        _currentLife = Mathf.Clamp(_currentLife - damage, 0, startLife);

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamaged?.Invoke(this);
    }
    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    public void OnDamage(float damage)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if (uiLifeUpdater != null)
        {
            // MODIFICAÇÃO ANTERIOR: uiLifeUpdater.UpdateValue((int) _currentLife / startLife);
            // NOVA MODIFICAÇÃO: uiLifeUpdater.UpdateLife(_currentLife / startLife);
            // MOTIVO 1: O cast para '(int)' transformava a vida atual em um número inteiro antes da divisão. Se a vida fosse 9.5f, ela virava 9. Se fosse menor que 'startLife' (ex: 9 / 10), o resultado em C# seria truncado para 0, esvaziando a barra imediatamente no primeiro dano.
            // MOTIVO 2: Mudança do método para 'UpdateLife' (ou o nome que você definiu no novo script de vida) para enviar a divisão direta em float (gerando um valor correto entre 0.0f e 1.0f).

            float percentage = _currentLife / startLife;
            uiLifeUpdater.UpdateLife(percentage);
        }
    }
}