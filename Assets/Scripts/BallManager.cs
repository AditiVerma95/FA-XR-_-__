using UnityEngine;

public class BallManager : MonoBehaviour
{
    [Header("References")]
    public GameObject ball;

    [Header("Spawn Button")]
    public Renderer spawnButtonRenderer;

    [Header("Materials")]
    public Material normalMaterial;
    public Material disabledMaterial;

    private Rigidbody rb;

    [Header("Stored Spawn Transform")]
    private Vector3 startLocalPos;
    private Quaternion startLocalRot;

    private bool hasSpawned = false;

    void Awake()
    {
        rb = ball.GetComponent<Rigidbody>();

        // Store LOCAL transform relative to table
        startLocalPos = ball.transform.localPosition;
        startLocalRot = ball.transform.localRotation;

        // Hide ball at start
        ball.SetActive(false);

        // Initial button material
        if (spawnButtonRenderer != null && normalMaterial != null)
        {
            spawnButtonRenderer.material = normalMaterial;
        }
    }

    public void SpawnBall()
    {
        // Prevent multiple spawns
        if (hasSpawned)
            return;

        hasSpawned = true;

        ball.SetActive(true);

        ResetBallPhysics();

        // Make spawn button grey
        if (spawnButtonRenderer != null &&
            disabledMaterial != null)
        {
            spawnButtonRenderer.material = disabledMaterial;
        }

        Debug.Log("Spawn Ball Called");
    }

    public void ResetBall()
    {
        if (!ball.activeSelf)
            return;

        ResetBallPhysics();

        Debug.Log("Reset Ball Called");
    }

    private void ResetBallPhysics()
    {
        // Temporarily disable physics
        rb.isKinematic = true;

        // Stop movement
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Restore LOCAL transform
        ball.transform.localPosition =
            startLocalPos + Vector3.up * 0.03f;

        ball.transform.localRotation = startLocalRot;

        // Re-enable physics
        Invoke(nameof(EnablePhysics), 0.02f);
    }

    private void EnablePhysics()
    {
        rb.isKinematic = false;
    }
}