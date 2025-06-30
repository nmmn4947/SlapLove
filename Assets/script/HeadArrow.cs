using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HeadArrow : MonoBehaviour
{
    Vector3 originalPos;
    bool isChessur = false;
    Vector3 chessurPosition;
    float transitionTime = 0.5f;
    float transitionKeep = 0.0f;

    //bool capturedOriginalPos = false;

    Image ch;

    RectTransform rectTransform;
    //[SerializeField] bool isP1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        // Force layout rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform.parent as RectTransform);
        ch = GetComponent<Image>();
        originalPos = rectTransform.position;
    }


    /*    void LateUpdate()
        {
            if (!capturedOriginalPos)
            {
                originalPos = rectTransform.position;
                capturedOriginalPos = true;
                Debug.Log("Captured original position: " + originalPos);
            }
        }*/

    // Update is called once per frame
    void Update()
    {
        transitionKeep += Time.deltaTime;
/*        if (transitionKeep > transitionTime)
        {

        }*/
        if (isChessur)
        {
            rectTransform.position = new Vector3(Mathf.Lerp(rectTransform.position.x, chessurPosition.x, transitionKeep / transitionTime), rectTransform.position.y, rectTransform.position.z);
        }
        else
        {
            rectTransform.position = new Vector3(Mathf.Lerp(rectTransform.position.x, originalPos.x, transitionKeep / transitionTime), rectTransform.position.y, rectTransform.position.z);
        }
    }

    public void setToChessur(Vector3 chessurPos)
    {
        isChessur = true;
        chessurPosition = chessurPos;
        transitionKeep = 0.0f;
        ch.color = Color.white;
    }

    public void setToNormal()
    {
        isChessur = false;
        transitionKeep = 0.0f;
        ch.color = Color.black;
    }

    public Vector3 getRectPos()
    {
        return originalPos;
    }

    public RectTransform getRectTransform()
    {
        return this.rectTransform;
    }
}
