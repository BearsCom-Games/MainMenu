using UnityEngine;

public class ScrollContent : MonoBehaviour
{
    public float ItemSpacing { get { return itemSpacing; } }
    public float VerticalMargin { get { return verticalMargin; } }
    public float Height { get { return height; } }
    public float ChildWidth { get { return childWidth; } }
    public float ChildHeight { get { return childHeight; } }

    private RectTransform rectTransform;

    private RectTransform[] rtChildren;

    private float height;

    private float childWidth, childHeight;

    [SerializeField]
    private float itemSpacing;

    [SerializeField]
    private float verticalMargin;

    [SerializeField]
    private int interval;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rtChildren = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }

        height = rectTransform.rect.height - (2 * verticalMargin);

        childWidth = rtChildren[0].rect.width;
        childHeight = rtChildren[0].rect.height + interval;

        InitializeContentVertical();
    }
    private void InitializeContentVertical()
    {
        float originY = 0 - (height * 0.5f);
        float posOffset = childHeight * 0.5f;
        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].localPosition;
            childPos.y = originY + posOffset + i * (childHeight + itemSpacing);

            int newPointX = Random.Range(0, 11) * 80;
            childPos.x = childPos.x + newPointX;
            
            rtChildren[i].localPosition = childPos;

            if(rtChildren[i].TryGetComponent<NumberTextUpdate>(out var textUpdate))
                textUpdate.UpdateNumber(i+1);
        }
    }
}
