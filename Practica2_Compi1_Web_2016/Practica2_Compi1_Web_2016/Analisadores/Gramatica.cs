using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irony.Ast;
using Irony.Parsing;

namespace Practica2_Compi1_Web_2016.Analisadores
{
    public class Gramatica : Grammar
    {
        public Gramatica() : base  (caseSensitive : true)
        {

            #region Expreciones Regulares
            NumberLiteral numero_entero = new NumberLiteral("numero_entero");
            //RegexBasedTerminal numero_entero = new RegexBasedTerminal("numero_entero", "[0-9]+");
            RegexBasedTerminal numero_decimal = new RegexBasedTerminal("numero_decimal", "[0-9]+.[0-9]+");
            RegexBasedTerminal caracter = new RegexBasedTerminal("caracter", "'[a-zA-Z]'");
            StringLiteral cadena = new StringLiteral("cadena", "\"");
            IdentifierTerminal identificador = new IdentifierTerminal("ID");
            CommentTerminal comentarioLinea = new CommentTerminal("ComentarioLinea", "//", "\n");
            CommentTerminal comentarioMultiLinea = new CommentTerminal("ComentarioMultiLinea", "/*", "*/");
            base.NonGrammarTerminals.Add(comentarioLinea);
            base.NonGrammarTerminals.Add(comentarioMultiLinea);
            #endregion

            #region Terminales
            var mas = ToTerm("+", "mas");
            var menos = ToTerm("-", "menos");
            var por = ToTerm("*", "multiplicacion");
            var div = ToTerm("/", "divicion");
            var poencia = ToTerm("^", "potencia");
            var tkint = ToTerm("int", "int");
            var raiz = ToTerm("raiz", "raiz");
            var graficar = ToTerm("graficar", "graficar");
            var tkdouble = ToTerm("double", "double");
            var tkbool = ToTerm("bool", "bool");
            var tkstring = ToTerm("string", "string");
            var tkchar = ToTerm("char", "char");
            var tkvoid = ToTerm("void", "void");
            var retornar = ToTerm("retornar", "retornar");
            var principal = ToTerm("principal", "principal");
            var SI = ToTerm("SI", "SI");
            var SINO_SI = ToTerm("SINO_SI", "SINO_SI");
            var SINO = ToTerm("SINO", "SINO");
            var INTERRUPTOR = ToTerm("INTERRUPTOR", "INTERRUPTOR");
            var CASO = ToTerm("CASO", "CASO");
            var DEFECTO = ToTerm("DEFECTO", "DEFECTO");
            var MIENTRAS = ToTerm("MIENTRAS", "MIENTRAS");
            var HACER = ToTerm("HACER", "HACER");
            var SALIR = ToTerm("salir", "salir");
            var tktrue = ToTerm("true", "true");
            var tkfalse = ToTerm("false", "false");
            var imprimir = ToTerm("imprimir", "imprimir");
            var programa = ToTerm("programa", "programa");
            var tkvar = ToTerm("var", "var");
            var aumento = ToTerm("++", "++");
            var disminucion = ToTerm("--", "--");
            var mayorigual = ToTerm(">=", ">=");
            var menorigual = ToTerm("<=", "<=");
            var mayor = ToTerm(">", ">");
            var menor = ToTerm("<", "<");
            var igual = ToTerm("==", "==");
            var diferente = ToTerm("!=", "!=");
            var parentesisabre = ToTerm("(", "(");
            var parentisiscierra = ToTerm(")", ")");
            var corcheteabre = ToTerm("[", "[");
            var corchetecierra = ToTerm("]", "]");
            var coma = ToTerm(",", "coma");
            var dospuntos = ToTerm(":", ":");
            var asignacion = ToTerm("::=", "asignacion");
            var puntoycoma = ToTerm(";", "puntoycoma");
            var or = ToTerm("OR", "or");
            var and = ToTerm("AND", "and");
            var not = ToTerm("NOT", "not");

            #endregion

            #region no Terminales
            NonTerminal Inicio = new NonTerminal("Inicio");
            NonTerminal Pcont = new NonTerminal("Pcont");
            NonTerminal Pcontp = new NonTerminal("Pcont'");
            NonTerminal Dvariables = new NonTerminal("Dvariables");
            NonTerminal Funciones = new NonTerminal("Funciones");
            NonTerminal Principal = new NonTerminal("Principal");
            NonTerminal Sent = new NonTerminal("Sent");
            NonTerminal Tipo = new NonTerminal("Tipo");
            NonTerminal Parametros = new NonTerminal("Parametros");
            NonTerminal Ids = new NonTerminal("Ids");
            NonTerminal E = new NonTerminal("E");
            NonTerminal Log = new NonTerminal("Log");
            NonTerminal ParValor = new NonTerminal("ParValor");
            NonTerminal Ex = new NonTerminal("Ex");
            NonTerminal ParValorp = new NonTerminal("ParValor'");
            NonTerminal Sentp = new NonTerminal("Sent'");
            NonTerminal NTvar = new NonTerminal("Var");
            NonTerminal IF = new NonTerminal("IF");
            NonTerminal Interrup = new NonTerminal("Interrup");
            NonTerminal Mien = new NonTerminal("Mien");
            NonTerminal Hace = new NonTerminal("Hace");
            NonTerminal Sent2 = new NonTerminal("Sent2");
            NonTerminal Mulsinosi = new NonTerminal("Mulsinosi");
            NonTerminal Sinosi = new NonTerminal("Sinosi");
            NonTerminal Casos = new NonTerminal("Casos");
            NonTerminal Caso = new NonTerminal("Caso");
            NonTerminal PorCaso = new NonTerminal("PorCaso");
            NonTerminal Sent2p = new NonTerminal("Sent2'");
            #endregion

            #region Gramatica
            //Inicio de la Gramatica 
            Inicio.Rule = programa + identificador + corcheteabre + Pcontp + corchetecierra;
            //Creamos Lista de Contenido de Programa
            Pcontp.Rule = Pcontp + Pcont
                | Pcont;
            //Aqui ponemos todo lo que puede benir
            Pcont.Rule = Dvariables
                | Funciones
                | Principal;
            //Aqui definimos el Metodo Principal
            Principal.Rule = principal + parentesisabre + parentisiscierra + corcheteabre + Sent + corchetecierra
                | principal + parentesisabre + parentisiscierra + corcheteabre + corchetecierra;
            //Aqui definimos las Funciones y Metodos
            Funciones.Rule = Tipo + identificador + parentesisabre + parentisiscierra + corcheteabre + Sent + corchetecierra
                | Tipo + identificador + parentesisabre + parentisiscierra + corcheteabre + corchetecierra
                | Tipo + identificador + parentesisabre + Parametros + parentisiscierra + corcheteabre + Sent + corchetecierra
                | Tipo + identificador + parentesisabre + Parametros + parentisiscierra + corcheteabre + corchetecierra;
            //Aqui se definen los tipos de Funciones
            Tipo.Rule = tkint
                | tkdouble
                | tkstring
                | tkchar
                | tkbool
                | tkvoid;
            //Aqui se definen los parametos que pueden llevar las funciones 
            Parametros.Rule = Parametros + coma + tkvar + identificador
                | tkvar + identificador;
            //Para la Declacion de Variables
            Dvariables.Rule = tkvar + Ids + puntoycoma
                | tkvar + identificador + asignacion + E + puntoycoma
                | tkvar + identificador + asignacion + Log + puntoycoma
                | identificador + asignacion + E + puntoycoma
                | identificador + asignacion + Log + puntoycoma;
            
            //multiple id...}
            Ids.Rule = Ids + coma + identificador
                | identificador;
            //Se define E
            E.Rule = E + mas + E
                | E + menos + E
                | E + por + E
                | E + div + E
                | numero_entero
                | cadena
                | identificador
                | identificador + parentesisabre + parentisiscierra
                | identificador + parentesisabre + ParValor + parentisiscierra
                | caracter
                | parentesisabre + E + parentisiscierra
                | raiz + parentesisabre + E + coma + E + parentisiscierra;
            //Aqui definimos los valores que pueden ir dentro de una funcion
            ParValor.Rule = ParValor + coma + ParValorp
                | ParValorp;
            //Aqui definmos Todos los parametos que puede benir
            ParValorp.Rule = numero_entero
                | cadena
                | tkchar
                | E
                | identificador
                | identificador + parentesisabre + parentisiscierra
                | identificador + parentesisabre + ParValor + parentisiscierra;
            //Aqui definomos las Expreciones Logiacas
            Log.Rule = Log + or + Log
                | Log + and + Log
                | not + Log
                | Ex
                | tkfalse
                | tktrue
                | parentesisabre + Log + parentisiscierra;
            //Aqui defimos las formas logcias 
            Ex.Rule = E + menor + E
                | E + mayor + E
                | E + menorigual + E
                | E + mayorigual + E
                | E + igual + E
                | E + diferente + E
                | parentesisabre + Ex + parentisiscierra
                |E;
            //Aqui va la parte de las multiples sentencias que pueden ir dentro de un metodo
            Sent.Rule = Sent + Sentp
                | Sentp;
                //| Empty;
            //Lo que pudebe bernir en las Sentencias
            Sentp.Rule = Dvariables
                | imprimir + parentesisabre + E + parentisiscierra + puntoycoma
                | graficar + parentesisabre + cadena + coma + cadena + coma + NTvar + coma + NTvar + coma + cadena + parentisiscierra + puntoycoma
                | IF
                | Interrup
                | Mien
                | Hace
                | identificador + parentesisabre + parentisiscierra + puntoycoma
                | identificador + parentesisabre + ParValor + parentisiscierra + puntoycoma;
            //Definicon de NTvar
            NTvar.Rule = numero_entero
               // | numero_decimal
                | menos + NTvar;
            IF.Rule = SI + parentesisabre + Log + parentisiscierra + corcheteabre + Sent2 + corchetecierra
                | SI + parentesisabre + Log + parentisiscierra + corcheteabre + Sent2 + corchetecierra + SINO + corcheteabre + Sent2 + corchetecierra
                | SI + parentesisabre + Log + parentisiscierra + corcheteabre + Sent2 + corchetecierra + Mulsinosi
                | SI + parentesisabre + Log + parentisiscierra + corcheteabre + Sent2 + corchetecierra + Mulsinosi + SINO + corcheteabre + Sent2 + corchetecierra;
            //Para los Mulpiples si no
            Mulsinosi.Rule = Mulsinosi + Sinosi
                | Sinosi;
            //Definicon se Sinosi
            Sinosi.Rule = SINO_SI + parentesisabre + Log + parentisiscierra + corcheteabre + Sent2 + corchetecierra;
            //Aqui definimos el Interrup
            Interrup.Rule = INTERRUPTOR + parentesisabre + E + parentisiscierra + corcheteabre + Casos + corchetecierra;
            //Aqui definimos los Casos 
            Casos.Rule = Casos + Caso
                | Caso
                | Empty;
            //Aqui definimos lo que es un Caso
            Caso.Rule = CASO + PorCaso + dospuntos + Sent2
                | DEFECTO + dospuntos + Sent2;
            //Aqui definimos esto del PorCaso
            PorCaso.Rule = cadena
                | numero_entero
                | caracter
                | tktrue
                | tkfalse;
            //Aqui definomos Mien
            Mien.Rule = MIENTRAS + parentesisabre + Log + parentisiscierra + corcheteabre + Sent2 + corchetecierra;
            //Aqui hacemos lo Hace
            Hace.Rule = HACER + corcheteabre + Sent2 + corchetecierra + MIENTRAS + parentesisabre + Log + parentisiscierra + puntoycoma;
            //Aqui definomos las Listas de Sentencis que puede ir en las estructuras
            Sent2.Rule = Sent2 + Sent2p
                | Sent2p
                | Empty;
            //Aqui definimos las Sentencias 
            Sent2p.Rule = Dvariables
                | imprimir + parentesisabre + E + parentisiscierra + puntoycoma
                | graficar + parentesisabre + cadena + coma + cadena + coma + NTvar + coma + NTvar + coma + cadena + parentisiscierra + puntoycoma
                | IF
                | Interrup
                | Mien
                | Hace
                | identificador + parentesisabre + parentisiscierra + puntoycoma
                | identificador + parentesisabre + ParValor + parentisiscierra + puntoycoma;
            
            #endregion

            #region preferencias
            this.Root = Inicio;
            RegisterOperators(1, Associativity.Left, mas, menos);
            RegisterOperators(2, Associativity.Left, por, div);
            RegisterOperators(3, Associativity.Left, or, and);
            RegisterOperators(4, Associativity.Right, not);
            #endregion

        }
    }
}