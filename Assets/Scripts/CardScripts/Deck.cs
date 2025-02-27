using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "Scriptable Objects/Deck")]

// Default list of all cards 
// NOTE: this list should never be changed directly. 
// If making references to cards, create a copy of this list. 
// Any changes made to this list directly will change the scriptable objects attached. 
public class Deck : ScriptableObject
{
    // References 
    public List<ScriptableObject> cards;
}
