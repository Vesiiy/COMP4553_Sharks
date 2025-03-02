using System.Collections.Generic;
using UnityEngine;

public class DeckBehaviour : MonoBehaviour
{
    // References
    public Deck deckScript;
    public PlayerHand playerHandScript;
    public PlayerHandUI playerHandUI; 

    public void DealCards()
    {
        Counters.cardsInPlay = Counters.roundNum * Counters.playerNum;
        int playerId = 0; 

        // Shallow copy of deckScript.cards<>
        // NOTE: this only makes a copy of the list with references to the same objects.
        // Any changes made to the cards in this list will make changes to the objects themselves. 
        List<ScriptableObject> cards = new(deckScript.cards);

        // Deals cards round robin style to each player and adds it to their hand 
        for (int i = 0; i < Counters.cardsInPlay; i++)
        {
            int randomIndex = Random.Range(0, cards.Count - 1);

            // Adds each card to the player's hand based on playerId
            playerHandScript.UpdatePlayerHand(cards[randomIndex], playerId);
            if (playerId == 0) {playerHandUI.UpdateCardSpacing(Counters.roundNum);}
            playerId++;
            if (playerId == Counters.playerNum) { playerId = 0; }

            // Removes the card from the deck
            cards.RemoveAt(randomIndex);
        }
    }
}
