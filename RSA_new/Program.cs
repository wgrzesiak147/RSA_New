using System;
using System.Collections.Generic;

namespace RSA_new {
    class Program {
        static void Main(string[] args) {
            CGlobalManager _manager = new CGlobalManager();
            CLink newLink = new CLink(0,0, 1,200);
            CLink newLink2 = new CLink(1,2, 1,100);
            CGlobalManager.GlobalLinkList.Add(new CLink(0, 0, 1, 200));
            CGlobalManager.GlobalLinkList.Add(new CLink(1, 2, 1, 100));
            List<CLink> newList = new List<CLink>();
            newList.Add(newLink);
            newList.Add(newLink2);
            CGlobalManager.LoadRoutes(@"e:\Studia\Goścień\DT14_1.75_Tbps\d.pat");
            //CRoute newRoute = new CRoute(newList);
            //foreach (var node in newRoute.ReturnPassedNodes()) {
            //    Console.WriteLine(node);
            //}
        }
    }
}
