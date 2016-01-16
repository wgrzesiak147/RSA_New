using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RSA_new.Entities;

namespace RSA_new {
    public class CGlobalManager {
        public static List<CRoute> GlobalRoutesList = new List<CRoute>();
        public static List<CLink> GlobalLinkList = new List<CLink>();
        public static List<CRequest> GlobalRequestList = new List<CRequest>(); 
        public static int linkCounter = 0;
        public static int requestCounter = 0;

        public static void LoadRequest(string _requestFilePath)
        {
            if (GlobalRequestList.Count == 0)
            {
                CGlobalManager _manager = new CGlobalManager();
                using (StreamReader requestFile = new StreamReader(_requestFilePath))
                {
                    int LineCounter = 0;
                    int RequestQuantity = 0;
                    string line;
                    while((line = requestFile.ReadLine()) != null)
                    {
                        if (LineCounter == 0)
                        {
                            Int32.TryParse(line, out RequestQuantity);
                            LineCounter++;
                        }
                        else
                        {
                            List<int> _line = line.Split(' ').Select(Int32.Parse).ToList();
                            List<CRequest> RequestList = _manager.CalculateRequestFromLine(_line);
                            foreach (var request in RequestList)
                            {
                                GlobalRequestList.Add(request);
                            }
                        }
                    }
                }
            }
        }

        public static void LoadLinks(string _topologyFilePath)
        {
            if (GlobalLinkList.Count == 0)
            {
                CGlobalManager _manager = new CGlobalManager();
                using (StreamReader topologyFile = new StreamReader(_topologyFilePath))
                {
                    int NodeQuantity = 0;
                    int LinkQuantity = 0;
                    int lineCounter = 0;
                    
                    string line;
                    while ((line = topologyFile.ReadLine()) != null)
                    {
                        if (lineCounter == 0)
                        {
                            Int32.TryParse(line, out NodeQuantity);
                            lineCounter++;
                        }

                        else if (lineCounter == 1)
                        {
                            Int32.TryParse(line, out LinkQuantity);
                            lineCounter++;
                        }

                        else
                        {
                            List<int> _line = line.Split(' ').Select(Int32.Parse).ToList();
                            List<CLink> linkList = _manager.CalculateLinksFromLine(lineCounter - 2,_line);
                            foreach (var link in linkList)
                            {
                                GlobalLinkList.Add(link);
                            }
                        }
                    }
                }
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

        private List<CLink> CalculateLinksFromLine(int nodeAIndex, List<int> line) {
            List<CLink> result = new List<CLink>();
            for (int i = 0; i < line.Count; i++)
            {
                if (line[i] > 0)
                {
                    result.Add(new CLink(linkCounter, nodeAIndex, i, line[i]));
                    linkCounter++;
                }
            }
            return result;     
        }

        private List<CRequest> CalculateRequestFromLine(List<int> line)
        {
            List<CRequest> result = new List<CRequest>();
            for (int i = 0; i > line.Count; i++)
            {
                if (line[i] > 0)
                {
                    result.Add(new CRequest(line[i], line[i+1], line[i+2]));
                }
            }
            return result;
        } 
    }
}
