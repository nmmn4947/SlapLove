using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class qteVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] character = new GameObject[3];

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
        Debug.Log(transform.name +"   character Length  "+character.Length);

        if (GameController.instance.GetCurrentCharState() == 0)
        {
            GameObject obj = Instantiate(character[0], transform.position, Quaternion.identity, transform);
            animator = obj.GetComponent<Animator>();
        }
        else if (GameController.instance.GetCurrentCharState() == 1)
        {
            GameObject obj = Instantiate(character[1], transform.position, Quaternion.identity, transform);
            animator = obj.GetComponent<Animator>();
        }
        else if (GameController.instance.GetCurrentCharState() == 2)
        {
            GameObject obj = Instantiate(character[2], transform.position, Quaternion.identity, transform);
            animator = obj.GetComponent<Animator>();
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
