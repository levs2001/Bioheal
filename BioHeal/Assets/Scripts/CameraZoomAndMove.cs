using UnityEngine;

public class CameraZoomAndMove : MonoBehaviour
{
    Vector3? touch;

    [SerializeField] private float zoomMin = 1;
    [SerializeField] private float zoomMax = 2;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake() {
        BoxCollider2D map = Resources.Load<BoxCollider2D>("Map/Map");
        mapMinX = map.bounds.center.x - map.size.x / 2;
        mapMaxX = map.bounds.center.x + map.size.x / 2;
        mapMinY = map.bounds.center.y - map.size.y / 2;
        mapMaxY = map.bounds.center.y + map.size.y / 2;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
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
        else if (Input.GetMouseButton(0))
        {
            if (!touch.HasValue)
            {
                touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            Vector3 direction = touch.Value - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position = ClampCamera(Camera.main.transform.position + direction);
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
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
