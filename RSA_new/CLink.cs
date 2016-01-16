namespace RSA_new {
    public class CLink {
        public int Index { get; set; }
        public int NodeA { get; set; }
        public int NodeB { get; set; }
        public int Distance { get; set; }
        public CLink(int _index, int _nodeA, int _nodeB, int _distance) {
            NodeA = _nodeA;
            NodeB = _nodeB;
            Index = _index;
            Distance = _distance;
        }

        public CLink(CLink b) {
            this.Index = b.Index;
            this.NodeA = b.NodeA;
            this.NodeB = b.NodeB;
            this.Distance = b.Distance;
        }
    }
}
