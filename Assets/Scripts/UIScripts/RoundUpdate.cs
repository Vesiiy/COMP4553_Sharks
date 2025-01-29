using TMPro;
using UnityEngine;

public class RoundUpdate: MonoBehaviour
{
    // References
    public GameOverlay gameOverlay;
    private int roundNum;

    private void Awake()
    {
        NextRound();
    }

    public void NextRound()
    {
        roundNum++;
        gameOverlay.UpdateText(roundNum);
    }
}
