using UnityEngine;
using UnityEngine.UI;

public class EvidencePanelManager : MonoBehaviour
{
    public static EvidencePanelManager Instance;

    [Header("UI References")]
    public GameObject evidencePanel;
    public Text evidenceNameText;
    public Text evidenceDescriptionText;

    private void Awake()
    {
        Instance = this;
        evidencePanel.SetActive(false);
    }

    private void Update()
    {
        if (evidencePanel.activeSelf &&
            (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E)))
        {
            ClosePanel();
        }
    }

    public void ShowEvidence(EvidenceData data)
    {
        evidenceNameText.text = data.evidenceName;
        evidenceDescriptionText.text = data.description;
        evidencePanel.SetActive(true);
    }

    private void ClosePanel()
    {
        evidencePanel.SetActive(false);
    }

    public bool IsPanelActive => evidencePanel.activeSelf;
}