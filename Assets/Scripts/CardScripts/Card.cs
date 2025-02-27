using UnityEngine;

public class Card : MonoBehaviour
{
    // References 
    public PlayerHand playerHandScript;
    public RoundScore roundScoreScript;
    public GameObject cardPrefab;

    public Counters.Suit cardSuit;
    public int cardWeight;
    public Sprite cardFront;

    public ScriptableObject card;
    public int playerId;

    // Assign the values from the card scriptable object to the card prefab 
    public void AttachCard(ScriptableObject card, int playerId)
    {
        this.card = card;
        this.playerId = playerId;

        // Instantiate a card clone
        GameObject cardObject = Instantiate(cardPrefab);
        cardObject.transform.SetParent(GameObject.Find("PlayerHand_" + playerId).transform, false);

        // Assign new values to the card clone
        cardObject.GetComponent<Card>().cardSuit = (Counters.Suit)card.GetType().GetField("cardSuit").GetValue(card);
        cardObject.GetComponent<Card>().cardWeight = (int)card.GetType().GetField("cardWeight").GetValue(card);
        cardObject.GetComponent<Card>().cardFront = (Sprite)card.GetType().GetField("cardFront").GetValue(card);

        // Assign the running scripts to the card clone
        cardObject.GetComponent<Card>().playerHandScript = GameObject.Find("CardManager").GetComponent<PlayerHand>();
        cardObject.GetComponent<Card>().roundScoreScript = GameObject.Find("ScoreManager").GetComponent<RoundScore>();

        // Disable collider on cards that do not belong to the player 
        // NOTE: ENABLE THIS ONCE BOTS HAVE THE ABILITY TO PLAY CARDS
        // ----------------------------------------------------------
        //if (playerId != 0)
        //{
        //    cardObject.GetComponent<BoxCollider2D>().enabled = false;
        //}
    }

    // Remove the card from the player's hand when clicked
    // NOTE: This will destory the card prefab in the scene, if we decide to use an animation 
    // to move the card on screen and keep it displayed we will need to change this 
    public void OnMouseDown()
    {
        roundScoreScript.AddCard(card, playerId);
        playerHandScript.RemoveCard(card, playerId);
        Destroy(gameObject);
    }
}
