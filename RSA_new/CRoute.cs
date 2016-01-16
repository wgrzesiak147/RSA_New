using System;
using System.Collections.Generic;
using System.Linq;

namespace RSA_new {
    class CRoute {
        private List<int> passedNodes;
        private List<CLink> passedLinks;
        public int NodeBegin { get; set; } = -1;
        public int NodeFinish { get; set; } = -1;
        public int Distance { get; set; } = 0;
        public int TakenSlotsCount { get; set; } = 0;
        public CRoute(List<CLink> _linkList) {
            if (_linkList.Count == 0) throw new ArgumentException("Route does not contain any links");
            if (!IsContinuous(_linkList)) throw new ArgumentException("Route is not traversable");
            passedLinks = new List<CLink>(_linkList);
            NodeBegin = passedLinks[0].NodeA;
            NodeFinish = passedLinks[passedLinks.Count - 1].NodeB;
            passedNodes = new List<int>(
                from cLink in passedLinks
                select cLink.NodeA);
            passedNodes.Add(NodeFinish);
            foreach (var link in passedLinks) {
                Distance += link.Distance;
            }
        }
        public CRoute(List<int> _indexes) {
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
            if (!IsContinuous(tmpLinkList)) throw new ArgumentException("Route is not traversable");
            passedLinks = new List<CLink>(tmpLinkList);
            NodeBegin = passedLinks[0].NodeA;
            NodeFinish = passedLinks[passedLinks.Count - 1].NodeB;
            passedNodes = new List<int>(
                from cLink in passedLinks
                select cLink.NodeA);
            passedNodes.Add(NodeFinish);
            foreach (var link in passedLinks) {
                Distance += link.Distance;
            }
        }
        public CRoute(CLink _link) {
            passedLinks = new List<CLink>();
            passedLinks.Add(_link);
            NodeBegin = _link.NodeA;
            NodeFinish = _link.NodeB;
            passedNodes = new List<int>();
            passedNodes.Add(NodeBegin);
            passedNodes.Add(NodeFinish);
            Distance += _link.Distance;
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
    }
}
