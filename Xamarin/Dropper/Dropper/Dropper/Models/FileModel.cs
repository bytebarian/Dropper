using System;
using System.Collections.Generic;
using System.IO;

namespace Dropper.Models
{
    public class FileModel
    {
        public FileModel() { }

        public FileModel(IDictionary<string, object> dict)
        {

        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public Stream Stream { get; set; }

        public IDictionary<string, object> ToDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
