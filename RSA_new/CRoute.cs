using System;
using System.Collections.Generic;
using System.Linq;

namespace RSA_new {
    public class CRoute {
        private List<int> passedNodes;
        private List<CLink> passedLinks;
        public int Index { get; set; } = -1;
        public int NodeBegin { get; set; } = -1;
        public int NodeFinish { get; set; } = -1;
        public int Distance { get; set; } = 0;
        public int TakenSlotsCount { get; set; } = 0;
        public Dictionary<int, int> DemandSlotMapping = null;
        public bool[] TakenSlotsArray = new bool[40];
        public Dictionary<int,int>  TakenSlotsArrayForRequest = new Dictionary<int, int>(); //key = requestID  value = ID's of taken slots
       // /This method takes first found free slot on SlotsArray and it allocates the request
        public bool TryAlocateSlots(int numberOfSLots,int requestId){
            for (int i = 0; i < TakenSlotsArray.Length; i++)
            {
                if (TakenSlotsArray[i] == false && (i + numberOfSLots) < TakenSlotsArray.Length)
                {
                    if (CheckIfAllocationPossible(numberOfSLots, i))
                    {
                        AllocateSlots(numberOfSLots, i, requestId);
                    }
                }
            }
            return false;
        }

        private void AllocateSlots(int numberOfSLots, int i, int requestId) //in this method we are sure that allocation is possible
        {
            for (int j = i; j < numberOfSLots; j++)
            {
               TakenSlotsArray[j] = true ;
               TakenSlotsArrayForRequest.Add(requestId,j);
               j++;
            }
         }

        

        private bool CheckIfAllocationPossible(int numberOfSLots, int index) //checking if allocation is possible for specified number of slots
        {
            for (int j = index; j < numberOfSLots; j++)
            {
                if(TakenSlotsArray[j] == true) //if any of watched sltos is taken (true) then allocation failed and return false
                {
                    return false;
                }
            }
            return true;
        }

        public CRoute(int _routeIndex, int _startNode, int _endNode, List<int> _indexes) {
            Index = _routeIndex;
            NodeBegin = _startNode;
            NodeFinish = _endNode;
            string route = String.Empty;
            for (int i = 0; i < _indexes.Count; i++) {
                route += _indexes[i].ToString() + " ";
            }

            List<CLink> tmpLinkList = new List<CLink>();
            for (int j = 0; j < _indexes.Count; j++) {
                if (CGlobalManager.GlobalLinkList.Count != 0) {
                    for (int i = 0; i < CGlobalManager.GlobalLinkList.Count; i++) {
                        if (CGlobalManager.GlobalLinkList[i].Index == _indexes[j]) {
                            tmpLinkList.Add(CGlobalManager.GlobalLinkList[i]);
                        }
                    }
                }
            }
            if (tmpLinkList.Count == 0) throw new ArgumentException("Route does not contain any links");
            //if (!IsContinuous(tmpLinkList)) throw new ArgumentException("Route " + route + " is not traversable");

            passedLinks = new List<CLink>();
            int tmpNodeA = _startNode;
            for (int i = 0; i < tmpLinkList.Count; i++) {
                if (i == 0 &&
                    IsContinuous(tmpLinkList)) {
                    passedLinks = tmpLinkList;
                    break;
                }
                passedLinks.Add(new CLink(tmpLinkList.Find(x => x.NodeA == tmpNodeA)));
                tmpNodeA = passedLinks[passedLinks.Count - 1].NodeB;
            }
            passedNodes = new List<int>(
                from cLink in passedLinks
                select cLink.NodeA);
            passedNodes.Add(NodeFinish);
            foreach (var link in passedLinks) {
                Distance += link.Distance;
            }
        }

        public List<int> ReturnPassedNodes() { return passedNodes; }
        private bool IsContinuous(List<CLink> _linkList) {
            int tmpNode = _linkList[0].NodeA;
            foreach (var link in _linkList) {
                if (link.NodeA != tmpNode) { return false; }
                tmpNode = link.NodeB;
            }
            return true;
        }
        public void LoadMappings(Dictionary<int, int> _slotMap) {
            if (DemandSlotMapping == null) {
                DemandSlotMapping = _slotMap;
            }
        }
    }
}
