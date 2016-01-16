using System;
using System.Collections.Generic;

namespace RSA_new {
    class Program {
        static void Main(string[] args) {
            CLink newLink = new CLink(100,0, 1,200);
            CLink newLink2 = new CLink(101,1, 2,100);
            List<CLink> newList = new List<CLink>();
            newList.Add(newLink);
            newList.Add(newLink2);
            CRoute newRoute = new CRoute(newList);
            foreach (var node in newRoute.ReturnPassedNodes()) {
                Console.WriteLine(node);
            }
        }
    }
}
