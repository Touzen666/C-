using System;

namespace Graf_Finder
{
    class DFS : BaseSearchStrategy
    {
        //funkcja odwiedzajaca wierzcholek
        private List<Node> visitNode(Node node, List<Node> listVisited, int endNode)
        {
            //skopiuj liste 
            List<Node> myNodeList = listVisited.Select(item => item).ToList();
            //dodaj element do sciezki
            myNodeList.Add(node);
            //jesli to jest ten wierzcholek do ktorego podazamy
            if (node.number == endNode)
            {   
                // to go zwróć
                return myNodeList;
            }
            // oznacz node jako odwiedzony
            node.tag = 1;      
            //Dla kazdego wierzchola ktory jest osiagalny z tego 
            foreach (Node childNode in node.nodes)
            {
                // jeśli wierzchołek nie został odwiedzony
                if (childNode.tag == 0)     
                {
                    //to go odwiedz i zwroc sciezke jezeli da sie znalezc sciezke do konca 
                    List<Node> returnList = this.visitNode(childNode, myNodeList, endNode);
                    //jesli ta sciezka istnieje to ja zwroc 
                    if (returnList != null)
                        return returnList;
                }
            }
            //jesli nie istnieje zwroc null
            return null;
        }

        public override List<Node> findAWay(Dictionary<int, Node> graph, int startNode, int endNode)
        {
            // Zmienna tag node'ów oznacza, czy wierzchołek został odwiedzony. 0 to nieodwiedzony, 1 to odwiedzony
            return this.visitNode(graph[startNode], new List<Node>(), endNode);
        }
    }
}
