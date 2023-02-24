public class Life : Block
{
    protected override void BlockCut()
    {
        base.BlockCut();
        LifeAnimationManager.StartAnimation(wholeBlock.transform.position);
    }

    protected override void BlockOutFromScreen(bool isWhole)
    {
        base.BlockOutFromScreen(isWhole);
    }
}
