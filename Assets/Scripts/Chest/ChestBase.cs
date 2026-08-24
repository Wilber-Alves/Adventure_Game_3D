using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using UnityEngine.Rendering;

public class ChestBase : MonoBehaviour
{

    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = 0.2f;
    public Ease tweenEase = Ease.OutBack;
    private float startScale;


    private void Start()
    {
        startScale = notification.transform.localScale.x;
        HideNotification();
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        animator.SetTrigger(triggerOpen);
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
