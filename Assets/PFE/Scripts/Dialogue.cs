using System.Collections.Generic;
using UnityEngine;  

[System.Serializable]
public class Dialogue
{
    public string npcName;

    [TextArea(3, 20)]
    public List<string> npcSentences;

    [TextArea(3, 20)]
    public List<string> playerSentences;

    public Dialogue(string npcName, List<string> npcSentences, List<string> playerSentences)
    {
        this.npcName = npcName;
        this.npcSentences = npcSentences;
        this.playerSentences = playerSentences;
    }
}
