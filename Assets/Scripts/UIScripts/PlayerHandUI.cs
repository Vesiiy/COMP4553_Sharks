using UnityEngine;
using UnityEngine.UI;

public class PlayerHandUI : MonoBehaviour
{
    public HorizontalLayoutGroup layoutGroup;
    public RectTransform playerHandRect; 
    public GameObject cardPrefab; 
    public GameObject playerHand_0;
    private float cardWidth;

    //overlapping of cards is inconsistent
    //can play card before play area is cleared, removing your played card

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

    public void CascadingCards()
    {
        for (int i = 0; i < playerHand_0.transform.childCount; i++)
        {
            //Cascades players cards
            Transform cardTransform = playerHand_0.transform.GetChild(i);
            Transform cardFront = cardTransform.Find("CardFront");
            SpriteRenderer spriteRenderer = cardFront.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = i;
        }
    }
}
