using TMPro;
using UnityEngine;

public class RoundUpdate: MonoBehaviour
{
    // References
    public GameOverlay gameOverlay;

    // Private variables
    private int roundNum;

    private void Start()
    {
        NextRound();
    }

    public void NextRound()
    {
        roundNum++;
        gameOverlay.UpdateText(roundNum);
    }
}
