using UnityEngine;
using TMPro;

public class GameOverlay: MonoBehaviour
{
    // References
    public TextMeshProUGUI roundCounterTMP;
    public TextMeshProUGUI playerBetTMP;
    public GameObject playerUI;

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

    public void UpdateBotBet(int playerIndex, int betAmount)
    {
        

        string playerPanel = "Player" + playerIndex + "_Panel";
        Transform panel = playerUI.transform.Find(playerPanel);
        
        Transform bet_TMP = panel.Find("Bet_TMP");
        
        TMP_Text betText = bet_TMP.GetComponent<TMP_Text>();

        if (betText == null)
        {
            Debug.LogError($"Bet_TMP in {playerPanel} does not have a TMP_Text component!");
            return;
        }
        betText.text = "Bet: " + betAmount;
    }
}
