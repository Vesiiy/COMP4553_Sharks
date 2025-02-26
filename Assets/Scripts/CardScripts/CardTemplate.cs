using UnityEngine;

[CreateAssetMenu(fileName = "CardTemplate", menuName = "Scriptable Objects/CardTemplate")]

// Template for creating the card scriptable objects 
public class CardTemplate : ScriptableObject
{
    // References 
    public Counters.Suit cardSuit;
    public int cardWeight;
    public Sprite cardFront;
}
