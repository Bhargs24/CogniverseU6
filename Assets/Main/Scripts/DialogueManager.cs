using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public DialogueData currentDialogue;

    private int dialogueIndex = 0;

    public void StartDialogue(DialogueData dialogue)
    {
        currentDialogue = dialogue;
        dialogueIndex = 0;
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
            return;
        }

        var line = currentDialogue.dialogueLines[dialogueIndex];
        subtitleText.text = line.dialogueText;

        if (!line.isPlayer)
        {
            CharacterManager.Instance.PlayAudio(line.characterID, line.voiceOverClip);
            CharacterManager.Instance.PlayAnimation(line.characterID, line.animationTrigger);
        }

        dialogueIndex++;
    }

    private void EndDialogue()
    {
        subtitleText.text = "";
        Debug.Log("Dialogue Ended.");
    }
}
