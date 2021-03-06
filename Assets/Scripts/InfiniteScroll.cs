using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    [SerializeField]
    private ScrollContent scrollContent;

    [SerializeField]
    private float outOfBoundsThreshold;

    private ScrollRect scrollRect;

    private Vector2 lastDragPosition;

    private bool positiveDrag;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        positiveDrag = eventData.position.y > lastDragPosition.y;

        lastDragPosition = eventData.position;
    }

    public void OnScroll(PointerEventData eventData)
    {
        positiveDrag = eventData.scrollDelta.y > 0;
    }
    public void OnViewScroll()
    {
        HandleVerticalScroll();
    }

    private void HandleVerticalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);

        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        int newPointX = Random.Range(0, 11) * 80;
        newPos.x = 150 + newPointX;

        if (positiveDrag)
        {
            newPos.y = endItem.position.y - scrollContent.ChildHeight * 1f + scrollContent.ItemSpacing;

            if (currItem.TryGetComponent<NumberTextUpdate>(out var numberText))
                numberText.UpdateNumber(currItem.GetComponent<NumberTextUpdate>().Number - scrollRect.content.childCount);
        }
        else
        {
            newPos.y = endItem.position.y + scrollContent.ChildHeight * 1f - scrollContent.ItemSpacing;
            if (currItem.TryGetComponent<NumberTextUpdate>(out var numberText))
                numberText.UpdateNumber(currItem.GetComponent<NumberTextUpdate>().Number + scrollRect.content.childCount);
        }

        if (currItem.TryGetComponent<NumberTextUpdate>(out var textUpdate))
        {
            if (textUpdate.Number > 0)
            {
                scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
            }

            else
            {
                scrollRect.movementType = ScrollRect.MovementType.Elastic;
            }
        }
        else
        {
            scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        }


        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    private bool ReachedThreshold(Transform item)
    {
        float posYThreshold = transform.position.y + scrollContent.Height * 0.5f + outOfBoundsThreshold;
        float negYThreshold = transform.position.y - scrollContent.Height * 0.5f - outOfBoundsThreshold;
        return positiveDrag ? item.position.y - scrollContent.ChildWidth * 0.5f > posYThreshold :
            item.position.y + scrollContent.ChildWidth * 0.5f < negYThreshold;
    }
}
