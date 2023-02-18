using UnityEngine;

public class WholeBlock : BlockPart
{
    [SerializeField][Range(0,10)] float colliderRadius = 1;
    public bool canRemoveHealth = false;

    private void Start()
    {
        isWhole = true;
        
    }

    private void OnEnable()
    {
       canRemoveHealth = !Samurai.isSamuraiEvent;
    }

    public override void Update()
    {
        CheckOutFromScreen();
        CheckBlockOnCut();
    }


    
    private void CheckBlockOnCut()
    {
        if (DataHolder.Cutter.IsCutMove)
        {
            var dist2Cutter = (DataHolder.Cutter.transform.position - transform.position).magnitude;
            if (colliderRadius >= dist2Cutter)
            {
                DataHolder.AllActiveBlockParts.Remove(this);
                Block.OnCutBlock.Invoke();
            }
        }
    }

    public override void CheckOutFromScreen()
    {
        if (transform.position.y <= DataHolder.DeathZone.StartPoint.y - 3)
        {
            readyToSpawn = true;
            gameObject.SetActive(false);
            Block.OnReadyStatementChanged?.Invoke(isWhole);
            DataHolder.AllActiveBlockParts.Remove(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, colliderRadius);
    }
}
