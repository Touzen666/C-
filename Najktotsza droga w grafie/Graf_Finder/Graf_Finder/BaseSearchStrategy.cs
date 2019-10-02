using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf_Finder
{
    //Bazowa strategia przeszukiwania grafu
    abstract class BaseSearchStrategy
    {
        // Abstrakcyjna funkcja znajdująca drogę. Zwraca null, jeżeli droga nie istnieje.
        public abstract List<Node> findAWay(Dictionary<int, Node> graph, int startNode, int endNode);
    }




}
