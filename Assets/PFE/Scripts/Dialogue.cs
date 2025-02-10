using System.Collections.Generic;  

[System.Serializable]
public class Dialogue
{
    public string npcName; 
    public List<string> npcSentences; 
    public List<string> playerSentences; 

    public Dialogue(string npcName, List<string> npcSentences, List<string> playerSentences)
    {
        this.npcName = npcName;
        this.npcSentences = npcSentences;
        this.playerSentences = playerSentences;
    }
}
