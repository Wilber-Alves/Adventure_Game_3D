using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UILifeUpdater : MonoBehaviour
{
    [Header("UI Reference")]
    // NOVA MODIFICAÇÃO: Substituído pelo componente Slider do Unity.
    public Slider uiSlider;

    [Header("Animation Settings")]
    public float animationDuration = 0.3f;
    public Ease animationEase = Ease.OutQuad;

    private Tween currentTween;

    private void OnValidate()
    {
        // NOVA MODIFICAÇÃO: Ajustado para buscar o componente Slider automaticamente.
        if (uiSlider == null) uiSlider = GetComponent<Slider>();
    }

    public void UpdateLife(float percentage)
    {
        // NOVA MODIFICAÇÃO: Ajustado para verificar o Slider.
        if (uiSlider == null) return;

        // Cancela a animação anterior
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        // Garante que o valor fique estritamente entre 0 e 1
        percentage = Mathf.Clamp01(percentage);

        // NOVA MODIFICAÇÃO: Utilização do DOSlider para animar a barra de forma suave sem borrar as texturas.
        currentTween = uiSlider.DOValue(percentage, animationDuration)
            .SetEase(animationEase);
    }
}