using UnityEngine;

public class CardAnimationBehaviour : MonoBehaviour
{
    public SpriteRenderer cardSprite;
    public Animator playerCardAnimator;
    public Sprite backOfCard;
    public Sprite frontOfCard;

    [ContextMenu("Flip Card")]
    void FlipCard()
    /** Call this whenever you'd like to flip a card. 
        It triggers the card flip animation. */
    {
        playerCardAnimator.SetBool("isFaceDown", false);
    }

    void SwapCardSideSprite()
    /** Used by the animator when the card flips 
        from back to front. */
    {
        cardSprite.sprite = frontOfCard;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
