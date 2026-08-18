using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour, IInteractable
{
    public string characterName;
    public DialogueLine[] lines;
    public CaseSpawner.CharacterRole role;

    public void SetCharacter(CharacterInfo info)
    {
        characterName = info.characterName;
        lines = info.lines;
    }

    public void SetRole(CaseSpawner.CharacterRole newRole)
    {
        role = newRole;
    }

    public void Interact()
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning($"{gameObject.name} has no dialogue lines assigned.");
            return;
        }

        DialogueManager.Instance.StartDialogue(lines, OnDialogueFinished);
    }

    private void OnDialogueFinished()
    {
        CaseSpawner.Instance.ReportInteraction(role);
    }
}