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

    //Players take turns starting each round
    public void UpdateRoundStarter()
    {
        Counters.currentTurn = Counters.nextRoundStarter;
        Counters.nextRoundStarter = (Counters.nextRoundStarter + 1) % Counters.playerNum;
        NextPlayerTurn();
    }

    public void UpdateCurrentTurn()
    {
        //Increments current turn if trick and round are in progress
        if (Counters.cardsInPlay != 0 && !Counters.trickOver)
        {
            Counters.currentTurn = (Counters.currentTurn + 1) % Counters.playerNum;
            NextPlayerTurn();
        }
        //Trick winner starts next trick
        else if (Counters.trickOver)
        {
            Counters.trickOver = false;
            NextPlayerTurn();
        }
    }

    //Progresses game to next user turn
    public void NextPlayerTurn() 
    {
        gameOverlayScript.TurnIndicator(Counters.currentTurn);
        // Debug.Log("Current turn: " + Counters.currentTurn);

        //Bots turns
        if (Counters.currentTurn != 0) 
        {
            StartCoroutine(BotTurn());
        }
    }

    public IEnumerator BotTurn()
    {
        yield return new WaitForSeconds(2f); //Delays bots reaction for increased immersion

        //Bots either place bet or play card
        if (Counters.bettingPhase && Counters.cardsInPlay != 0) {BotPlaceBet();}
        else if (!Counters.bettingPhase) {BotPlayCard();}

        UpdateCurrentTurn();
    }

    public void BotPlayCard() 
    {
        //Locates bots hand
        GameObject botHand = GameObject.Find("PlayerHand_" + Counters.currentTurn);

        //only plays card if any exist
        if (botHand.transform.childCount > 0)
        {
            bool cardPlayed = false;
            int cardInHand = 0;

            while (!cardPlayed)
            {
                GameObject cardToDestroy = botHand.transform.GetChild(cardInHand).gameObject;
                Card cardToPlay = cardToDestroy.GetComponent<Card>();

                if (playerHandScript.CanPlayCard(cardToPlay, Counters.currentTurn))
                {
                    gameOverlayScript.UpdateBotCardCount(Counters.currentTurn);
                    playerHandScript.RemoveCard(cardToPlay.card, Counters.currentTurn);
                    roundScoreScript.AddCard(cardToPlay.card, Counters.currentTurn);
                    Destroy(cardToDestroy);

                    cardPlayed = true;
                }
                else {cardInHand++;}
            }
        }

    } 

    public void BotPlaceBet() 
    {
        int botBet = Random.Range(0, Counters.roundNum + 1);
        Counters.playerBet[Counters.currentTurn] = botBet;

        // Debug.Log("Player " + Counters.currentTurn + " Bet: " + botBet);
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
