using System.Collections;
using UnityEngine;

public class playerFightDisplay : MonoBehaviour
{
    Animator animator;
    RectTransform rect;
    Vector3 originalPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        originalPos = rect.localPosition;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSlap()
    {
        StartCoroutine(shake());
        animator.Play("playerSlap");
    }

    public void playHurt()
    {
        StartCoroutine(shake());
        animator.Play("playerHurt");
    }

    public void playMiss()
    {
        animator.Play("playerMiss");
    }

    public void playIdle()
    {
        animator.Play("playerIdle");
    }

    public void playSlapMissed()
    {
        animator.Play("playerSlapMissed");
    }

    private IEnumerator shake()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        float strength = 30f;
        float shakeInterval = 0.02f; // how often to change shake

        float shakeElapsed = 0f;

        while (elapsed < duration)
        {
            shakeElapsed += Time.unscaledDeltaTime;
            elapsed += Time.unscaledDeltaTime;

            if (shakeElapsed >= shakeInterval / Mathf.Max(Time.timeScale, 0.01f))
            {
                float x = Random.Range(-strength, strength);
                rect.localPosition = originalPos + new Vector3(x, 0f, 0f);
                shakeElapsed = 0f;
            }

            yield return null;
        }

        rect.localPosition = originalPos;
    }


}
