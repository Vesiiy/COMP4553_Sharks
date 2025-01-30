using TMPro;
using UnityEngine;

public class RoundUpdate: MonoBehaviour
{
    // References
    public GameOverlay gameOverlay;
    public TMP_InputField betInput;

    // Private variables
    private int roundNum;
    private int playerBet;

    private void Start()
    {
        NextRound();
    }

    // Update round number 
    public void NextRound()
    {
        roundNum++;
        gameOverlay.UpdateRound(roundNum);
        GetPlayerBet();
    }

    // Get player bet 
    public void GetPlayerBet()
    {
        if (int.TryParse(betInput.text, out playerBet))
        {
            gameOverlay.UpdateBet(playerBet);
            // This checks ASAP - need to wait for user to press button
        }
        else
        {
            Debug.Log("Player entered wrong input type.");
            // Happens if player inputs a non-int, this should eventually update whatever text prompt the player sees to indicate they entered wrong input 
            return;
        }
    }
}
