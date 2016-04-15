using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace Practica_2_Compiladores_1.Analizadores
{
    class Gramatica : Grammar
    {
        public Gramatica () : base(caseSensitive : false)
        {

            #region Expreciones Regulares
            RegexBasedTerminal numero = new RegexBasedTerminal("numero","[0-9]+");
            #endregion

            #region Terminales
            var mas = ToTerm("+");
            var menos = ToTerm("-");
            var por = ToTerm("*");
            var dir = ToTerm("/");
            #endregion

            #region no Terminales
            #endregion

            #region Gramatica
            #endregion

            #region preferencias
            #endregion

        }
    }
}
