public class ManaBarView : StatBarView<Mana>
{
    protected override void SubscribeEvents()
    {
        stat.OnSpend += StartLerp;
        stat.OnRestore += StartLerp;
    }
}
