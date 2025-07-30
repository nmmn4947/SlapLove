using System;
using UnityEngine;
using UnityEngine.UI;

public class qteVisual : MonoBehaviour
{
    static public qteVisual instance;

    [SerializeField] private GameObject[] character = new GameObject[3];

    [SerializeField] private Sprite _spriteOpen;
    [SerializeField] private Sprite _spriteWindUp;
    private AnimatorOverrideController animatorOverride;
    private Animator animator;
    [SerializeField] private Sprite _spriteClose;
    Image image;
    GameObject instantiatedObj = null;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        spawnCharacterSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance)
        {
            
        }
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

    public void spawnCharacterSprite()
    {
        if (instantiatedObj != null)
        {
            Destroy(instantiatedObj);
            instantiatedObj = null;
        }
        if (GameController.instance.GetCurrentCharState() == 0)
        {
            GameObject obj = Instantiate(character[0], transform.position, Quaternion.identity, transform);
            animator = obj.GetComponent<Animator>();
            instantiatedObj = obj;
        }
        else if (GameController.instance.GetCurrentCharState() == 1)
        {
            GameObject obj = Instantiate(character[1], transform.position, Quaternion.identity, transform);
            animator = obj.GetComponent<Animator>();
            instantiatedObj = obj;
        }
        else if (GameController.instance.GetCurrentCharState() == 2)
        {
            GameObject obj = Instantiate(character[2], transform.position, Quaternion.identity, transform);
            animator = obj.GetComponent<Animator>();
            instantiatedObj = obj;
        }
        else
        {
            Debug.LogError("Invalid character state");
        }
    }
}
