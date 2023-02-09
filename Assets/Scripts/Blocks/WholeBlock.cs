using UnityEngine;

public class WholeBlock : BlockPart
{
    private Rect _rect;

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
        var spriteRect = rectTransform.rect;
        var offset = new Vector3(spriteRect.width/2, spriteRect.height/2);
        _rect = new Rect(transform.position - offset*1.5f,spriteRect.size*1.5f);
        
        if (DataHolder.Cutter.isCutMove)
        {
            if (_rect.Contains(DataHolder.Cutter.transform.position))
            {
                Block.OnCutBlock.Invoke();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_rect.min,_rect.max);
    }
}
