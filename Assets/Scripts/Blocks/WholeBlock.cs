using UnityEngine;

public class WholeBlock : BlockPart
{
    [SerializeField][Range(0,10)] float colliderRadius = 1;
    bool canRemoveHealth = false;

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
        if(canRemoveHealth)
        {
            CheckOutFromScreen();
        }
        
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

    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, colliderRadius);
    }
}
