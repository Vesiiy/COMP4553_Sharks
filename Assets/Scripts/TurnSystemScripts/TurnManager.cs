using UnityEngine;
using System.Collections;


public class TurnManager : MonoBehaviour
{
    private RoundScore roundScoreScript;
    private PlayerHand playerHandScript;
    public GameOverlay gameOverlayScript;
    private void Start() 
    {
        roundScoreScript = FindFirstObjectByType<RoundScore>();
        playerHandScript = FindFirstObjectByType<PlayerHand>();
    }

    public void UpdateRoundStarter()
    {
        Counters.currentTurn = Counters.nextRoundStarter;
        Counters.nextRoundStarter = (Counters.nextRoundStarter + 1) % Counters.playerNum;
        NextPlayerTurn();
    }

    public void UpdateCurrentTurn()
    {
        Counters.currentTurn = (Counters.currentTurn + 1) % Counters.playerNum;
        NextPlayerTurn();
    }

    public void NextPlayerTurn() 
    {
        Debug.Log("Current turn: " + Counters.currentTurn);

        if (Counters.currentTurn != 0) 
        {
            StartCoroutine(BotTurn());
        }
    }

    public IEnumerator BotTurn()
    {
        yield return new WaitForSeconds(1f);
        if (Counters.bettingPhase) {BotPlaceBet();}
        else if (!Counters.bettingPhase) {BotPlayCard();}
        UpdateCurrentTurn();
    }

    public void BotPlayCard() 
    {
        GameObject botHand = GameObject.Find("PlayerHand_" + Counters.currentTurn);
        //Debug.Log(botHand);

        if (botHand.transform.childCount > 0)
        {
            GameObject cardToDestroy = botHand.transform.GetChild(0).gameObject;
            ScriptableObject cardToPlay = cardToDestroy.GetComponent<Card>().card;

            roundScoreScript.AddCard(cardToPlay, Counters.currentTurn);
            playerHandScript.RemoveCard(cardToPlay, Counters.currentTurn);
            Destroy(cardToDestroy);
        }
    } 

    public void BotPlaceBet() 
    {
        int botBet = Random.Range(0, Counters.roundNum);
        Counters.playerBet[Counters.currentTurn] = botBet;
        Debug.Log("Player " + Counters.currentTurn + " Bet: " + botBet);
        gameOverlayScript.UpdateBotBet(Counters.currentTurn, botBet);
        Counters.betsPlaced++;
        CheckBettingPhase();
    }

    public void CheckBettingPhase()
    {
        if (Counters.betsPlaced == Counters.playerNum)
        {
            Counters.bettingPhase = false;
            Counters.betsPlaced = 0;
        }
    }
}
