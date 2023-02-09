using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField] TrailRenderer sliceTrail;
    [SerializeField] [Range(0,1)] private float needSpeedToCut;
    [SerializeField] public float strengthOfCut;
    
    public Vector2 sliceVector;
    public bool isCutMove;
    
    private float _speed;
    private Vector3 _lastPos;
    

    private void Awake()
    {
        DataHolder.Cutter = this;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveCut(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCut();
        }
    }

    void MoveCut(Vector3 touchPosition)
    {

        var pointOnCanvas = DataHolder.MainCamera.ScreenToWorldPoint(touchPosition);
        pointOnCanvas.z = 0;

        transform.position = Vector3.Lerp(transform.position,pointOnCanvas,1f) ;

        sliceVector = transform.position - _lastPos;
        _speed = sliceVector.magnitude / Time.deltaTime;

        isCutMove = _speed > needSpeedToCut;

        _lastPos = transform.position;
    }

    void StopCut()
    {
        _speed = 0;
    }
}
