using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Assets.Models
{
    public class JsonLevelLoader : ILevelLoader
    {
        private readonly JsonLevelConfig _levels;
        public JsonLevelLoader(string json)
        {
            _levels = JsonConvert.DeserializeObject<JsonLevelConfig>(json);
        }

        public IEnumerable<int[,]> Load()
        {
            return _levels.Levels.Select(level => level.Blocks);
        }
    }
}
