namespace Assets.Models
{
    public static class Events
    {
        public delegate void BlockDestroyedHandler(Block block);
        public static event BlockDestroyedHandler BlockDestroyed;
        public static void RaiseBlockDestroyed(Block block)
        {
            BlockDestroyed?.Invoke(block);
        }
    }
}
