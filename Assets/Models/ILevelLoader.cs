using System.Collections.Generic;

namespace Assets.Models
{
    public interface ILevelLoader
    {
        IEnumerable<int[,]> Load();
    }
}
