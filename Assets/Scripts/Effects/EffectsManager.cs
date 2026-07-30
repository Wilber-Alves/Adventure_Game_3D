using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using EDGEE.Core.Singleton;

[DefaultExecutionOrder(-100)] // garante Awake deste rodar cedo
public class EffectsManager : Singleton<EffectsManager>
{
    public Volume processVolume;
    public float duration = 0.1f;

    [SerializeField] private Vignette _vignette;
    [Tooltip("Intensidade máxima da vinheta durante o flash")]
    [SerializeField] private float vignetteMaxIntensity = 0.45f; // ajuste conforme necessário

    protected override void Awake()
    {
        base.Awake();

        if (processVolume == null)
        {
            Debug.LogError("EffectsManager: processVolume não atribuído.", this);
            return;
        }

        if (processVolume.profile == null)
        {
            Debug.LogError("EffectsManager: processVolume.profile é null.", this);
            return;
        }

        if (!processVolume.profile.TryGet<Vignette>(out _vignette) || _vignette == null)
        {
            Debug.LogWarning("EffectsManager: Vignette não encontrado no profile.", this);
            _vignette = null;
            return;
        }

        // garante overrideState antes de manipular valores
        _vignette.color.overrideState = true;
        _vignette.intensity.overrideState = true;

        // estado inicial: totalmente 'preto' e sem intensidade (invisível)
        _vignette.color.value = Color.black;
        _vignette.intensity.value = 0f;
    }

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        if (_vignette == null)
        {
            Debug.LogWarning("ChangeVignette chamado mas _vignette é null.", this);
            return;
        }

        // interrompe corrotina anterior se houver (evita sobreposição)
        StopCoroutine(nameof(FlashColorVignette));
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        if (_vignette == null)
            yield break;

        float time = 0f;

        // sobe cor + intensidade
        while (time < duration)
        {
            float t = time / duration;
            _vignette.color.value = Color.Lerp(Color.black, Color.red, t);
            _vignette.intensity.value = Mathf.Lerp(0f, vignetteMaxIntensity, t);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _vignette.color.value = Color.red;
        _vignette.intensity.value = vignetteMaxIntensity;

        // volta ao normal (desvanece)
        time = 0f;
        while (time < duration)
        {
            float t = time / duration;
            _vignette.color.value = Color.Lerp(Color.red, Color.black, t);
            _vignette.intensity.value = Mathf.Lerp(vignetteMaxIntensity, 0f, t);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // garante estado final invisível
        _vignette.color.value = Color.black;
        _vignette.intensity.value = 0f;
    }
}