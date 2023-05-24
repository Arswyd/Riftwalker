using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 2f;
    [SerializeField] float shakeAmplitude = 2f;
    [SerializeField] float shakeFrequency = 2f;

    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noisePerlin;

    void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        noisePerlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        noisePerlin.m_AmplitudeGain = shakeAmplitude;
        noisePerlin.m_FrequencyGain = shakeFrequency;

        yield return new WaitForSeconds(shakeDuration);

        noisePerlin.m_AmplitudeGain = 0;
        noisePerlin.m_FrequencyGain = 0;
    }
}
