using System.Collections.Generic;
using UnityEngine; 
[System.Serializable]
public class Dialogue
{
    public string npcName;
    public AudioClip npcVoiceClip; // Son propre au PNJ

    [TextArea(3, 20)]
    public List<string> npcSentences;

    [TextArea(3, 20)]
    public List<string> playerSentences;

    public Dialogue(string npcName, AudioClip npcVoiceClip, List<string> npcSentences, List<string> playerSentences)
    {
        this.npcName = npcName;
        this.npcVoiceClip = npcVoiceClip;
        this.npcSentences = npcSentences;
        this.playerSentences = playerSentences;
    }
}