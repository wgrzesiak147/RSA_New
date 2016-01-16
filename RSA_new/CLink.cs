namespace RSA_new {
    class CLink {
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
    }
}
