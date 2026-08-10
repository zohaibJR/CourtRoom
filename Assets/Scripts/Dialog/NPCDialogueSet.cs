using UnityEngine;

[System.Serializable]
public class NPCDialogueSet
{
    [Tooltip("Must match the NpcId on the NPC's DialogueTrigger in the scene")]
    public string npcId;

    public DialogueLine[] lines;
}