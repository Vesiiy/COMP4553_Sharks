using System.Collections.Generic;
using UnityEngine;

public class Counters : MonoBehaviour
{
    public enum Suit { Club, Spade, Heart, Diamond, None };

    // Global variables that are used and updated throughout the game from various scripts
    public static int playerNum;
    public static int roundNum;
    public static int cardsInPlay;
    public static int betsPlaced;
    public static int currentTurn;
    public static int nextRoundStarter;

    public static Suit trumpSuit;
    public static Suit trickSuit;
    
    public static bool trickSetCheck;
    public static bool bettingPhase;
    public static bool trickOver;

    public static List<int> playerBet = new();
    public static List<int> roundScores = new();

    // Assigned in editor 
    public int NumberOfPlayers;

    private void Awake()
    {
        // Initializing variables
        playerNum = NumberOfPlayers;
        roundNum = 0;
        cardsInPlay = 0;
        betsPlaced = 0;
        currentTurn = 0;
        nextRoundStarter = 0;

        trumpSuit = Suit.None;
        // trickSuit = Suit.None;

        trickSetCheck = true;
        bettingPhase = true;
        trickOver = false;

        for (int i = 0; i < playerNum; i++) { playerBet.Add(0); }
        for (int i = 0; i < playerNum; i++) { roundScores.Add(0); }
    }
}
