using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private bool zoomState;  // To track the zoom state (whether zoomed in or zoomed out)
    [SerializeField] private Camera mainCamera;  // Reference to the main camera
    [SerializeField] private float zoomSpeed = 5f; // Speed of the zoom transition
    private float targetOrthographicSize; // Target size for zooming

    void Start()
    {
        // If mainCamera is not assigned, automatically assign the main camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main;  // Automatically assign the main camera if not set
        }

        zoomState = false;  // Initial state is zoomed out (or at default zoom)
        targetOrthographicSize = mainCamera.orthographicSize; // Set the initial target size
    }

    // Update is called once per frame
    void Update()
    {
        // Gradually move toward the target size to create a smooth zoom effect
        if (Mathf.Abs(mainCamera.orthographicSize - targetOrthographicSize) > 0.1f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        }
    }

    // Toggle between zooming in and zooming out
    public void Zoom()
    {
        if (zoomState)
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }

    // Zoom in closer to the board (using orthographic camera size)
    public void ZoomIn()
    {
        targetOrthographicSize = 30f;  // Zoom in by setting target orthographic size
        zoomState = true;
    }

    // Zoom out to the default zoom (using orthographic camera size)
    public void ZoomOut()
    {
        targetOrthographicSize = 35f;  // Zoom out by setting target orthographic size
        zoomState = false;
    }
}
