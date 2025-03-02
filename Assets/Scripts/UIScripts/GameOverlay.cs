using UnityEngine;
using TMPro;

public class GameOverlay: MonoBehaviour
{
    // References
    public TextMeshProUGUI roundCounterTMP;
    public TextMeshProUGUI playerBetTMP;
    public TextMeshProUGUI playerScoreTMP;
    public GameObject playerUI;
    private int[] cardCounts;

    // Update round counter TMP
    private void Awake()
    {
        cardCounts = new int[Counters.playerNum];
    }
    public void UpdateRound()
    {
        roundCounterTMP.text = "Round: " + Counters.roundNum;

        for (int i = 1; i < Counters.playerNum; i++) {
            cardCounts[i] = Counters.roundNum;
            UpdateBotCardCount(i);
        }
    }
    // Update player bet TMP
    public void UpdateBet()
    {
        playerBetTMP.text = "Bet: " + Counters.playerBet[0];
    }

    public void UpdateScore(int score)
    {
        playerScoreTMP.text = "Score: " + score;
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

    public void UpdateBotScore(int playerIndex, int score)
    {
        string playerPanel = "Player" + playerIndex + "_Panel";
        Transform panel = playerUI.transform.Find(playerPanel);

        Transform score_TMP = panel.Find("Score_TMP");
        TMP_Text scoreText = score_TMP.GetComponent<TMP_Text>();
        scoreText.text = "Score: " + score;
    }

    public void UpdateBotCardCount(int playerIndex)
    {
        if (!Counters.bettingPhase) {cardCounts[playerIndex]--;}

        string playerPanel = "Player" + playerIndex + "_Panel";
        Transform panel = playerUI.transform.Find(playerPanel);

        Transform cards_TMP = panel.Find("Cards_TMP");
        TMP_Text cardsText = cards_TMP.GetComponent<TMP_Text>();
        cardsText.text = "Cards: " + cardCounts[playerIndex];
    }
}
