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
        Counters.roundNum++;
        gameOverlayScript.UpdateRound();
        StartCoroutine(GetPlayerBet());

        // CLEAR PLAYER HAND USED FOR TESTING -- REMOVE LATER !!!
        playerHandScript.ClearPlayerHands();
        deckBehaviourScript.DealCards();
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
            if (int.TryParse(playerBetInput.text, out Counters.playerBet))
            {
                // Update overlay 
                gameOverlayScript.UpdateBet();
                ObjectActive(betObjects);
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
