using UnityEngine;

public class Card : MonoBehaviour
{
    // References 
    public PlayerHand playerHandScript;
    public RoundScore roundScoreScript;
    public TurnManager turnManagerScript;
    public GameObject cardPrefab;

    public Counters.Suit cardSuit;
    public int cardWeight;
    public GameObject spriteFront;
    public GameObject spriteBack;

    public ScriptableObject card;
    public int playerId;

    // Assign the values from the card scriptable object to the card prefab 
    public void AttachCard(ScriptableObject card, int playerId)
    {
        this.card = card;
        this.playerId = playerId;

        // Instantiate a card clone
        Debug.Log("" + playerId);

        GameObject cardObject = Instantiate(cardPrefab);
        cardObject.transform.SetParent(GameObject.Find("PlayerHand_" + playerId).transform, false);

        Debug.Log($"Dealing card to Player {playerId} - Card Object: {cardObject.name}");


        // Assign new values to the card clone
        Card cardComponent = cardObject.GetComponent<Card>();
        cardComponent.cardSuit = (Counters.Suit)card.GetType().GetField("cardSuit").GetValue(card);
        cardComponent.cardWeight = (int)card.GetType().GetField("cardWeight").GetValue(card);

        cardComponent.spriteFront = cardObject.transform.Find("CardFront").gameObject;
        cardComponent.spriteBack = cardObject.transform.Find("CardBack").gameObject;

        SpriteRenderer frontRenderer = cardComponent.spriteFront.GetComponent<SpriteRenderer>();
        SpriteRenderer backRenderer = cardComponent.spriteBack.GetComponent<SpriteRenderer>();

        // Assign the running scripts to the card clone
        cardObject.GetComponent<Card>().playerHandScript = GameObject.Find("CardManager").GetComponent<PlayerHand>();
        cardObject.GetComponent<Card>().roundScoreScript = GameObject.Find("ScoreManager").GetComponent<RoundScore>();

        frontRenderer.sprite = (Sprite)card.GetType().GetField("cardFront").GetValue(card);

        // Disable collider on cards that do not belong to the player 
        // NOTE: ENABLE THIS ONCE BOTS HAVE THE ABILITY TO PLAY CARDS
        // ----------------------------------------------------------
        if (playerId > 0)
        {
            cardObject.GetComponent<BoxCollider2D>().enabled = false;
            cardComponent.spriteFront.SetActive(false);
            cardComponent.spriteBack.SetActive(true);
        }   
        else if (playerId == 0)
        {
            cardComponent.spriteFront.SetActive(true);
            cardComponent.spriteBack.SetActive(false);
        }
    }

    // Remove the card from the player's hand when clicked
    // NOTE: This will destory the card prefab in the scene, if we decide to use an animation 
    // to move the card on screen and keep it displayed we will need to change this 
    public void OnMouseDown()
    {
        //Restricts user from playing cards as per game rules
        if (Counters.currentTurn == 0 && !Counters.bettingPhase) 
        {
            roundScoreScript.AddCard(card, playerId);
            playerHandScript.RemoveCard(card, playerId);
            Destroy(gameObject);

            FindFirstObjectByType<TurnManager>().UpdateCurrentTurn();
        }
    }
}
