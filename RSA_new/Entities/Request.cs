using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_new.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public int StartNode { get; set; }
        public int EndNode { get; set; }
        public int Size { get; set; }

        public Request(int startNode, int endNode, int size){
            StartNode = startNode;
            EndNode = endNode;
            Size = size;
        }
    }
}
