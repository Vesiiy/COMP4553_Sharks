using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundUpdate: MonoBehaviour
{
    // References
    public GameOverlay gameOverlayScript;
    public DeckBehaviour deckBehaviourScript;
    public PlayerHand playerHandScript;
    public TurnManager turnManagerScript;

    public TMP_InputField playerBetInput;
    public Button submitBetButton;
    public GameObject[] betObjects;

    // Private variables
    private bool isPaused;

    private void Start()
    {
        ObjectActive(betObjects);
        NextRound();
    }

    // Call all functions needed to start a new round
    public void NextRound()
    {
        if (Counters.cardsInPlay == 0)
        {
            Counters.roundNum++;
            Counters.bettingPhase = true;
            NextTrumpSuit();
            Counters.trickSetCheck = true;

            gameOverlayScript.UpdateRound();
    
            deckBehaviourScript.DealCards();
            turnManagerScript.UpdateRoundStarter();

            StartCoroutine(GetPlayerBet());
        }
    }

    // Update the trump suit
    public void NextTrumpSuit()
    {
        if (Counters.trumpSuit == Counters.Suit.None) { Counters.trumpSuit = Counters.Suit.Club; }
        else { Counters.trumpSuit++; }
        gameOverlayScript.UpdateTrumpSuit();
    }

    // Toggles
    public void Paused() { isPaused = !isPaused; }
    public void ObjectActive(GameObject[] objArray)
    {
        foreach (GameObject obj in objArray)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }

    // Player bet coroutine 
    IEnumerator GetPlayerBet()
    {
        ObjectActive(betObjects);
        // Wait for isPaused to become true

        while (true)
        {
            yield return new WaitUntil(() => isPaused);
            if (int.TryParse(playerBetInput.text, out int playerBet) && Counters.currentTurn == 0)
            {
                Counters.playerBet[0] = playerBet;

                // Update overlay 
                gameOverlayScript.UpdateBet();
                ObjectActive(betObjects);
                
                Counters.betsPlaced++;
                turnManagerScript.CheckBettingPhase();
                turnManagerScript.UpdateCurrentTurn();
                break;
            }
            else
            {
                Debug.Log("Player entered wrong input type.");
                yield return new WaitForSeconds(1f);
                Paused();
                // Happens if player inputs a non-int, this should eventually update a text prompt to indicate they entered wrong input 
            }
        }

        Paused();
    }
}
