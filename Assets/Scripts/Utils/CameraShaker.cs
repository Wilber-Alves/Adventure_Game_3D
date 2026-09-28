using UnityEngine;
using Unity.Cinemachine;
using EDGEE.Core.Singleton;

public class CameraShaker : Singleton<CameraShaker>
{
    [Header("Configuração padrão do shake")]
    [SerializeField] private float defaultAmplitude = 2f;
    [SerializeField] private float defaultFrequency = 2f;
    [SerializeField] private float defaultDuration = 0.2f;

    [Header("Referência")]
    [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

    private float shakeTimer;

    private void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
            {
                StopShake();
            }
        }
    }

    public void Shake()
    {
        Shake(defaultAmplitude, defaultFrequency, defaultDuration);
    }

    public void Shake(float amplitude, float frequency, float duration)
    {
        var noise = GetActiveNoise();
        if (noise == null) return;

        noise.AmplitudeGain = amplitude;
        noise.FrequencyGain = frequency;
        shakeTimer = duration;
    }

    private void StopShake()
    {
        var noise = GetActiveNoise();
        if (noise == null) return;

        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
    }

    private CinemachineBasicMultiChannelPerlin GetActiveNoise()
    {
        if (stateDrivenCamera == null) return null;

        var liveChild = stateDrivenCamera.LiveChild as CinemachineCamera;
        if (liveChild == null) return null;

        return liveChild.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }
}