using System;

namespace Assets.Models
{
    public static class Events
    {
        public static event Action<Block> BlockDestroyed;
        public static void RaiseBlockDestroyed(Block block)
        {
            BlockDestroyed?.Invoke(block);
        }
    }
}
