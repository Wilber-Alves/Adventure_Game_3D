using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using UnityEngine.Rendering;

public class ChestBase : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.Z;
    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = 0.2f;
    public Ease tweenEase = Ease.OutBack;

    [Space]
    public ChestItemBase chestItem;


    private float startScale;
    private bool _chestOpened = false;

    private void Start()
    {
        startScale = notification.transform.localScale.x;
        HideNotification();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();

        }

    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_chestOpened) return;

        animator.SetTrigger(triggerOpen);
        _chestOpened |= true;
        HideNotification();
        Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            ShowNotification();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        PlayerController player = other.transform.GetComponent<PlayerController>();
        if (player != null)
        {
            HideNotification();
        }
    }
    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, tweenDuration);
    }
    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.SetActive(false);
    }
}
