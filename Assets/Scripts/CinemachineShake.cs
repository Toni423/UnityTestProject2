using System;
using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour {
    private CinemachineVirtualCamera _camera;

    private void Awake() {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void shake(float intensity, float time) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        StartCoroutine(DelayedCoroutine.delayedCoroutine(time,
            () => cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f));
    }
}
