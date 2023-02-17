using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : Block
{
    protected override void BlockCut()
    {
        base.BlockCut();
        DataHolder.Cutter.TouchIsActive = false;
    }
}
