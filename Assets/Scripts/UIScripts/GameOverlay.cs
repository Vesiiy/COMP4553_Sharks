using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameOverlay: MonoBehaviour
{
    // References
    public TextMeshProUGUI roundCounterTMP;
    public TextMeshProUGUI playerBetTMP;
    public TextMeshProUGUI playerScoreTMP;
    public TextMeshProUGUI trumpSuitTMP;
    public TextMeshProUGUI winLoseTMP;
    public TextMeshProUGUI[] trickWins;
    public TextMeshProUGUI[] playerBets;

    public Button returnButton;
    public Button replayButton;

    public GameObject pauseOverlayCanvas;
    public GameObject playerUI;

    // Private Variables
    public TextMeshProUGUI trickSuitTMP;
    public Image[] playerPanels;
    private int[] cardCounts;
    private bool isPaused;

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

    public void UpdateTrumpSuit()
    {
        trumpSuitTMP.text = "Trump: " + Counters.trumpSuit;
    }

    public void UpdateTrickSuit()
    {
        if (!Counters.trickOver)
        {
            trickSuitTMP.text = "Trick: " + Counters.trickSuit;

            if (Counters.roundNum % 5 == 0) {
                trumpSuitTMP.text = "Trump: " + Counters.trickSuit;
            }
        }
        else if (Counters.trickOver)
        {
            trickSuitTMP.text = "Trick: ";

            if (Counters.roundNum % 5 == 0) {
                trumpSuitTMP.text = "Trump: None";
            }
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

    public void ClearTricks()
    {
        for (int i = 0; i < Counters.playerNum; i++)
        {
            trickWins[i].text = "Tricks: ";
        }
    }

    public void ClearBets()
    {
        for (int i = 0; i < Counters.playerNum; i++)
        {
            playerBets[i].text = "Bet: ";
        }
    }

    public void UpdateTrick(int playerIndex, int tricksWon)
    {
        trickWins[playerIndex].text = "Tricks: " + tricksWon;
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

    public void UpdateCardPlayArea(int slotIndex, List<Tuple<ScriptableObject, int, int>> cardsPlayed)
    {
        GameObject cardPlayArea = GameObject.Find("CardPlayArea");

        Transform slot = cardPlayArea.transform.GetChild(slotIndex);
        SpriteRenderer spriteRenderer = slot.GetComponent<SpriteRenderer>();

        ScriptableObject cardData = cardsPlayed[slotIndex].Item1;
        Sprite cardSprite = (Sprite)cardData.GetType().GetField("cardFront").GetValue(cardData);

        spriteRenderer.sprite = cardSprite;

        // Halve the size of the card sprite
        slot.localScale = new Vector3(0.5f, 0.5f, 1f);
    }

    public IEnumerator ClearCardPlayArea(bool nextRound)
    {
        GameObject cardPlayArea = GameObject.Find("CardPlayArea");

        if (!nextRound) {
            yield return new WaitForSeconds(2f);
        } else {
            yield return new WaitForSeconds(0);

        }
        for (int i = 0; i < cardPlayArea.transform.childCount; i++)
        {
            cardPlayArea.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    private void Paused() { isPaused = !isPaused; }

    public void PauseGame()
    {
        Paused();
        pauseOverlayCanvas.SetActive(isPaused);
    }

    public void GameEnd(bool win)
    {
        PauseGame();

        // Switch to game end pause overlay
        returnButton.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(true);

        winLoseTMP.gameObject.SetActive(true);

        if (win) { winLoseTMP.text = "You Win!"; }
        else { winLoseTMP.text = "You Lose"; }
    }
    public void TurnIndicator(int playerIndex)
    {
        for (int i = 0; i < Counters.playerNum; i++)
        {
            Color panelColor = playerPanels[i].color;
            if (i == playerIndex) 
            {
                panelColor.a = 1.0f;
            }
            else
            {
                panelColor.a = 0.0f;
            }
            playerPanels[i].color = panelColor;
        }

    }
}
