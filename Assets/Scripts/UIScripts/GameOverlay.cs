using UnityEngine;
using TMPro;

public class GameOverlay: MonoBehaviour
{
    // References
    public TextMeshProUGUI tmpObject;

    // Update TMP text 
    public void UpdateText(int roundNum)
    {
        tmpObject.text = "Round: " + roundNum;
    }
}
