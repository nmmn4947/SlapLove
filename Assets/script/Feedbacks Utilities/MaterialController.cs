using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

public class MaterialController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float maxIntensity = 5f;
    float feedbackTime = 0.5f; // Time to reach max intensity
    
    private float currentIntensity = 0f;
    private MMF_Player mMF_Player;

    void Start()
    {
        
        mMF_Player = GetComponent<MMF_Player>();
        feedbackTime = mMF_Player.TotalDuration;
        Material newMat = new Material(image.material);
        image.material = newMat;

        currentIntensity = newMat.GetFloat("_GlowAmount");

        newMat.EnableKeyword("_EMISSION");
    }


    public void StartGlow()
    { 
        StartCoroutine(GlowCoroutine());
    }

    IEnumerator GlowCoroutine()
    {
        float timeElapsed = 0;
        float lerpedValue = 0f;
        
        while (timeElapsed < feedbackTime)
        {
            float time = timeElapsed / feedbackTime;
            lerpedValue = Mathf.Lerp(currentIntensity, maxIntensity, time);
            image.material.SetFloat("_GlowAmount", lerpedValue);
            timeElapsed += Time.deltaTime;
            yield return null;
            
        }
        lerpedValue = maxIntensity;
        yield return new WaitForSeconds(0); // Wait for a second before resetting
    }

    public void ResetGlow()
    {
        currentIntensity = 0f;
        image.material.SetColor("_EmissionColor", Color.black);
    }
}
