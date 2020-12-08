using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{   
    public static CameraShake Instance {get; private set;}
    private CinemachineVirtualCamera cinemachine;
    private float shakeTime;

    private void Awake() 
    {
        Instance = this;
        cinemachine = GetComponent<CinemachineVirtualCamera>();   
    }

    private void Update() 
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if(shakeTime <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasic = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasic.m_AmplitudeGain = 0f;
            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasic = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasic.m_AmplitudeGain = intensity;
        shakeTime = time;
    }

}
