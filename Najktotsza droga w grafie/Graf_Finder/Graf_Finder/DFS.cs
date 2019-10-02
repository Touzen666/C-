using System;
using System.Collections.Generic;
using System.Linq;

namespace Graf_Finder
{
    class DFS : BaseSearchStrategy
    {
        // metoda "odwiedzająca" wierzchołek
        private List<Node> visitNode(Node node, List<Node> listVisited, int endNode)
        {
            // Inspiracja algorytmem podanym na https://pl.wikipedia.org/wiki/Przeszukiwanie_w_g%C5%82%C4%85b
            List<Node> myNodeList = listVisited.Select(item => item).ToList();
            //Dodaj do listy odwiedzonych wezlow
            myNodeList.Add(node);
            //Sprawdz czy doszlismy do wezla koncowego 
            if (node.number == endNode)
            {//Jesli tak to zwroc droge ktora przyszlismy
                return myNodeList;
            }
            
            node.tag = 1;       // oznacz node jako odwiedzony
            foreach (Node childNode in node.nodes)
            {//Jesli wezel jest jeszcze nie odwiedzony to go odwiedz 
                if (childNode.tag == 0)
                {
                    List<Node> returnList = this.visitNode(childNode, myNodeList, endNode);
                    if (returnList != null)//Jezeli tedy dalo sie dojsc to zwroc ta droge 
                        return returnList;
                }
            }

            return null; //jesli sie nie dalo zwroc null
        }

        public override List<Node> findAWay(Dictionary<int, Node> graph, int startNode, int endNode)
        {
            // Zmienna tag node'ów oznacza, czy wierzchołek został odwiedzony. 0 to nieodwiedzony, 1 to odwiedzony
            return this.visitNode(graph[startNode], new List<Node>(), endNode);
        }
    }
}
