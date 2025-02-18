using System.Collections.Generic;
using UnityEngine;

public class DeckBehaviour : MonoBehaviour
{
    // References
    public Deck deckScript;

    public void DealCards(int roundNum, int playerNum)
    {
        int cardsInPlay = roundNum * playerNum;
        int playerId = playerNum; 

        // Shallow copy of deckScript.cards<>
        // NOTE: this only makes a copy of the list with references to the same objects.
        // Any changes made to the cards in this list will make changes to the objects themselves. 
        List<ScriptableObject> cards = new(deckScript.cards);

        for (int i = 0; i < cardsInPlay; i++)
        {
            int randomIndex = Random.Range(0, cards.Count -1);

            // playerCard.updatePlayerDeck(cards[randomIndex], playerId);
            // playerId--;
            // if (playerId = 0) { playerId = playerNum };

            // This will eventually send the delt card to the playerCards script and will add
            // it to the corresponding players hand

            cards.RemoveAt(randomIndex);
        }
    }
}
