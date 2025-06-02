namespace Assets.Scripts.Runtime.Interaction.Platforms
{
    internal class DestroyPlatform : BasePlatform
    {
        public override void Interaction()
        {
            Destroy(gameObject);
        }
    }
}
