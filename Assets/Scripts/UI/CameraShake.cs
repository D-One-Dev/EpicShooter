using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    [SerializeField] private CinemachineVirtualCamera _camera;
    private float shakeTimer;
    private void Awake()
    {
        instance = this;
    }

    public void ShakeCamera(float intensity = 1.25f, float duration = .1f)
    {
        CinemachineBasicMultiChannelPerlin shake = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shake.m_AmplitudeGain = intensity;
        shakeTimer = duration;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin shake = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                shake.m_AmplitudeGain = 0f;
            }
        }
    }

}
