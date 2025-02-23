using UnityEngine;
using UnityEngine.InputSystem;

public class Card : MonoBehaviour
{
    public enum Suit { Club, Spade, Heart, Diamond };

    // References 
    public PlayerHand playerHandScript;
    public RoundScore roundScoreScript;
    public GameObject cardPrefab;

    public Suit cardSuit;
    public int cardWeight;
    public Sprite cardFront;

    // Private Variables
    private ScriptableObject card;
    private int playerId;

    // Assign the values from the card scriptable object to the card prefab 
    public void AttachCard(ScriptableObject card, int playerId)
    {
        this.card = card;
        this.playerId = playerId;

        // Instantiate the card prefab 
        GameObject cardObject = Instantiate(cardPrefab);
        cardObject.transform.SetParent(GameObject.Find("PlayerHand_" + playerId).transform, false);

        // Assign new values to the card prefab
        cardObject.GetComponent<Card>().cardSuit = (Suit)card.GetType().GetField("cardSuit").GetValue(card);
        cardObject.GetComponent<Card>().cardWeight = (int)card.GetType().GetField("cardWeight").GetValue(card);
        cardObject.GetComponent<Card>().cardFront = (Sprite)card.GetType().GetField("cardFront").GetValue(card);
    }

    // Remove the card from the player's hand when clicked
    // NOTE: This will destory the card prefab in the scene, if we decide to use an animation 
    // to move the card on screen and keep it displayed we will need to change this 
    public void OnMouseDown()
    {
        roundScoreScript.AddCard(card, playerId);
        // playerHandScript.RemoveCard(card, playerId);
        Destroy(gameObject);
        Debug.Log("check");
    }
}
