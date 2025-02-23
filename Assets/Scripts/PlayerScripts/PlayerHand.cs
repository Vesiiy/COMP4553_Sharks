using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    // References 
    public Card cardScript;

    // Private Variables 
    private readonly Dictionary<string, List<ScriptableObject>> playerHands = new();

    private void Awake()
    {
        // Creates a new list for each player that will hold their cards
        for (int i = 0; i < Counters.playerNum; i++)
        {
            string key = "PlayerHand_" + i;
            playerHands[key] = new List<ScriptableObject>();
        }
    }

    // Adds a card to the player's hand
    public void UpdatePlayerHand(ScriptableObject card, int playerId)
    {
        string key = "PlayerHand_" + playerId;
        playerHands[key].Add(card);

        cardScript.AttachCard(card, playerId);
    }

    // Check the contents of each hand -- used for testing
    public void CheckHands()
    {
        foreach (var item in playerHands)
        {
            string key = item.Key;
            List<ScriptableObject> objectList = item.Value;

            foreach (var card in objectList)
            {
                Debug.Log("Key: " + key + " Card: " + card);
            }
        }
    }

    // Remove a card from the player's hand
    public void RemoveCard(ScriptableObject card, int playerId)
    {
        string key = "PlayerHand_" + playerId;
        playerHands[key].Remove(card);
    }

    // Clears all player hands -- used for testing 
    public void ClearPlayerHands()
    {
        foreach (KeyValuePair<string, List<ScriptableObject>> playerHand in playerHands)
        {
            playerHand.Value.Clear();
        }
    }
}
