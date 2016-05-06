using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irony.Ast;
using Irony.Parsing;

namespace Practica2_Compi1_Web_2016.Analisadores
{
    public class GraficaAST
    {
        private static int contador;
        private static String grafo;

        public static string grafico(ParseTreeNode raiz) 
        {
            grafo = "digraph G{";
            grafo += "nodo0[label=\"" + eliminacion(raiz.ToString()) + "\"];" + "";
            contador = 1;
            ReccorerArbol("nodo0",raiz);
            grafo += "}";
            return grafo;
        }

        private static void ReccorerArbol(string p, ParseTreeNode hijos)
        {
            foreach(ParseTreeNode hijo in hijos.ChildNodes)
            {
                String nHijo = "nodo" + contador.ToString();
                grafo += nHijo + "[label = \""+eliminacion(hijo.ToString())+"\"];\n";
                grafo += p + "->" + nHijo + ";\n";
                contador++;
                ReccorerArbol(nHijo,hijo);
            }
        }


        private static String eliminacion(String cadena)
        {
            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");
            cadena = cadena.Replace("(Key symbol)","");
            cadena = cadena.Replace("(Keyword)", "");
            return cadena;
        }

    }
}