using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class FileSender
    {
        public String name;
        public int size;
        public byte[] Data;
        public FileSender(byte[] Data, String name)
        {
            this.name = name;
            this.Data = Data;
            this.size = Data.Length;
        }
    }
}
