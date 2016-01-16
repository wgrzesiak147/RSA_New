using System.Collections.Generic;

namespace RSA_new {
    public class CNode {
        private static int count = 0;
        // Neighboring nodes
        private List<int> neighborList;
        // <node_index, list_of_available_routes_to_that_node>
        private Dictionary<int, List<CRoute>> availableRoutes;
        public int Index { get; set; } = 0;
        public CNode(List<int> _neighborList, Dictionary<int, List<CRoute>> _routesList) {
            // Unsafe for multithread - TOFIX
            count++;
            Index = count;
            neighborList = new List<int>(_neighborList);
            availableRoutes = new Dictionary<int, List<CRoute>>(_routesList);
        }
        public CNode(Dictionary<int, List<CRoute>> _routesList) {
            // Unsafe for multithread - TOFIX
            count++;
            Index = count;
            availableRoutes = new Dictionary<int, List<CRoute>>(_routesList);

        }
        public List<int> ReturnAllNeigbors() { return neighborList; }
    }
}
