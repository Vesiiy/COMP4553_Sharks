using System;
using System.Collections.Generic;
using UnityEngine;

public class RoundScore : MonoBehaviour
{
    // References
    public PlayerHand playerHandScript;
    public RoundUpdate roundUpdateScript;
    public GameOverlay gameOverlayScript;

    public List<Tuple<ScriptableObject, int, int>> cardsPlayed = new();
    public List<int> playerScores = new();

    // Private variables
    [NonSerialized]
    private int playOrder;
    private int tempCardWeight;

    private void Start()
    {
        for (int i = 0; i < Counters.playerNum; i++) { playerScores.Add(0); }
    }

    // Add a card to the list of cards played, this will be used to determine who won the trick 
    public void AddCard(ScriptableObject card, int playerId)
    {
        // Assign the trick suit if trickSetCheck is true 
        if (Counters.trickSetCheck) 
        {
            // Check if the card played is a wild card 
            tempCardWeight = (int)card.GetType().GetField("cardWeight").GetValue(card);
            if (tempCardWeight != 1 && tempCardWeight != 15)
            {
                Counters.trickSuit = (Counters.Suit)card.GetType().GetField("cardSuit").GetValue(card);
                Counters.trickSetCheck = false;
            }
        }

        // Add card to the list of cards played
        cardsPlayed.Add(new Tuple<ScriptableObject, int, int>(card, playerId, playOrder));
        playOrder++;

        // Calculate trick winner when all players have played a card
        if (playOrder == Counters.playerNum)
        {
            playOrder = 0;
            CalculateTrickWinner();
        }
    }

    // Compare the cards played to determine who won the trick 
    // NOTE: atm this does not check if all players played a lose card, the game would likely break if that happened... so please don't do that
    public void CalculateTrickWinner()
    {
        Tuple<ScriptableObject, int, int> held = cardsPlayed[0];

        foreach (var item in cardsPlayed)
        {
            Counters.Suit heldSuit = (Counters.Suit)held.Item1.GetType().GetField("cardSuit").GetValue(held.Item1);
            Counters.Suit itemSuit = (Counters.Suit)item.Item1.GetType().GetField("cardSuit").GetValue(item.Item1);

            int heldWeight = (int)held.Item1.GetType().GetField("cardWeight").GetValue(held.Item1);
            int itemWeight = (int)item.Item1.GetType().GetField("cardWeight").GetValue(item.Item1);

            switch (item)
                {
                    // First card played
                    case var (_, _, item3) when item3 == 0:
                        held = item;
                        break;
                    // item = win
                    case var _ when itemWeight == 15:
                        held = item;
                        goto breakLoop;
                    // item = trumpSuit
                    case var _ when itemSuit == Counters.trumpSuit:
                        // held && item = trumpSuit --- check weight
                        if (heldSuit == Counters.trumpSuit)
                        {
                            if (itemWeight > heldWeight) { held = item; }
                        }
                        // held != trumpSuit && item != lose
                        else if (itemWeight != 1) { held = item; }
                        break;
                    // item = trickSuit
                    case var _ when itemSuit == Counters.trickSuit:
                        // held && item = trickSuit 
                        if (heldSuit == Counters.trickSuit)
                        {
                            if (itemWeight > heldWeight) { held = item; }
                        }
                        // held != trumpSuit || trickSuit && item != lose 
                        // This also accounts for the first card played being a lose card
                        else if (heldSuit != Counters.trumpSuit && itemWeight != 1) { held = item; }
                        break;
                    // item != trickSuit || trumpSuit
                    default:
                        break;
                }
        }

    breakLoop:
        ClearCardsPlayed();
        playerScores[held.Item2]++;
        Debug.Log("Player " + held.Item2 + " won the trick.");
        UpdateScores();
        Counters.currentTurn = held.Item2;
    }

    public void ClearCardsPlayed() { cardsPlayed.Clear(); }

    public void UpdateScores()
    {
        for (int i = 0; i < Counters.playerNum; i++)
        {
            int temp;

            // Calculate points to be awarded to each player
            if (Counters.playerBet[i] == playerScores[i])
            { temp = 20 + (playerScores[i] * 10); }
            else
            { temp = (Counters.playerBet[i] - playerScores[i]) * -10; }

            // Update round scores and reset player scores
            Counters.roundScores[i] += temp;

            if (i > 0)
            {
                gameOverlayScript.UpdateBotScore(i, Counters.roundScores[i]);
            }
            else if (i == 0)
            {
                gameOverlayScript.UpdateScore(Counters.roundScores[i]);
            }
            playerScores[i] = 0;

            Debug.Log("Player " + i + " scored " + temp + " points this round and has " + Counters.roundScores[i] + " points");
        }
    }

}
