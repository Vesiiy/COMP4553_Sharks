using UnityEngine;

public class Counters : MonoBehaviour
{
    // Global variables that used and updated throughout the game from various scripts
    public static int playerNum;
    public static int roundNum;
    public static int playerBet;

    public int NumberOfPlayers;

    private void Awake()
    {
        // Initializing variables
        playerNum = NumberOfPlayers;
        roundNum = 0;
        playerBet = 0;
    }
}
