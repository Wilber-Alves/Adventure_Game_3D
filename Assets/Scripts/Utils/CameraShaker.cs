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
    private CinemachineBasicMultiChannelPerlin activeNoise;
    private ICinemachineCamera lastLiveChild;

    private void Update()
    {
        // Detecta troca de vcam ativa (RUN -> IDLE -> DEATH etc)
        if (stateDrivenCamera != null)
        {
            var currentLive = stateDrivenCamera.LiveChild;
            if (currentLive != lastLiveChild)
            {
                ForceStopShake();
                lastLiveChild = currentLive;
            }
        }

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

        // Se já havia shake em outra vcam, zera antes de trocar
        if (activeNoise != null && activeNoise != noise)
        {
            activeNoise.AmplitudeGain = 0f;
            activeNoise.FrequencyGain = 0f;
        }

        activeNoise = noise;
        activeNoise.AmplitudeGain = amplitude;
        activeNoise.FrequencyGain = frequency;
        shakeTimer = duration;
    }

    private void StopShake()
    {
        if (activeNoise == null) return;

        activeNoise.AmplitudeGain = 0f;
        activeNoise.FrequencyGain = 0f;
        activeNoise = null;
        shakeTimer = 0f;
    }

    // Usado quando a vcam troca no meio de um shake: cancela sem depender do noise atual
    private void ForceStopShake()
    {
        if (activeNoise != null)
        {
            activeNoise.AmplitudeGain = 0f;
            activeNoise.FrequencyGain = 0f;
            activeNoise = null;
        }
        shakeTimer = 0f;
    }

    private CinemachineBasicMultiChannelPerlin GetActiveNoise()
    {
        if (stateDrivenCamera == null) return null;

        var liveChild = stateDrivenCamera.LiveChild as CinemachineCamera;
        if (liveChild == null) return null;

        return liveChild.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }
}