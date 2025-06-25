using System.Collections;
using UnityEngine;

public class HeadArrow : MonoBehaviour
{
    Vector3 originalPos;
    bool isChessur = false;
    Vector3 chessurPosition;
    float transitionTime = 1.0f;
    float transitionKeep = 0.0f;

    RectTransform rectTransform;
    //[SerializeField] bool isP1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPos = rectTransform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transitionKeep += Time.deltaTime;
        if (transitionKeep > transitionTime)
        {
            if (isChessur)
            {
                rectTransform.position = new Vector3(Mathf.Lerp(originalPos.x, chessurPosition.x, transitionKeep/transitionTime), rectTransform.position.y, rectTransform.position.z);
            }
            else
            {
                rectTransform.position = new Vector3(Mathf.Lerp(chessurPosition.x, originalPos.x, transitionKeep / transitionTime), rectTransform.position.y, rectTransform.position.z);
            }
        }
    }

    public void setToChessur(Vector3 chessurPos)
    {
        isChessur = true;
        chessurPosition = chessurPos;
        transitionKeep = 0.0f;
    }

    public void setToNormal()
    {
        isChessur = false;
        transitionKeep = 0.0f;
    }
}
