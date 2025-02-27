using UnityEngine;
using TMPro;

public class GameOverlay: MonoBehaviour
{
    // References
    public TextMeshProUGUI roundCounterTMP;
    public TextMeshProUGUI playerBetTMP;

    // Update round counter TMP
    public void UpdateRound()
    {
        roundCounterTMP.text = "Round: " + Counters.roundNum;
    }
    // Update player bet TMP
    public void UpdateBet()
    {
        playerBetTMP.text = "Your bet: " + Counters.playerBet[0];
    }
}
