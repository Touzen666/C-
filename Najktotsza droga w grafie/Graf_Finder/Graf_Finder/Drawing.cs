using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graf_Finder
{
    class Drawing
    {
        public static Bitmap draw(Dictionary<int, Node> graph, List<Node> way)
        {//Tworzy obiekt typu bit Map (1024x768)
            Bitmap bmp = new Bitmap(1024, 768);
            //jest to obiekt typu grafika i on nam pozawala rysowac po tym wszystkim 
            Graphics grf = Graphics.FromImage(bmp);
            //Definiuje dwa rodzaje pedzla 
            Brush circleBrush = new SolidBrush(Color.Magenta);
            Brush textBrush = new SolidBrush(Color.Black);
            foreach (Node node in graph.Values)
            {//dla kazdego wierzcholka grafu narysuj kolko o wymiarach 50x50
                grf.FillEllipse(circleBrush, node.x * 10, node.y * 10, 50, 50);
            }

            //Stworz nowe pedzle 
            Brush wayBrush = new SolidBrush(Color.Red);
            Brush vertexBrush = new SolidBrush(Color.Green);
            //stworz nowe piora
            Pen wayPen = new Pen(wayBrush, 3);
            Pen vertexPen = new Pen(vertexBrush, 3);

            // zaprojektuj i dodaj strzalke do pior
            GraphicsPath capPath = new GraphicsPath();
            capPath.AddLine(-5, 0, 5, 0);
            capPath.AddLine(-5, 0, 0, 5);
            capPath.AddLine(0, 5, 5, 0);
            //Dodanie sciezki jako zakonczenia do pior
            wayPen.CustomEndCap = new System.Drawing.Drawing2D.CustomLineCap(null, capPath);
            vertexPen.CustomEndCap = new System.Drawing.Drawing2D.CustomLineCap(null, capPath);

            //Dla kazdego wezla 
            foreach (Node node in graph.Values)
            {
                // Dla kazdego wezla do ktorego jest przjscie z bierzacego wezla 
                foreach (Node childNode in node.nodes)
                {
                    // Narysuj linie ze strzalka na koncu
                    grf.DrawLine(vertexPen, new Point(node.x * 10+25, node.y * 10+25), new Point(childNode.x * 10+25, childNode.y * 10+25));
                }
            }

            // narysuj droge
            for (int i=0; i<way.Count-1; i++)
            {
                Node first = way[i];
                Node second = way[i + 1];
                //wypisanie znalezionej sciezki w konsoli
                Console.WriteLine(first.number.ToString() + " to " + second.number.ToString());
                //narysuj znaleziona sciezke 
                grf.DrawLine(wayPen, new Point(first.x * 10 + 25, first.y * 10 + 25), new Point(second.x * 10 + 25, second.y * 10 + 25));
            }

            // narysuj tekst(liczby)
            foreach (Node node in graph.Values)
            {
                grf.DrawString(node.number.ToString(), new Font("Tahoma", 20), textBrush, node.x * 10 + 12, node.y * 10 + 62);
            }
            //zwroc bitmape obiektu
            return bmp;
        }
    }
}
