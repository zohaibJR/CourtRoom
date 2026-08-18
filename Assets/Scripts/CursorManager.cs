using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DisableCursor();
    }

    private void Update()
    {
        // Press Escape to enable cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EnableCursor();
        }

        // Left click to disable cursor again
        if (Input.GetMouseButtonDown(0))
        {
            DisableCursor();
        }
    }

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}