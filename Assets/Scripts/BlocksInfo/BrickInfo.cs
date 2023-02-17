using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Blocks/Brick")]
public class BrickInfo : BlockInfo
{
    public override Type BlockType { get; set; } = typeof(Brick);

}
