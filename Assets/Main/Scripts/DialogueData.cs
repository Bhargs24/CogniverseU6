using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string characterID;
        public string dialogueText;
        public AudioClip voiceOverClip;
        public string animationTrigger;
        public bool isPlayer;
    }

    public List<DialogueLine> dialogueLines;
}