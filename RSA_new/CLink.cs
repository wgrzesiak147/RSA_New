using System.Collections.Generic;

namespace RSA_new {
    public class CLink
    {
        public int Index { get; set; }
        public int NodeA { get; set; }
        public int NodeB { get; set; }
        public int Distance { get; set; }
        public int TakenSlotsCount { get; private set; } = 0;//TODO : CHECK IF ITS NEEDED HERE
        public bool[] TakenSlotsArray = new bool[40];
        public Dictionary<int, List<int>> TakenSlotsArrayForRequest = new Dictionary<int, List<int>>(); //key = requestID  value = list of taken slots indexes

        // /This method takes first found free slot on SlotsArray and it allocates the request



        public bool TryAlocateSlots(int numberOfSLots, int requestId)
        {
            for (int i = 0; i < TakenSlotsArray.Length; i++)
            {
                if (TakenSlotsArray[i] == false && (i + numberOfSLots) < TakenSlotsArray.Length)
                {
                    if (CheckIfAllocationPossible(numberOfSLots, i))
                    {
                        AllocateSlots(numberOfSLots, i, requestId);
                        return true;
                    }
                }
            }
            return false;
        }

        public void AllocateSlots(int numberOfSLots, int index, int requestId) //in this method we are sure that allocation is possible
        {
            for (int j = index; j < index + numberOfSLots; j++)
            {
                TakenSlotsArray[j] = true;
                TakenSlotsCount++;

                if (!TakenSlotsArrayForRequest.ContainsKey(requestId))
                {
                    TakenSlotsArrayForRequest.Add(requestId, new List<int>());
                }
                TakenSlotsArrayForRequest[requestId].Add(j);
            }
        }

        public bool CheckIfAllocationPossible(int numberOfSLots, int index) //checking if allocation is possible for specified number of slots
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

        public CLink(int _index, int _nodeA, int _nodeB, int _distance)
        {
            NodeA = _nodeA;
            NodeB = _nodeB;
            Index = _index;
            Distance = _distance;
        }

        public CLink(CLink b)
        {
            this.Index = b.Index;
            this.NodeA = b.NodeA;
            this.NodeB = b.NodeB;
            this.Distance = b.Distance;
        }
    }
}

