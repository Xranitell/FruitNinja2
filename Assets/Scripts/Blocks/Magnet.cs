using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet: Block
{
    protected override void BlockCut()
    {
        base.BlockCut();
        StartCoroutine(AttractBlockParts(0.2f, 3 , 2));
    }

    IEnumerator AttractBlockParts(float updateInterval, float duration, float speed)
    {
        var timer = 0f;
        var blockParts = DataHolder.PullOfBlockParts;

        while (timer < duration)
        {
            timer += updateInterval;

            foreach (var part in blockParts)
            {
                var moveVector = wholeBlock.rectTransform.position - part.rectTransform.position;
                
                blockParts.ForEach(x=>x.physicalObject.useGravity = false);
                part.physicalObject.AddForce(moveVector,timer * speed);
            }
            
            blockParts.ForEach(x=>x.physicalObject.useGravity = true);
            yield return new WaitForSeconds(updateInterval);
        }
    }

}