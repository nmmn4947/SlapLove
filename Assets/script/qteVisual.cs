using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class qteVisual : MonoBehaviour
{
    [SerializeField] private AnimatorController[] characterAnimators;

    [SerializeField] private Sprite _spriteOpen;
    [SerializeField] private Sprite _spriteWindUp;
    private AnimatorOverrideController animatorOverride;
    private Animator animator;
    [SerializeField] private Sprite _spriteClose;
    Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        if (GameController.instance.GetCurrentCharState() == 0)
        {
            animator.runtimeAnimatorController = characterAnimators[0];
        }
        else if (GameController.instance.GetCurrentCharState() == 1)
        {
            animator.runtimeAnimatorController = characterAnimators[1];
        }
        else if (GameController.instance.GetCurrentCharState() == 2)
        {
            animator.runtimeAnimatorController = characterAnimators[2];
        }
        else
        {
            Debug.LogError("Invalid character state");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.checkQTE())
        {
            animator.Play("Open");
        }
        else if (GameController.instance.checkQTEWindUp())
        {
            animator.Play("WindUp");
        }
        else
        {
            animator.Play("Close");
        }
    }
}
