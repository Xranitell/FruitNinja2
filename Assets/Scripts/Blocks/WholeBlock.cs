using UnityEngine;

public class WholeBlock : BlockPart
{
    [SerializeField][Range(0,10)] float colliderRadius = 1;

    private void Start()
    {
        isWhole = true;
    }

    public override void Update()
    {
        base.Update();
        CheckBlockOnCut();
    }
    
    private void CheckBlockOnCut()
    {
        if (DataHolder.Cutter.isCutMove)
        {
            var dist2Cutter = (DataHolder.Cutter.transform.position - transform.position).magnitude;
            if (colliderRadius >= dist2Cutter)
            {
                DataHolder.AllActiveBlockParts.Remove(this);
                Block.OnCutBlock.Invoke();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, colliderRadius);
    }
}
