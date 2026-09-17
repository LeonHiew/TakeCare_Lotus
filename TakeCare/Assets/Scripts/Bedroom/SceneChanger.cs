using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;
    CinemachineVirtualCamera pc_cam;
    private bool playerNearby = false;

    private void Awake()
    {
        pc_cam = GameObject.Find("PC Cam").GetComponent<CinemachineVirtualCamera>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            pc_cam.enabled = true;
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

    private void OnMouseDown()
    {
        if (playerNearby)
        {
            pc_cam.enabled = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}