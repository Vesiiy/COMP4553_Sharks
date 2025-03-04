using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    // References 
    public Card cardScript;

    // Private Variables 
    private readonly Dictionary<string, List<ScriptableObject>> playerHands = new();

    private void Awake()
    {
        // Creates a new list for each player that will hold their cards
        for (int i = 0; i < Counters.playerNum; i++)
        {
            string key = "PlayerHand_" + i;
            playerHands[key] = new List<ScriptableObject>();
        }
    }

    // Adds a card to the player's hand
    public void UpdatePlayerHand(ScriptableObject card, int playerId)
    {
        string key = "PlayerHand_" + playerId;
        playerHands[key].Add(card);

        cardScript.AttachCard(card, playerId);
    }

    // Check the contents of each hand -- used for testing
    public bool CanPlayCard(Card clickedCard, int playerId)
    {
        Debug.Log("Trick Suit: " + Counters.trickSuit);

        string key = "PlayerHand_" + playerId;
        bool hasTrickSuit = false;

        foreach (var card in playerHands[key])
        {
            CardTemplate cardData = (CardTemplate)card;
            if (cardData.cardSuit == Counters.trickSuit)
            {
                hasTrickSuit = true;
                break;
            }
        }
        //wild cards can always be played || no trick suit set yet
        if (clickedCard.cardSuit == Counters.Suit.None) {return true;}

        //if player has trick suit, they must play it
        else if (hasTrickSuit && clickedCard.cardSuit != Counters.trickSuit)
        {
            Debug.Log("Must play card from trick suit");
            return false;
        }

        return true;
    }

    // Remove a card from the player's hand
    public void RemoveCard(ScriptableObject card, int playerId)
    {
        string key = "PlayerHand_" + playerId;
        playerHands[key].Remove(card);
        Counters.cardsInPlay--;
        // Debug.Log("Cards in play: " + Counters.cardsInPlay);
    }

    // Check the contents of each hand -- used for testing
    public void CheckHands()
    {
        foreach (var item in playerHands)
        {
            string key = item.Key;
            List<ScriptableObject> objectList = item.Value;

            foreach (var card in objectList)
            {
                Debug.Log("Key: " + key + " Card: " + card);
            }
        }
    }

    // Clears all player hands -- used for testing 
    public void ClearPlayerHands()
    {
        foreach (KeyValuePair<string, List<ScriptableObject>> playerHand in playerHands)
        {
            playerHand.Value.Clear();
        }
    }
}
