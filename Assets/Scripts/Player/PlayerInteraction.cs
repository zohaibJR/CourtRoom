using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public Camera playerCamera;
    public float interactionDistance = 3f;

    private void Update()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive)
        {
            return; // dialogue manager handles Space/E itself while open
        }

        CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                Debug.Log("Press E");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (playerCamera == null)
            return;

        Gizmos.color = Color.red;

        Vector3 start = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;
        Vector3 end = start + direction * interactionDistance;

        Gizmos.DrawLine(start, end);
    }
}