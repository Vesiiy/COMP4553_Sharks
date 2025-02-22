using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    // References 
    public GameObject cardPrefab;

    // Private Variables 
    private readonly Dictionary<string, List<ScriptableObject>> playerHands = new();

    private void Start()
    {
        // Creates a new list for each player that will hold their cards
        for (int i = 0; i < Counters.playerNum; i++)
        {
            string key = "PlayerHand_" + i;
            playerHands[key] = new List<ScriptableObject>();
            Debug.Log(key + " has been created.");

            // Creates a parent game object for each player's hand
            GameObject cardParent = new();
            cardParent.name = key;
        }
    }

    // Adds a card to the player's hand
    public void UpdatePlayerHand(ScriptableObject card, int playerId)
    {
        string key = "PlayerHand_" + playerId;
        playerHands[key].Add(card);

        // Debug statements for testing -- remove later !!!
        Debug.Log(key + " has " + playerHands[key].Count + " cards.");
        Debug.Log(card);

        // Instantiate the card prefab 
        GameObject cardObject = Instantiate(cardPrefab);
        cardObject.transform.SetParent(GameObject.Find(key).transform);
    }

    // Remove a card from the player's hand
    public void RemoveCard(ScriptableObject card, int playerId)
    {
        string key = "PlayerHand_" + playerId;
        playerHands[key].Remove(card);
    }

    // Clears all player hands -- used for testing, might not need ???
    public void ClearPlayerHands()
    {
        foreach (KeyValuePair<string, List<ScriptableObject>> playerHand in playerHands)
        {
            playerHand.Value.Clear();
        }
    }
}
