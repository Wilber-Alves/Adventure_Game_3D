using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DestructableItemsBase : MonoBehaviour
{
    public HealthBase healthBase;

    public float shakeDuration = 2f;
    public int shakeForce = 15;

    public int dropCoinsAmount = 10;
    public GameObject PFB_Coin;
    public Transform DropItem;

    private Coroutine dropCoroutine;

    private void Awake()
    {
        if (healthBase == null)
            healthBase = GetComponent<HealthBase>();

        if (healthBase != null)
            healthBase.OnDamaged += OnDamage;
    }

    private void OnDamage(HealthBase h)
    {
        if (this == null || !gameObject.activeInHierarchy)
            return;

        transform.DOKill();

        transform
            .DOShakeScale(
                shakeDuration,
                Vector3.up * 2f,
                shakeForce
            )
            .SetLink(gameObject);

        if (dropCoroutine != null)
            StopCoroutine(dropCoroutine);

        dropCoroutine = StartCoroutine(DropGroupOfCoinsCoroutine());
    }

    private void DropCoins()
    {
        if (PFB_Coin == null || DropItem == null)
            return;

        GameObject coin = Instantiate(PFB_Coin);
        coin.transform.position = DropItem.position;
    }

    private IEnumerator DropGroupOfCoinsCoroutine()
    {
        for (int i = 0; i < dropCoinsAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(0.1f);
        }

        dropCoroutine = null;
    }

    private void OnDestroy()
    {
        transform.DOKill();

        if (healthBase != null)
            healthBase.OnDamaged -= OnDamage;

        if (dropCoroutine != null)
            StopCoroutine(dropCoroutine);
    }
}