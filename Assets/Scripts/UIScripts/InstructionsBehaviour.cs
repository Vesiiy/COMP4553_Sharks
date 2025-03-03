
using TMPro;
using UnityEngine;
//using UnityEngine.UI;

public class InstructionsBehaviour : MonoBehaviour
{
    const int MAXINSTRUCTIONS = 5;
    const string BASENAMEOFINSTRUCTIONTEXTS = "InstructionText";

    public GameObject textsParent;

    //This holds where in the instructions we are
    int instructionsPosition;

    private void Start()
    {
        instructionsPosition = 1;
    }

    public void Increment_InstructionsPosition()
    /** Called whenever the right (>) instructions button is pressed. */
    {
        //If we can actually scrool right the instructions
        if (instructionsPosition < MAXINSTRUCTIONS)
        {
            //Get the relevant instruction texts
            TMP_Text oldActiveInstruction = GameObject.Find(BASENAMEOFINSTRUCTIONTEXTS + instructionsPosition.ToString()).GetComponent<TMP_Text>();
            instructionsPosition += 1;
            TMP_Text newActiveInstruction = GameObject.Find(BASENAMEOFINSTRUCTIONTEXTS + instructionsPosition.ToString()).GetComponent<TMP_Text>();

            //Enbale/disable as needed
            oldActiveInstruction.enabled = false;
            newActiveInstruction.enabled = true;

            //Check to see if we need to "disable" the button
            if (instructionsPosition == MAXINSTRUCTIONS)
                GameObject.Find("RightArrowText").GetComponent<TMP_Text>().SetText("");
                
            //"Enable" the other button
            GameObject.Find("LeftArrowText").GetComponent<TMP_Text>().SetText("<");
        }
    }

    public void Decrement_InstructionsPosition()
    /** Called whenever the left (<) instructions button is pressed. */
    {
        //If we can actually scrool right the instructions
        if (instructionsPosition > 1)
        {
            //Get the relevant instruction texts
            TMP_Text oldActiveInstruction = GameObject.Find(BASENAMEOFINSTRUCTIONTEXTS + instructionsPosition.ToString()).GetComponent<TMP_Text>();
            instructionsPosition -= 1;
            TMP_Text newActiveInstruction = GameObject.Find(BASENAMEOFINSTRUCTIONTEXTS + instructionsPosition.ToString()).GetComponent<TMP_Text>();

            //Enbale/disable as needed
            oldActiveInstruction.enabled = false;
            newActiveInstruction.enabled = true;

            //Check to see if we need to "disable" the button
            if (instructionsPosition == 1)
                GameObject.Find("LeftArrowText").GetComponent<TMP_Text>().SetText("");

            //"Enable" the other button
            GameObject.Find("RightArrowText").GetComponent<TMP_Text>().SetText(">");
        }
    }
}
