using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookersIntegration.Model
{
   
    public class FileFormatModel
    {
        public string Name { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int Length { get; set; }
        public int TotalSize { get; set; }

        public FileFormatModel()
        { }
        public FileFormatModel(string name, int startIndex, int endIndex, int length)
        {
            this.Name = name;
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
            this.Length = length;
        }

    }
}
