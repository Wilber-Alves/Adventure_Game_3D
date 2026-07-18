using System.Collections;   
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]

    public Color color = Color.red;
    public float duration = 0.2f;

    private Material _material;
    private Color _originalColor;

    private Tween _currentTween;


    private void Awake()
    {
        _material = meshRenderer.material;
        _originalColor = _material.GetColor("_EmissionColor");
    }


    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (!_currentTween.IsActive())
        _currentTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
    }
}