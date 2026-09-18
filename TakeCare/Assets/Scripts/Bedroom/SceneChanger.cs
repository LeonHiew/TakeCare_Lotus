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
        pc_cam = GetComponent<CinemachineVirtualCamera>();

        if (pc_cam == null)
        {
            Debug.LogError(
                "SceneChanger: No CinemachineVirtualCamera found on this GameObject.",
                this
            );
            return;
        }

        pc_cam.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            pc_cam.enabled = false;
        }
    }

    private void Update()
    {
        if (!playerNearby)
            return;

        // Space toggles camera on/off
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pc_cam.enabled = !pc_cam.enabled;
        }

        // E changes scene
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}