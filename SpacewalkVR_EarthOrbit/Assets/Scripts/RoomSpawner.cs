using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class RoomSpawner : MonoBehaviour
{
    [Header("XRI System")]
    [SerializeField] private TeleportationProvider teleportationProvider;

    [Header("Spawn Target")]
    [SerializeField] private Transform targetSpawnPoint;
    [SerializeField] private bool spawnOnStart = false;

    private void Start()
    {
        if (teleportationProvider == null)
        {
            teleportationProvider = FindFirstObjectByType<TeleportationProvider>();
        }

        if (spawnOnStart)
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        if (teleportationProvider == null || targetSpawnPoint == null) return;

        // Uses the exact XYZ position and rotation of your spawn point.
        // If it is slightly above the ground, gravity will pull the player down.
        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = targetSpawnPoint.position,
            destinationRotation = targetSpawnPoint.rotation,
            matchOrientation = MatchOrientation.TargetUpAndForward
        };

        teleportationProvider.QueueTeleportRequest(request);
    }

    public void SpawnPlayerTo(Transform newTarget)
    {
        targetSpawnPoint = newTarget;
        SpawnPlayer();
    }
}