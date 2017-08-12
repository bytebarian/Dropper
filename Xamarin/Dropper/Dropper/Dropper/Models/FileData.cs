using System;
using System.Collections.Generic;

namespace Dropper.Models
{
    public class FileData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
