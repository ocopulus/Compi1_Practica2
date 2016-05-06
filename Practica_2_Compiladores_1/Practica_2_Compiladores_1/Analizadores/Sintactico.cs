using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace Practica_2_Compiladores_1.Analizadores
{
    class Sintactico : Grammar
    {
        public static ParseTreeNode analizar(String cadena) 
        {
            Gramatica garamtacia = new Gramatica();
            LanguageData lenguaje = new LanguageData(garamtacia);
            Parser parse = new Parser(lenguaje);
            ParseTree arbol = parse.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;
            return raiz;
        }
    }
}
