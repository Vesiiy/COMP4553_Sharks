using UnityEngine;

public class Card : MonoBehaviour
{
    public enum Suit { Club, Spade, Heart, Diamond };

    // References 
    public Suit cardSuit;
    public int cardWeight;
    public Sprite cardFront;

    // Assign the values from the card scriptable object to the card prefab 
    public void AttachCard(ScriptableObject card)
    {
        cardSuit = (Suit)card.GetType().GetField("cardSuit").GetValue(card);
        cardWeight = (int)card.GetType().GetField("cardWeight").GetValue(card);
        cardFront = (Sprite)card.GetType().GetField("cardFront").GetValue(card);
    }
}
