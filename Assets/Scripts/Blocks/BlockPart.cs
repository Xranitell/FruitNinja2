using UnityEngine;

public class BlockPart: MonoBehaviour
{
    public PhysicalObject physicalObject;
    public RectTransform rectTransform;
    public SpriteRenderer spriteRenderer;
    public Block Block { get; set; }
    
    
    public bool isWhole;
    public bool readyToSpawn = true;

    private void Awake()
    {
        
        isWhole = false;
        Block = GetComponentInParent<Block>();
    }

    private void OnBecameVisible()
    {
        readyToSpawn = false;
        DataHolder.AllActiveBlockParts.Add(this);
    }

    public virtual void Update()
    {
        CheckOutFromScreen();
    }

    public virtual void CheckOutFromScreen()
    {
        if (transform.position.y <= DataHolder.DeathZone.StartPoint.y - 3)
        {
            readyToSpawn = true;
            gameObject.SetActive(false);
            Block.OnReadyStatementChanged?.Invoke(isWhole);
            DataHolder.AllActiveBlockParts.Remove(this);
        }
    }
}