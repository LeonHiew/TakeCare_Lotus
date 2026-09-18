using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;

    private CinemachineVirtualCamera pc_cam;
    private bool playerNearby = false;

    private void Awake()
    {
        // Get the Cinemachine camera on this same GameObject
        pc_cam = GetComponent<CinemachineVirtualCamera>();

        if (pc_cam == null)
        {
            Debug.LogError(
                "SceneChanger: No CinemachineVirtualCamera found on this GameObject.",
                this
            );
        }

        // Camera starts disabled
        pc_cam.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            // Enable camera when player enters trigger
            pc_cam.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            // Disable camera when player leaves trigger
            pc_cam.enabled = false;
        }
    }

    private void Update()
    {
        // Press E while player is inside the trigger
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}