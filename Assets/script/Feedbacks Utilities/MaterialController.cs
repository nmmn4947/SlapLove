using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MaterialController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color glowColor = Color.white;
    [SerializeField] private float maxIntensity = 5f;
    [SerializeField] private float speed = 1f;

    private Material mat;
    private float currentIntensity = 0f;

    void Start()
    {
        mat = image.material;
        mat.EnableKeyword("_EMISSION");
    }


    public void StartGlow()
    { 
        StartCoroutine(GlowCoroutine());
    }

    IEnumerator GlowCoroutine()
    {
        while (currentIntensity < maxIntensity)
        {
            currentIntensity += Time.deltaTime * speed;
            mat.SetColor("_EmissionColor", glowColor * currentIntensity);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0); // Wait for a second before resetting
    }

    public void ResetGlow()
    {
        currentIntensity = 0f;
        mat.SetColor("_EmissionColor", Color.black);
    }
}
