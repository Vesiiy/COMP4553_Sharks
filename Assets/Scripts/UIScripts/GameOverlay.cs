using UnityEngine;
using TMPro;

public class GameOverlay: MonoBehaviour
{
    // References
    public TextMeshProUGUI tmpObject;

    private void Update()
    {
        
    }

    // Update TMP text 
    public void UpdateText(int roundNum)
    {
        tmpObject.text = "Round: " + roundNum;
    }
}
