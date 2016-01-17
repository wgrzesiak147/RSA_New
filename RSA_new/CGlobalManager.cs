using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RSA_new.Entities;

namespace RSA_new {
    //storing and loading data of network like routes, links, requests
    public class CGlobalManager {
        public static List<CRoute> GlobalRoutesList = new List<CRoute>();
        public static List<CLink> GlobalLinkList = new List<CLink>();
        public static List<CRequest> GlobalRequestList = new List<CRequest>(); 
        public static int LinkCounter = 0;
        public static int RequestCounter = 0;


        //Loading request to GlobalManager
        public static void LoadRequests(string path)
        {
            int counter = 0;
            string line;
                // Read the file and display it line by line.
                using (StreamReader file = new StreamReader(path))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (counter == 0)
                        {
                            int requestQuantity;
                            Int32.TryParse(line, out requestQuantity);
                        }
                        else
                        {
                            CRequest req = GetRequestFromRow(line,counter);
                            GlobalRequestList.Add(req);
                        }
                        counter++;
                    }
                }
            }

        private static CRequest GetRequestFromRow(string line,int requestId){
            int startNode = Int32.Parse(line.Substring(0, 3).Trim());
            int endNode = Int32.Parse(line.Substring(2, 3).Trim());
            int capacity = Int32.Parse(line.Substring(5).Trim());
            CRequest result = new CRequest(requestId,startNode, endNode, capacity);
            return result;
        }

        //loading links to global manager
        public static void LoadLinks(string topologyFilePath){
            if (GlobalLinkList.Count == 0)
            {
                CGlobalManager manager = new CGlobalManager();
                using (StreamReader topologyFile = new StreamReader(topologyFilePath))
                {
                    int nodeQuantity = 0;
                    int linkQuantity = 0;
                    int lineCounter = 0;
                    
                    string line;
                    while ((line = topologyFile.ReadLine()) != null)
                    {
                        if (lineCounter == 0)
                        {
                            Int32.TryParse(line, out nodeQuantity);
                            lineCounter++;
                        }

                        else if (lineCounter == 1)
                        {
                            Int32.TryParse(line, out linkQuantity);
                            lineCounter++;
                        }

                        else
                        {
                            List<int> _line = line.Split('\t').Select(Int32.Parse).ToList();
                            int nodeA = lineCounter - 2;
                            List<CLink> linkList = manager.CalculateLinksFromLine(nodeA, _line);
                            foreach (var link in linkList) {
                                GlobalLinkList.Add(link);
                            }
                            lineCounter++;
                        }
                    }
                }
            }
        }

        //Loading routes to global manager
        public static void LoadRoutes(string routeFilePath) {
            if (GlobalRoutesList.Count == 0) {
                CGlobalManager _manager = new CGlobalManager();
                using (StreamReader routeFile = new StreamReader(routeFilePath)) {
                    int routesQuantity = 0; //propably useless information
                    int lineCounter = 0;
                    int startNodeNumber = 0;
                    int endNodeNumber = 1;
                    string line;
                    while ((line = routeFile.ReadLine()) != null) {
                        if (lineCounter == 0) { Int32.TryParse(line, out routesQuantity); lineCounter++; }
                        else {
                            List<int> list = line.Split(' ').Select(Int32.Parse).ToList();
                            List<int> linkIndexes = _manager.CalculateRouteFromBinary(list);
                            if (startNodeNumber == endNodeNumber) { endNodeNumber++; }
                            GlobalRoutesList.Add(new CRoute(lineCounter - 1, startNodeNumber, endNodeNumber, linkIndexes));
                            if (lineCounter % 30 == 0) { endNodeNumber++; }
                            if (lineCounter % 390 == 0) { startNodeNumber++; endNodeNumber = 0; }
                            lineCounter++;
                        }
                    }
                }
            }
        }

        public static void LoadRouteSlotMappings(string slotMapFilePath) {
            using (StreamReader slotMapFile = new StreamReader(slotMapFilePath)) {
                string line;
                int lineCounter = 0;
                while ((line = slotMapFile.ReadLine()) != null) {
                    Dictionary<int, int> demandSlotMap = new Dictionary<int, int>();
                    List<int> list = line.Split('\t').Select(Int32.Parse).ToList();
                    for (int i = 0; i < list.Count; i++) {
                        demandSlotMap.Add((i+1)*10,list[i]);
                    }
                    GlobalRoutesList.Find(x => x.Index == lineCounter).LoadMappings(demandSlotMap); // THIS SHOULD DO THE TRICK
                    lineCounter++;
                }
            }
        }

        private List<int> CalculateRouteFromBinary(List<int> line) {
            List<int> result = new List<int>();
            int counter = 0;
            foreach (var element in line) {
                if (element == 1) {    //in case of route creation: if element == 1 it means that this link is used in this route
                    result.Add(counter); //adding indexes of existing nodes
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
                    result.Add(new CLink(LinkCounter, nodeAIndex, i, line[i]));
                    LinkCounter++;
                }
            }
            return result;     
        } 
    }
}
