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
           


        private static CRequest GetRequestFromRow(string line,int requestId)
        {
            int startNode = Int32.Parse(line.Substring(0, 3).Trim());
            int endNode = Int32.Parse(line.Substring(2, 3).Trim());
            int capacity = Int32.Parse(line.Substring(5).Trim());
            CRequest result = new CRequest(requestId,startNode, endNode, capacity);
            return result;
        }
        /*
        public static void LoadRequests(string _requestFilePath)
        {
            if (GlobalRequestList.Count == 0)
            {
                CGlobalManager _manager = new CGlobalManager();
                using (StreamReader requestFile = new StreamReader(_requestFilePath))
                {
                    int LineCounter = 0;
                    int RequestQuantity = 0;
                    string line;
                    while((line = requestFile.ReadLine()) != null) {
                        if (line[0] == ' ') { line = line.Remove(0, 1); }
                        for (int i = 0; i < line.Length; i++) {
                            if (i > 0 &&
                                line[i-1] == ' ') {
                                line = line.Remove(i, 1);
                            }
                        }
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
        */

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
                            List<int> _line = line.Split('\t').Select(Int32.Parse).ToList();
                            int _nodeA = lineCounter - 2;
                            List<CLink> linkList = _manager.CalculateLinksFromLine(_nodeA, _line);
                            foreach (var link in linkList) {
                                GlobalLinkList.Add(link);
                            }
                            lineCounter++;
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

        public static void LoadRouteSlotMappings(string _slotMapFilePath) {
            using (StreamReader slotMapFile = new StreamReader(_slotMapFilePath)) {
                string line;
                int lineCounter = 0;
                while ((line = slotMapFile.ReadLine()) != null) {
                    Dictionary<int, int> DemandSlotMap = new Dictionary<int, int>();
                    List<int> _line = line.Split('\t').Select(Int32.Parse).ToList();
                    for (int i = 0; i < _line.Count; i++) {
                        DemandSlotMap.Add((i+1)*10,_line[i]);
                    }
                    GlobalRoutesList.Find(x => x.Index == lineCounter).LoadMappings(DemandSlotMap); // THIS SHOULD DO THE TRICK
                    lineCounter++;
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
        /*
        private List<CRequest> CalculateRequestFromLine(List<int> line)
        {
            List<CRequest> result = new List<CRequest>();
            for (int i = 0; i > line.Count; i++)
            {
                if (line.ElementAt(i) > 0)
                {
                    result.Add(new CRequest(line[i], line[i+1], line[i+2]));
                }
            }
            return result;
        } 
        */
    }
}
