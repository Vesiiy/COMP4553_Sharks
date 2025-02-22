using UnityEngine;

[CreateAssetMenu(fileName = "CardTemplate", menuName = "Scriptable Objects/CardTemplate")]

// Template for creating the card scriptable objects 
public class CardTemplate : ScriptableObject
{
    public enum Suit { Club, Spade, Heart, Diamond };

    // References 
    public Suit cardSuit;
    public int cardWeight;
    public Sprite cardFront;
}
