using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public Text speakerNameText;
    public Text dialogueBodyText;

    private DialogueLine[] currentLines;
    private int currentIndex;
    private Action onDialogueComplete;

    private void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf &&
            (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)))
        {
            Advance();
        }
    }

    public void StartDialogue(DialogueLine[] lines, Action onComplete = null)
    {
        if (lines == null || lines.Length == 0) return;

        currentLines = lines;
        currentIndex = 0;
        onDialogueComplete = onComplete;

        dialoguePanel.SetActive(true);
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        DialogueLine line = currentLines[currentIndex];
        speakerNameText.text = line.speakerName;
        dialogueBodyText.text = line.text;
    }

    public void Advance()
    {
        if (!dialoguePanel.activeSelf) return;

        currentIndex++;
        if (currentIndex >= currentLines.Length)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        onDialogueComplete?.Invoke();
        onDialogueComplete = null;
    }

    public bool IsDialogueActive => dialoguePanel.activeSelf;
}