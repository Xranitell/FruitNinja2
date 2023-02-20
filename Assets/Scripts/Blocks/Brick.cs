using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : Block
{
    protected override void BlockCut()
    {
        DataHolder.Cutter.TouchIsActive = false;
        destroyParticle.transform.position = wholeBlock.transform.position;
        destroyParticle.Play();
    }
}
