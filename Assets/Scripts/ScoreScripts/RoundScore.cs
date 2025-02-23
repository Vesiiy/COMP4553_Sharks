using System;
using System.Collections.Generic;
using UnityEngine;

public class RoundScore : MonoBehaviour
{
    // References
    public List<List<Tuple<ScriptableObject, int, int>>> cardsPlayed;

    // Private Variables
    private int playOrder;

    // Add a card to the list of cards played, this will be used to determine who won the trick 
    public void AddCard(ScriptableObject card, int playerId)
    {
        cardsPlayed.Add(new List<Tuple<ScriptableObject, int, int>>());
        cardsPlayed[playerId].Add(new Tuple<ScriptableObject, int, int>(card, playOrder, playerId));
        playOrder++;
    }

    // Compare the cards played to determine who won the trick 
    public void CalculateTrickWinner(Enum trickSuit, Enum roundSuit)
    {
        //playOrder = 0; 

        //foreach (List<Tuple<ScriptableObject, int, int>> card in cardsPlayed)
        //{
        //    // If card suit matches the trick or round suit 
        //    if (card.GetType().GetField("cardSuit").GetValue(card) == trickSuit || 
        //        card.GetType().GetField("cardSuit").GetValue(card) == roundSuit)
        //    {
        //        //
        //    }
        //}
    }
}
