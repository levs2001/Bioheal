using UnityEngine;

public class CameraZoomAndMove : MonoBehaviour
{
    // Position where the user clicked to move the camera
    Vector3? touch;

    // Maximum and minimum value to zoom
    [SerializeField] private float zoomMin = 1;
    [SerializeField] private float zoomMax = 2;

    // Map bounds
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake()
    {
        BoxCollider2D map = Resources.Load<BoxCollider2D>("Map/Map");
        mapMinX = map.bounds.center.x - map.size.x / 2;
        mapMaxX = map.bounds.center.x + map.size.x / 2;
        mapMinY = map.bounds.center.y - map.size.y / 2;
        mapMaxY = map.bounds.center.y + map.size.y / 2;
    }

    private void Update()
    {
        // If user clicked on the screen, then remember the position of the click
        if (Input.GetMouseButtonDown(0))
        {
            touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // If user pressed with two fingers, then zoom
        if (Input.touchCount == 2)
        {
            CameraZoomForPhone();
        }
        // If user keeps pressing, then make camera move
        else if (Input.GetMouseButton(0))
        {
            MoveCamera();
        }

        // For scroll zooming
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void CameraZoomForPhone()
    {
        Touch firstTouch = Input.GetTouch(0);
        Touch secondTouch = Input.GetTouch(1);

        Vector2 firstTouchLastPos = firstTouch.position - firstTouch.deltaPosition;
        Vector2 secondTouchLastPos = secondTouch.position - secondTouch.deltaPosition;

        float distTouch = (firstTouchLastPos - secondTouchLastPos).magnitude;
        float curDistTouch = (firstTouch.position - secondTouch.position).magnitude;

        float difference = curDistTouch - distTouch;

        Zoom(0.01f * difference);
        touch = null;
    }

    private void MoveCamera()
    {
        if (!touch.HasValue)
        {
            touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        Vector3 direction = touch.Value - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Camera.main.transform.position = ClampCamera(Camera.main.transform.position + direction);
    }

    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomMin, zoomMax);
        Camera.main.transform.position = ClampCamera(Camera.main.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = Camera.main.orthographicSize;
        float camWidth = Camera.main.orthographicSize * Camera.main.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;

        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
