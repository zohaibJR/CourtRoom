using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenuPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateMainMenuPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMainMenuPanel()
    {
        MainMenuPanel.SetActive(true);
    }
}

