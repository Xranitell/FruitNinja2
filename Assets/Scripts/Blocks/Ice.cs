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
        
        
        StartCoroutine(SlowTime(Time.deltaTime, iceInfo.timeDuration));
        
    }

    IEnumerator SlowTime(float delay, float duration)
    {
        FreezeEffect.StartFreezeScreen();
        var timer = 0f;
        while (timer < duration)
        {
            timer += delay;
            var timeScale = ((IceInfo)blockInfo).stopTimeAnimation.Evaluate(timer);
            Time.timeScale = timeScale;
            yield return new WaitForSecondsRealtime(delay);
        }
        FreezeEffect.EndFreezeScreen();
        
    }

    protected override void BlockOutFromScreen(bool isWhole)
    { 
        base.BlockOutFromScreen(isWhole);
    }
}
