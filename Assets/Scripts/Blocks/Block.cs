using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Block : MonoBehaviour
{
    public WholeBlock wholeBlock;
    public List<BlockPart> blockParts = new List<BlockPart>();
    public ParticleSystem destroyParticle;
    
    public UnityAction<bool> OnReadyStatementChanged;
    public UnityAction OnCutBlock;

    public BlockInfo blockInfo;
    
    protected virtual void Start()
    {
        OnReadyStatementChanged += BlockOutFromScreen;
        OnCutBlock += BlockCut;
    }

    protected virtual void BlockCut()
    {
        wholeBlock.readyToSpawn = true;
        destroyParticle.transform.position = wholeBlock.transform.position;
        destroyParticle.Play();
        
        SliceBlock();
    }

    protected virtual void BlockOutFromScreen(bool isWhole)
    {
        if (blockParts[0].readyToSpawn &&
            blockParts[1].readyToSpawn &&
            wholeBlock.readyToSpawn)
        {
            DataHolder.Pull.Add(this);
        }
    }

    void SliceBlock()
    {
        wholeBlock.gameObject.SetActive(false);
        
        var sliceVector = DataHolder.Cutter.sliceVector;
        var perpendicular = sliceVector.x > 0? Vector2.Perpendicular(sliceVector): -Vector2.Perpendicular(sliceVector);
        var degAngle = Vector2.Angle(perpendicular, Vector2.right) ;
        var radAngle =degAngle * Mathf.Deg2Rad;
        
        for (int i = 0; i < blockParts.Count; i++)
        {
            blockParts[i].gameObject.SetActive(true);
            var spriteWidth = blockParts[i].rectTransform.rect.width / 2;
            Vector3 offset = new Vector2(spriteWidth * Mathf.Cos(radAngle), spriteWidth * Mathf.Sin(radAngle));
            Vector3 moveDir = Vector2.Perpendicular(sliceVector);

            ReworkDirAndOffset(sliceVector, i,ref moveDir, ref offset);
            
            blockParts[i].transform.position = wholeBlock.transform.position;
            
            if (blockInfo.useOffsetAndRotation)
            {
                blockParts[i].transform.position += (offset * blockParts[i].transform.localScale.x);
                blockParts[i].transform.rotation = Quaternion.AngleAxis(degAngle,Vector3.forward);
            }

            blockParts[i].transform.localScale = wholeBlock.transform.localScale;
            var wholePhysicalObject = wholeBlock.physicalObject;
            var impulse = new Vector3(wholePhysicalObject.Velocity.x, wholePhysicalObject.Velocity.y,0);
            blockParts[i].physicalObject.AddForce(moveDir,DataHolder.Cutter.strengthOfCut, impulse);
        }
    }

    void ReworkDirAndOffset(Vector2 sliceVector, int i, ref Vector3 moveDir, ref Vector3 offset)
    {
        if (sliceVector.x>0 && i == 0 || sliceVector.x <= 0 && i != 0)
        {
            moveDir *= -1;
        }
        if ((sliceVector.x>0 && i == 0) || (sliceVector.x <= 0 && i == 0))
        {
            offset *= -1;
        }
    }
}
