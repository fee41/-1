using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tree
    {
        public int Id { get; set; }
        public string authName { get; set; }

        public string Url { get; set; }

        public List<Tree> children { get; set; }
    }
}