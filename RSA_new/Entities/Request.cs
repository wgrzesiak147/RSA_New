using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_new.Entities
{
    public class CRequest
    {
        public int Id { get; set; }
        public int StartNode { get; set; }
        public int EndNode { get; set; }
        public int Size { get; set; }

        public CRequest(int id, int startNode, int endNode, int size)
        {
            Id = id;
            StartNode = startNode;
            EndNode = endNode;
            Size = size;
        }
        /*
        public CRequest(int startNode, int endNode, int size){
            StartNode = startNode;
            EndNode = endNode;
            Size = size;
        }
        */
    }
}
