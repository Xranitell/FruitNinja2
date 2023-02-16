using DG.Tweening;
using UnityEngine;

public class Bomb : Block
{
    protected override void BlockCut()
    {
        base.BlockCut();
        DataHolder.HealthManager.ChangeHealthValue(-1);
        AddExplosionForce();
        DataHolder.MainCamera.transform.DOShakePosition(1f,new Vector3(1,1,0),10);
    }

    void AddExplosionForce()
    {
        foreach (var part in DataHolder.AllActiveBlockParts)
        {
            var throwVector = part.transform.position - wholeBlock.transform.position;
            part.physicalObject.AddForce(throwVector,1/throwVector.magnitude *
                ((BombInfo)blockInfo).explosionForce);
        }
    }

    protected override void BlockOutFromScreen(bool isWhole)
    {
        base.BlockOutFromScreen(isWhole);
    }
    
}
