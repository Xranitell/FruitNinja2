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
        var blockParts = DataHolder.AllActiveBlockParts;

        while (timer < duration)
        {
            timer += updateInterval;

            foreach (var part in blockParts)
            {
                if (part.Block.GetType() != typeof(Bomb))
                {
                    var moveVector = wholeBlock.rectTransform.position - part.rectTransform.position;
                    blockParts.ForEach(x=>x.physicalObject.UseGravity = false);
                    part.physicalObject.AddForce(moveVector,timer * speed);
                }
            }
            
            blockParts.ForEach(x=>x.physicalObject.UseGravity = true);
            yield return new WaitForSeconds(updateInterval);
        }
    }

}