using System;
using System.Collections.Generic;

namespace RSA_new {
    class Program {
        static void Main(string[] args) {
            CGlobalManager _manager = new CGlobalManager();
            CGlobalManager.LoadLinks(@"e:\Studia\Goścień\DT14_1.75_Tbps\dd.net");
            CGlobalManager.LoadRoutes(@"e:\Studia\Goścień\DT14_1.75_Tbps\d.pat");
            CGlobalManager.LoadRequest(@"e:\Studia\Goścień\DT14_1.75_Tbps\51.dem");
            CGlobalManager.LoadRouteSlotMappings(@"e:\Studia\Goścień\DT14_1.75_Tbps\d1.spec");
            //CRoute newRoute = new CRoute(newList);
            //foreach (var node in newRoute.ReturnPassedNodes()) {
            //    Console.WriteLine(node);
            //}
        }
    }
}
