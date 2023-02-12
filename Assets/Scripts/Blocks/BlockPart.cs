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
        DataHolder.PullOfBlockParts.Add(this);
        isWhole = false;
        Block = GetComponentInParent<Block>();
    }

    private void OnBecameVisible()
    {
        readyToSpawn = false;
    }

    public virtual void Update()
    {
        if (transform.position.y <= DataHolder.DeathZone.StartPoint.y - 3)
        {
            readyToSpawn = true;
            gameObject.SetActive(false);
            Block.OnReadyStatementChanged?.Invoke(isWhole);
        }
    }
}