using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RSA_new {
    class CGlobalManager {
        public static List<CRoute> GlobalRoutesList = null;
        public static List<CLink> GlobalLinkList = null;
        public static void LoadLinks(string _topologyFilePath) {
            if (GlobalLinkList == null) {

            }
        }
        public static void LoadRoutes(string _routeFilePath) {
            if (GlobalRoutesList == null) {
                CGlobalManager _manager = new CGlobalManager();
                using (StreamReader routeFile = new StreamReader(_routeFilePath)) {
                    int lineCounter = 0;
                    int startNodeNumber = 0;
                    int endNodeNumber = 1;
                    string line;
                    while ((line = routeFile.ReadLine()) != null) {
                        if (lineCounter == 0) { Int32.TryParse(line, out RoutesQuantity); lineCounter++; }
                        else {
                            List<int> _line = line.Split(' ').Select(Int32.Parse).ToList();
                            if (startNodeNumber == endNodeNumber) { endNodeNumber++; }
                            List<int> linkIndexes = _manager.CalculateRouteFromBinary(_line);
                            CRoute currRoute = new CRoute(linkIndexes);
                            //Incrementing index for route
                            if (currRoute != null) {
                                RoutesCount++;
                                AllRoutes.Add(currRoute);
                            }
                            #region Uncoment for simple checkup of line reading ;)
                            //if (lineCounter%390 == 0) {
                            //    int i = 0;
                            //    i = 1 + 2;
                            //} 
                            #endregion
                            if (lineCounter % 30 == 0) { endNodeNumber++; }
                            if (lineCounter % 390 == 0) { startNodeNumber++; endNodeNumber = 0; }
                            lineCounter++;
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
