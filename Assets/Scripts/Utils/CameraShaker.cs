using UnityEngine;
using Unity.Cinemachine;
using EDGEE.Core.Singleton;

[RequireComponent(typeof(CinemachineCamera))]
public class CameraShaker : Singleton<CameraShaker>
{
    [Header("Configuração padrão do shake")]
    [SerializeField] private float defaultAmplitude = 2f;
    [SerializeField] private float defaultFrequency = 2f;
    [SerializeField] private float defaultDuration = 0.2f;

    private CinemachineCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private float shakeTimer;

    protected override void Awake()
    {
        base.Awake();

        virtualCamera = GetComponent<CinemachineCamera>();
        noise = virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void OnEnable()
    {
        StopShake();
    }

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
        if (noise == null) return;

        noise.AmplitudeGain = amplitude;
        noise.FrequencyGain = frequency;
        shakeTimer = duration;
    }

    private void StopShake()
    {
        if (noise == null) return;

        noise.AmplitudeGain = 0f;
        noise.FrequencyGain = 0f;
    }
}