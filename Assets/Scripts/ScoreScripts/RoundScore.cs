using System;
using System.Collections.Generic;
using UnityEngine;

public class RoundScore : MonoBehaviour
{
    // References
    public PlayerHand playerHandScript;

    // Need to use list here - dictionary only takes 2 args 
    public List<Tuple<ScriptableObject, int, int>> cardsPlayed = new();

    // Private variables
    [NonSerialized]
    public int playOrder;

    // Add a card to the list of cards played, this will be used to determine who won the trick 
    public void AddCard(ScriptableObject card, int playerId)
    {
        cardsPlayed.Add(new Tuple<ScriptableObject, int, int>(card, playerId, playOrder));
        playOrder++;

        if (playOrder == Counters.playerNum)
        {
            playOrder = 0;
            CalculateTrickWinner();
        }
    }

    // Compare the cards played to determine who won the trick 
    public void CalculateTrickWinner()
    {
        Debug.Log("Calculating Trick Winner");
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
