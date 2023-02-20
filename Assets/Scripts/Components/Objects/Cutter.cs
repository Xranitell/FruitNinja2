using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField] TrailRenderer sliceTrail;
    [SerializeField] public float strengthOfCut; 
    [SerializeField] [Range(0,500)] public float speedCutterForSlice = 500f;
    
    public Vector2 sliceVector;
    public bool _touchIsActive;
    public bool TouchIsActive
    {
        get => _touchIsActive;
        set
        {
            _touchIsActive = value;
            IsCutMove = value;
        }
    }
    public bool IsCutMove
    {
        get => _isCutMove;
        private set
        {
            _isCutMove = value;
            sliceTrail.startColor =Color.Lerp(sliceTrail.startColor,IsCutMove? Color.red:Color.gray,1f) ;
        }
    }
    public bool _isCutMove;
    
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
        
        if (Input.GetMouseButtonUp(0))
        {
            StopCut();
            IsCutMove = false;
            TouchIsActive = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            sliceTrail.enabled = true;
            TouchIsActive = true;
            transform.position = DataHolder.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            sliceTrail.Clear();
        }
    }

    void MoveCut(Vector3 touchPosition)
    {
        if(TouchIsActive)
        {
            var pointOnCanvas = DataHolder.MainCamera.ScreenToWorldPoint(touchPosition);
            pointOnCanvas.z = 0;

            transform.position = Vector3.Lerp(transform.position, pointOnCanvas, 1f);

            sliceVector = transform.position - _lastPos;
            _speed = sliceVector.magnitude / Time.deltaTime;

            if (_speed > speedCutterForSlice)
            {
                IsCutMove = true;
            }
            else
            {
                IsCutMove = false;
            }
        ;

            _lastPos = transform.position;
        }
    }

    void StopCut()
    {
        _speed = 0;
        sliceTrail.enabled = false;
        
    }
}
