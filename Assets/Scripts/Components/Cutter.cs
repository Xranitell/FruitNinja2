using UnityEngine;

public class Cutter : MonoBehaviour
{
    public Vector2 sliceVector;
    public float speed;
    
    private TrailRenderer _sliceTrail;
    private Camera _mainCamera;
    

    private void Awake()
    {
        DataHolder.Cutter = this;
    }
    private void Start()
    {
        _sliceTrail = GetComponent<TrailRenderer>();
        _mainCamera = Camera.main;
    }
    void Update()
    {
        if (Input.touches.Length > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                MoveTrail(touch);
                speed = Mathf.Sqrt(Mathf.Pow(touch.deltaPosition.x / Time.deltaTime,2) + Mathf.Pow(touch.deltaPosition.y / Time.deltaTime,2));
                sliceVector = touch.deltaPosition;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                speed = 0;
            }
            else if(touch.phase == TouchPhase.Began)
            {
                MoveTrail(touch);
            }
        }
    }
    void MoveTrail(Touch touch)
    {
        var pointOnCanvas = _mainCamera.ScreenToWorldPoint(touch.position);
        pointOnCanvas.z = 0;
        _sliceTrail.transform.position = pointOnCanvas;
    }
}
