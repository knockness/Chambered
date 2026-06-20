using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraShakeScript : MonoBehaviour
{
    [Header("Cinemachine Component Access Variables")]
    public CinemachineCamera attachedCamera;
    public CinemachineBasicMultiChannelPerlin perlinNoise;

    [Header("Testing Variables")]
    public bool trigger;
    public float intensity;
    public float velocity;
    public int time;

    void Awake()
    {
        perlinNoise.AmplitudeGain = 0f; //How far the camera shakes.
        perlinNoise.FrequencyGain = 0f; //How fast the camera shakes.

        trigger = false;
    }

    void Update()
    {
        if(trigger == true) //Trigger used soley for testing.
        {
            trigger = false;
            StartCoroutine(CameraShakeCorutine(intensity, velocity, time));
        }
    }

    public IEnumerator CameraShakeCorutine(float cameraShakeIntensity, float cameraShakeVelocity, int cameraShakeDuration)
    {
        for (int elapsedFrames = 0; elapsedFrames < cameraShakeDuration; elapsedFrames++)
        {
            perlinNoise.AmplitudeGain = cameraShakeIntensity;
            perlinNoise.FrequencyGain = cameraShakeVelocity;
            yield return new WaitForFixedUpdate();
        }

        perlinNoise.AmplitudeGain = 0f;
        perlinNoise.FrequencyGain = 0f;
    }
}
