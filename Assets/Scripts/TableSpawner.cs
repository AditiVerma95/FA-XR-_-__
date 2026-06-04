using UnityEngine;

public class TableSpawner : MonoBehaviour
{
    public Transform xrCamera;

    public GameObject table;
    public GameObject instructionText;

    public OVRHand rightHand;

    private bool wasPinching = false;

    void Start()
    {
        table.SetActive(false);
    }

    void Update()
    {
        if (rightHand == null)
            return;

        if (!rightHand.IsTracked)
            return;

        bool grabbing =
            rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Middle) > 0.7f &&
            rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Ring) > 0.7f;

        if (grabbing && !wasPinching)
        {
            SpawnTable();
        }

        wasPinching = grabbing;
    }

    private void SpawnTable()
    {
        Vector3 cameraForward = xrCamera.transform.forward.normalized;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 spawnPosition = xrCamera.transform.position + (cameraForward * 0.6f) + new Vector3(0f, -0.3f, 0f);

        table.transform.position = spawnPosition;

        table.transform.LookAt(xrCamera);
        
        Vector3 tableRotation = table.transform.rotation.eulerAngles;
        tableRotation.x = 0f;
        tableRotation.y += 180f;
        tableRotation.z = 0f;

        table.transform.rotation = Quaternion.Euler(tableRotation);

        table.SetActive(true);
    }

   
}