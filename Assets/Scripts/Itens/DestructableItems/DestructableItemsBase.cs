using UnityEngine;
using DG.Tweening;

public class DestructableItemsBase : MonoBehaviour
{
    public HealthBase healthBase;

    public float shakeDuration = 2f;
    public int shakeForce = 15;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamaged += OnDamage;
    }

    private void OnDamage (HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up * 2, shakeForce);
    }


}
