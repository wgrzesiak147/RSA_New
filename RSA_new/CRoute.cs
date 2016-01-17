using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSA_new {
    public class CRoute {
        private readonly List<int> _passedNodes;
        private readonly List<CLink> _passedLinks;
        public int Index { get; set; } = -1;
        public int NodeBegin { get; set; } = -1;
        public int NodeFinish { get; set; } = -1;
        public int Distance { get; set; } = 0;
        public int TakenSlotsCount { get; private set; } = 0;
        public Dictionary<int, int> DemandSlotMapping = null;
        public bool[] TakenSlotsArray = new bool[40];
        public Dictionary<int,List<int>>  TakenSlotsArrayForRequest = new Dictionary<int, List<int>>(); //key = requestID  value = list of taken slots indexes

        public CRoute(CRoute source)
        {
            _passedNodes = source._passedNodes;
            _passedLinks = source._passedLinks;
            Index = source.Index;
            NodeBegin = source.NodeBegin;
            NodeFinish = source.NodeFinish;
            Distance = source.Distance;
            TakenSlotsCount = source.TakenSlotsCount;
            DemandSlotMapping = source.DemandSlotMapping;
            TakenSlotsArray = source.TakenSlotsArray;
            TakenSlotsArrayForRequest = new Dictionary<int, List<int>>(); 
            foreach (var requestSlots in source.TakenSlotsArrayForRequest)
            {
                List<int> slots = requestSlots.Value.ToList();
                TakenSlotsArrayForRequest.Add(requestSlots.Key,slots);
            }
        }
       
        // /This method takes first found free slot on the links SlotsArrays and it allocates the request (int the links and the routes)
        public bool TryAlocateSlots(int numberOfSLots,int requestId)
        {
            bool result = false;
            int index = -1;
            while (!result)
            {
                index++;
                if (index >= TakenSlotsArray.Length) return false;
                foreach (var link in _passedLinks)
                {
                    CLink currLink = CGlobalManager.GlobalLinkList.FirstOrDefault(x => x.Index == link.Index);
                    if (currLink != null)
                    {
                        result = currLink.CheckIfAllocationPossible(numberOfSLots, index);
                        if (result == false)
                        {
                            break;
                        }
                    }
                }
            }
            foreach (var link in _passedLinks)
            {
                CLink currLink = CGlobalManager.GlobalLinkList.FirstOrDefault(x => x.Index == link.Index);
                if (currLink != null)
                {
                    currLink.AllocateSlots(numberOfSLots, index,requestId);
                }
            }

            if (CheckIfAllocationPossible(numberOfSLots,index))
            {
            AllocateSlots(numberOfSLots, index, requestId);
            return true;
            }
            return false;

        }

        private void AllocateSlots(int numberOfSLots, int index, int requestId) //in this method we are sure that allocation is possible
        {
            for (int j = index; j < index + numberOfSLots; j++)
            {
               TakenSlotsArray[j] = true ;
                TakenSlotsCount++;

               if (!TakenSlotsArrayForRequest.ContainsKey(requestId))
                {
                    TakenSlotsArrayForRequest.Add(requestId, new List<int>());
                }
               TakenSlotsArrayForRequest[requestId].Add(j);
            }
         }

        

        private bool CheckIfAllocationPossible(int numberOfSLots, int index) //checking if allocation is possible for specified number of slots
        {
            for (int j = index; j < index + numberOfSLots; j++)
            {
                if (j >= TakenSlotsArray.Length) return false;
                if (TakenSlotsArray[j] == true) //if any of watched sltos is taken (true) then allocation failed and return false
                {
                    return false;
                }
            }
            return true;
        }

        public CRoute(int routeIndex, int startNode, int endNode, List<int> indexes) {
            Index = routeIndex;
            NodeBegin = startNode;
            NodeFinish = endNode;
            string route = String.Empty;
            for (int i = 0; i < indexes.Count; i++) {
                route += indexes[i].ToString() + " ";
            }

            List<CLink> tmpLinkList = new List<CLink>();
            for (int j = 0; j < indexes.Count; j++) {
                if (CGlobalManager.GlobalLinkList.Count != 0) {
                    for (int i = 0; i < CGlobalManager.GlobalLinkList.Count; i++) {
                        if (CGlobalManager.GlobalLinkList[i].Index == j) {
                            tmpLinkList.Add(CGlobalManager.GlobalLinkList[i]);
                        }
                    }
                }
            }
            if (tmpLinkList.Count == 0) throw new ArgumentException("Route does not contain any links");

            _passedLinks = new List<CLink>();
            int tmpNodeA = startNode;
            for (int i = 0; i < tmpLinkList.Count; i++) {
                if (i == 0 &&
                    IsContinuous(tmpLinkList)) {
                    _passedLinks = tmpLinkList;
                    break;
                }
                _passedLinks.Add(new CLink(tmpLinkList.Find(x => x.NodeA == tmpNodeA)));
                tmpNodeA = _passedLinks[_passedLinks.Count - 1].NodeB;
            }
            _passedNodes = new List<int>(
                from cLink in _passedLinks
                select cLink.NodeA);
            _passedNodes.Add(NodeFinish);
            foreach (var link in _passedLinks) {
                Distance += link.Distance;
            }
        }



        public List<int> ReturnPassedNodes() { return _passedNodes; }


        private bool IsContinuous(List<CLink> linkList) {
            int tmpNode = linkList[0].NodeA;
            foreach (var link in linkList) {
                if (link.NodeA != tmpNode) { return false; }
                tmpNode = link.NodeB;
            }
            return true;
        }


        public void LoadMappings(Dictionary<int, int> slotMap) {
            if (DemandSlotMapping == null) {
                DemandSlotMapping = slotMap;
            }
        }

        //it free the slots and delete the entry about request from current route
        public void FreeSlots(KeyValuePair<int, List<int>> requestAndSlots){
            List<int> slotsToFree = requestAndSlots.Value;
            foreach (var slot in slotsToFree)
            {
                TakenSlotsArray[slot] = false; //free slots
                TakenSlotsCount--;
            }
            TakenSlotsArrayForRequest.Remove(requestAndSlots.Key); //removing request from dictionary
        }
    }
}
