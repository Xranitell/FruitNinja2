public class Life : Block
{
    protected override void BlockCut()
    {
        base.BlockCut();
        AdditiveLifeAnimation.StartAnimation(wholeBlock.rectTransform.position);
    }

    protected override void BlockOutFromScreen(bool isWhole)
    {
        base.BlockOutFromScreen(isWhole);
    }
}
