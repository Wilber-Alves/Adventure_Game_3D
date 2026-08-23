using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIWeaponUpdater : MonoBehaviour
{
    public Image uiImage;

    [Header("Animation")]
    public float animationDuration = 0.5f;
    public Ease animationEase = Ease.OutBack;

    private Tween currentTween;

    private void OnValidate()
    {
        if (uiImage == null) GetComponent<Image>();
     
    }
    
    public void UpdateValue(float f)
    { 
        uiImage.fillAmount = f;
        
    }

    public void UpdateValue(float max, float current)
    {
        // Update the fill amount based on the current and max values
        // uiImage.fillAmount = 1 - (current / max);
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }
        currentTween = uiImage.DOFillAmount(1 - (current / max), animationDuration).SetEase(animationEase);
    }

}


