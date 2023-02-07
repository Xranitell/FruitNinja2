using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Block
{
    protected override void BlockCut()
    {
        base.BlockCut();
        AddForceForAllBlocks();
        DataHolder.HealthManager.ChangeHealthValue(-1);
    }

    void AddForceForAllBlocks()
    {
        foreach (var blockPart in DataHolder.PullOfBlockParts)
        {
            blockPart.PhysicalObject.AddForce(blockPart.transform.position - transform.position,10);
        }
    }

    protected override void BlockOutFromScreen(bool isWhole)
    {
        base.BlockOutFromScreen(isWhole);
    }
}
