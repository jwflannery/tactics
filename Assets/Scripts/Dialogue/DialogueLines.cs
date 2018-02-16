using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Dialogue/Lines", order = 1)]
public class DialogueLines : ScriptableObject {
    public string[] lines =
    {
        "Hi.",
        "This is a set of lines.",
        "If you can read this...",
        "It means the dialogue system is working!"
    };
}