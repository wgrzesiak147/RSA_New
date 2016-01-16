using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RSA_new {
    public class CGlobalManager {
        public static List<CRoute> GlobalRoutesList = new List<CRoute>();
        public static List<CLink> GlobalLinkList = new List<CLink>();
        public static void LoadLinks(string _topologyFilePath) {
            if (GlobalLinkList == null) {

            }
        }
        public static void LoadRoutes(string _routeFilePath) {
            if (GlobalRoutesList.Count == 0) {
                CGlobalManager _manager = new CGlobalManager();
                using (StreamReader routeFile = new StreamReader(_routeFilePath)) {
                    int RoutesQuantity = 0;
                    int lineCounter = 0;
                    int startNodeNumber = 0;
                    int endNodeNumber = 1;
                    string line;
                    while ((line = routeFile.ReadLine()) != null) {
                        if (lineCounter == 0) { Int32.TryParse(line, out RoutesQuantity); lineCounter++; }
                        else {
                            List<int> _line = line.Split(' ').Select(Int32.Parse).ToList();
                            List<int> linkIndexes = _manager.CalculateRouteFromBinary(_line);
                            GlobalRoutesList.Add(new CRoute(linkIndexes));
                            #region OLDCODE - can be deleted I think...
                            //if (startNodeNumber == endNodeNumber) { endNodeNumber++; }
                            //Incrementing index for route
                            //if (currRoute != null) {
                            //    RoutesCount++;
                            //    AllRoutes.Add(currRoute);
                            //}
                            //#region Uncoment for simple checkup of line reading ;)
                            //if (lineCounter%390 == 0) {
                            //    int i = 0;
                            //    i = 1 + 2;
                            //} 
                            //#endregion
                            //if (lineCounter % 30 == 0) { endNodeNumber++; }
                            //if (lineCounter % 390 == 0) { startNodeNumber++; endNodeNumber = 0; }
                            //lineCounter++;
                            #endregion
                        }
                    }
                }
            }
        }

        private List<int> CalculateRouteFromBinary(List<int> line) {
            List<int> result = new List<int>();
            int counter = 0;
            foreach (var element in line) {
                if (element == 1) { //in case of route creation: if element == 1 it means that this link is used in this route
                    result.Add(counter);
                }
                counter++;
            }
            // Returns indexes of links
            return result;
        }

        private List<int> CalculateFromLine(List<int> line) {
            List<int> result = new List<int>();
            int counter = 0;
            foreach (var element in line) {
                if (element == 1) { //in case of route creation: if element == 1 it means that this link is used in this route
                    result.Add(counter);
                }
                counter++;
            }
            return result;
        }
    }
}
