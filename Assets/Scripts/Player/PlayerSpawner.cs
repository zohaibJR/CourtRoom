//Attached on Empty Object
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Spawn Point")]
    public Transform spawnPoint;

    public void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab is not assigned!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn Point is not assigned!");
            return;
        }

        Instantiate(
            playerPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }
}