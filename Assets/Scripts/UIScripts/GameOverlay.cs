using UnityEngine;
using TMPro;

public class GameOverlay: MonoBehaviour
{
    // References
    public TextMeshProUGUI roundCounterTMP;
    public TextMeshProUGUI playerBetTMP;

    // Update round counter TMP
    public void UpdateRound(int roundNum)
    {
        roundCounterTMP.text = "Round: " + roundNum;
    }
    // Update player bet TMP
    public void UpdateBet(int playerBet)
    {
        playerBetTMP.text = "Your bet: " + playerBet;
    }
}
