using UnityEngine;
using UnityEngine.UI;

public class PlayerHandUI : MonoBehaviour
{
    public HorizontalLayoutGroup layoutGroup;
    public RectTransform playerHandRect; 
    public GameObject cardPrefab; 
    private float cardWidth;


    private void Start() {
        RectTransform cardRect = cardPrefab.GetComponent<RectTransform>();
        cardWidth = cardRect.rect.width;
    }

    public void UpdateCardSpacing(int totalCards)  
    {
        if (layoutGroup == null) return;
        float spacing = (playerHandRect.rect.width / totalCards) - cardWidth;
        layoutGroup.spacing = spacing;
    }
}
