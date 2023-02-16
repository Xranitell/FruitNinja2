using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Ice : Block
{
    protected override void BlockCut()
    {
        var iceInfo = blockInfo as IceInfo;
        base.BlockCut();
        TimeManager.Instance.ChangeTimeScale(iceInfo.updateRate,iceInfo.timeDuration,iceInfo.stopTimeAnimation);
    }

    protected override void BlockOutFromScreen(bool isWhole)
    { 
        base.BlockOutFromScreen(isWhole);
    }
}
