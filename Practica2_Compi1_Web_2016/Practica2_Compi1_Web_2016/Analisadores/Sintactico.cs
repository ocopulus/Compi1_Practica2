using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irony.Ast;
using Irony.Parsing;
using System.IO;
using System.Diagnostics;

namespace Practica2_Compi1_Web_2016.Analisadores
{
    public class Sintactico : Grammar
    {
        public static string hola;
        
        public static ParseTree analizar(String cadena)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;
            return arbol;
        }

        public static void arboll(ParseTreeNode raiz) 
        {
            String arbol = GraficaAST.grafico(raiz);
            StreamWriter file = new StreamWriter("C:\\sub\\grafico.dot");
            file.WriteLine(arbol);
            file.Close();
            ProcessStartInfo startInfo = new ProcessStartInfo("C:/Program Files (x86)/Graphviz 2.28/bin/dot.exe");
            startInfo.Arguments = "dot -Tpng \"C:/sub/grafico.dot\" -o \"C:/sub/graf.png\"";
            Process.Start(startInfo);
        }
    }
}