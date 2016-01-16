using System;
using System.Collections.Generic;

namespace RSA_new {
    class Program {
        static void Main(string[] args) {
            CGlobalManager _manager = new CGlobalManager();
            CGlobalManager.LoadLinks(@"D:\maciek\Magister\semestr 2\PSK\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\dd.net");
            CGlobalManager.LoadRoutes(@"D:\maciek\Magister\semestr 2\PSK\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\d.pat");
            CGlobalManager.LoadRequest(@"D:\maciek\Magister\semestr 2\PSK\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\51.dem");
            //CRoute newRoute = new CRoute(newList);
            //foreach (var node in newRoute.ReturnPassedNodes()) {
            //    Console.WriteLine(node);
            //}
        }
    }
}
