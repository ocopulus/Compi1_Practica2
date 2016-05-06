using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irony.Ast;
using Irony.Parsing;

namespace Practica2_Compi1_Web_2016.Analisadores
{
    public class recolectar
    {
        public static String resultado;
        private static String ambito="gobal";
        private static String metodo;
        private static String Expre = null;
        private static int bo = 0;
        private static ArrayList parametos = new ArrayList();
        public static ArrayList lisVariables = new ArrayList();
        public static ArrayList lisFunciones = new ArrayList();
        private static ParseTreeNode jjPrincipal = null;

        public static String iniciar(ParseTreeNode raiz)
        {
            String valor;
            valor = recolecta(raiz);
            valor += "\n-------------------------------------";
            valor += acciones(jjPrincipal);
            return valor;
        }
        public static String recolecta(ParseTreeNode raiz) 
        {
            String result = null;
            switch (raiz.Term.Name.ToString())
            {
                case "Inicio" :
                    {
                        if(raiz.ChildNodes.Count==5)
                        {
                            result += recolecta(raiz.ChildNodes[3]);
                        }
                        break;
                    }
                case "Pcont'":
                    {
                        if(raiz.ChildNodes.Count==2)
                        {
                            result += recolecta(raiz.ChildNodes[0]);
                            result += recolecta(raiz.ChildNodes[1]);
                        }
                        else if (raiz.ChildNodes.Count==1)
                        {
                            result += recolecta(raiz.ChildNodes[0]);
                        }
                        break;
                    }
                case "Pcont" :
                    {
                        result += recolecta(raiz.ChildNodes[0]);
                        break;
                    }
                case "Principal":
                    {
                        #region Super Sent
                        if (raiz.ChildNodes.Count==6)
                        {
                            jjPrincipal = raiz.ChildNodes[4];
                            //ambito = "principal";
                            //result += "\n" + recolecta(raiz.ChildNodes[4]);
                            ambito = "gobal";
                            result += "\n Se guardo las sentencias del main";
                        }
                        else if (raiz.ChildNodes.Count==5)
                        {
                            result += "\n Sen encontro la FUNCION Princpal pero esta Vacio ";
                        }
                        #endregion
                        break;
                    }
                case "Funciones":
                    {
                        #region Super Sent
                        String tipo = recolecta(raiz.ChildNodes[0]);
                        string nombre = raiz.ChildNodes[1].Token.Value.ToString();
                        if(raiz.ChildNodes.Count==6)
                        {
                            #region sent
                            Fun f = new Fun(nombre,tipo,null,null);
                            lisFunciones.Add(f);
                            result += "\n Ser registro funcion "+nombre;
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==7)
                        {
                            #region Sent
                            if(raiz.ChildNodes[3].Term.Name.ToString()==")")
                            {
                                Fun f = new Fun(nombre, tipo, null, raiz.ChildNodes[5]);
                                ambito = nombre;
                                result += "\n"+ recolecta(raiz.ChildNodes[5]);
                                lisFunciones.Add(f);
                                ambito = "gobal";
                                result += "\n Ser registro la funcion " + nombre;
                            }
                            else if (raiz.ChildNodes[3].Term.Name.ToString() == "Parametros")
                            {

                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==8)
                        {
                            #region Sent

                            #endregion
                        }
                        #endregion
                        break;
                    }
                case "Tipo":
                    {
                        #region Super Sent
                        if(raiz.ChildNodes.Count==1)
                        {
                            result = raiz.ChildNodes[0].Token.Value.ToString();
                        }
                        #endregion
                        break;
                    }
                case "Dvariables":
                    {
                        #region Super Sent
                        if (raiz.ChildNodes.Count==5)
                        {
                            #region sent
                            //Aqui biene una delacion de una variale y se le asigna varlor de una ves
                            String valor = recolecta(raiz.ChildNodes[3]);
                            String id = raiz.ChildNodes[1].Token.Value.ToString();
                            String Tipo=null;
                            if (Char.IsDigit(valor, 0))
                            {
                                Tipo = "int";
                            }
                            else if (Char.IsLetter(valor, 0))
                            {
                                if (valor == "true" || valor == "false")
                                {
                                    Tipo = "bool";
                                }
                                else if (valor.Length != 1)
                                {
                                    Tipo = "String";
                                }
                                else
                                {
                                    Tipo = "Char";
                                }
                            }
                            variables va = new variables();
                            va.Ambito1 = ambito;
                            va.Nombre = id;
                            va.Tipo = Tipo;
                            va.Valor = valor;
                            result += "\nSe creo : " + id + " Valor: " + valor;
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==4)
                        {
                            #region sent
                            //Aqui biene una varialbe que se le cambia el valor
                            String id = raiz.ChildNodes[0].Token.Value.ToString();
                            String valor = recolecta(raiz.ChildNodes[2]);
                            String Tipo=null;
                            result += "\n"+ id + ":" + valor;
                            variables va = new variables();
                            if(Char.IsDigit(valor,0))
                            {
                                Tipo ="int";
                            }
                            else if (Char.IsLetter(valor,0))
                            {
                                if(valor=="true" || valor == "false")
                                {
                                    Tipo = "bool";
                                }
                                else if (valor.Length != 1)
                                {
                                    Tipo = "String";
                                }
                                else
                                {
                                    Tipo = "Char";
                                }
                            }
                            foreach (variables item in lisVariables)
                            {
                                if(item.Nombre==id)
                                {
                                    item.Valor = valor;
                                    item.Tipo = Tipo;
                                    result += " se cambio el valor";
                                }
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==3)
                        {
                            #region sent
                            String ids = recolecta(raiz.ChildNodes[1]);
                            result += "\n"+ ids;
                            //Aqui para guardar la variable en la tabla de simbolo esta variable no tiene valor
                            String[] temp = ids.Split(',');
                            for (int i = 0; i < temp.Length; i++)
                            {
                                variables va = new variables();
                                va.Ambito1 = ambito;
                                va.Tipo = null;
                                va.Nombre = temp[i];
                                va.Valor = null;
                                lisVariables.Add(va);
                            }
                            #endregion
                        }
                        #endregion;
                        break;
                    }
                case "Ids":
                    {
                        #region SuperSent
                        if (raiz.ChildNodes.Count == 3)
                        {
                            #region sent
                            //Aque el la posicon 2 tenemos un id y en la pocion 0 tenemos un id o Ids
                            String id = recolecta(raiz.ChildNodes[0]);
                            result = id + ","+raiz.ChildNodes[2].Token.Value.ToString();
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==1)
                        {
                            #region sent
                            //Aqui temos solo un id
                            result = raiz.ChildNodes[0].Token.Value.ToString();
                            #endregion
                        }
                        break;
                        #endregion
                    }
                case "E":
                    {
                        #region SuperSent
                        if (raiz.ChildNodes.Count==1)
                        {
                            #region sent
                            if (raiz.ChildNodes[0].Term.Name.ToString() != "ID")
                            {
                                result = raiz.ChildNodes[0].Token.Value.ToString();
                            }
                            else
                            {
                                String id = raiz.ChildNodes[0].Token.Value.ToString();
                                foreach (variables item in lisVariables)
                                {
                                    if(item.Nombre==id)
                                    {
                                        result = item.Valor;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==3)
                        {
                            #region sent 
                            if(raiz.ChildNodes[0].Term.Name.ToString()=="E")
                            {//se opera numeros jaj
                                #region sent Opracon
                                String valor1 = recolecta(raiz.ChildNodes[0]);
                                String valor2 = recolecta(raiz.ChildNodes[2]);
                                double v = Convert.ToDouble(valor1);
                                double v2 = Convert.ToDouble(valor2);
                                if (raiz.ChildNodes[1].Term.Name.ToString() == "mas")
                                {
                                    double re = v + v2;
                                    result = Convert.ToString(re);
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString() =="menos")
                                {
                                    double re = v - v2;
                                    result = Convert.ToString(re);
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString() =="multiplicacion")
                                {
                                    double re = v * v2;
                                    result = Convert.ToString(re);
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString() =="divicion")
                                {
                                    double re = v / v2;
                                    result = Convert.ToString(re);
                                }
                                #endregion
                            }
                            else if (raiz.ChildNodes[0].Term.Name.ToString()=="ID")
                            {//Aqui es porque estoy llamando a una funcion culera jaja
                                
                            }
                            else if (raiz.ChildNodes[0].Term.Name.ToString()=="(")
                            {
                                result = recolecta(raiz.ChildNodes[1]);
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==4)
                        {//se llama a una funcion con parmetros
                            #region sent

                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==6)
                        {//se invoco la raiz 
                            #region sent
                            string valor = recolecta(raiz.ChildNodes[2]);
                            string valor2 = recolecta(raiz.ChildNodes[4]);
                            double v = Convert.ToDouble(valor);
                            double v2 = Convert.ToDouble(valor2);
                            double re = Math.Pow(v, (1.0 / v2));
                            result = "" + re;
                            #endregion
                        }
                        break;
                        #endregion
                    }
                case "Log":
                    {
                        #region SuperSent
                        if(raiz.ChildNodes.Count==3)
                        {//aqui puede benir lo de and y or y (log)
                            #region sent
                            if(raiz.ChildNodes[0].Term.Name.ToString()=="(")
                            {
                                result = recolecta(raiz.ChildNodes[1]);
                            }
                            else if (raiz.ChildNodes[1].Term.Name.ToString()=="or")
                            {
                                String valor = recolecta(raiz.ChildNodes[0]);
                                String valor2 = recolecta(raiz.ChildNodes[2]);
                                bool v = Convert.ToBoolean(valor);
                                bool v2 = Convert.ToBoolean(valor2);
                                result = (v | v2) ? "true" : "false";
                            }
                            else if (raiz.ChildNodes[1].Term.Name.ToString()=="and")
                            {
                                String valor = recolecta(raiz.ChildNodes[0]);
                                String valor2 = recolecta(raiz.ChildNodes[2]);
                                bool v = Convert.ToBoolean(valor);
                                bool v2 = Convert.ToBoolean(valor2);
                                result = (v & v2) ? "true" : "false";
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==2)
                        {//aqui es el puto not
                            #region sent
                            string v = recolecta(raiz.ChildNodes[1]);
                            bool r = Convert.ToBoolean(v);
                            r = !r;
                            result = r.ToString().ToLower();
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==1)
                        {//valor logico o Ex
                            #region sent
                            if (raiz.ChildNodes[0].Term.Name.ToString() != "Ex")
                            {
                                result = raiz.ChildNodes[0].Token.Value.ToString();
                            }
                            else
                            {
                                result = recolecta(raiz.ChildNodes[0]);
                            }
                            #endregion
                        }
                        #endregion
                        break;
                    }
                case "Ex":
                    {
                        #region SuperSente
                        if(raiz.ChildNodes.Count==3)
                        {
                            #region sent
                            if (raiz.ChildNodes[0].Term.Name.ToString()=="(")
                            {
                                result = recolecta(raiz.ChildNodes[1]);
                            }
                            else
                            {
                                String valor = recolecta(raiz.ChildNodes[0]);
                                String valor2 = recolecta(raiz.ChildNodes[2]);
                                double v = Convert.ToDouble(valor);
                                double v2 = Convert.ToDouble(valor2);
                                if(raiz.ChildNodes[1].Term.Name.ToString()==">")
                                {
                                    result = (v > v2) ? "true" : "false";
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString()=="<")
                                {
                                    result = (v < v2) ? "true" : "false";
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString()==">=")
                                {
                                    result = (v >= v2) ? "true" : "false";
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString() == "<=")
                                {
                                    result = (v <= v2) ? "true" : "false";
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString()=="==")
                                {
                                    result = (v == v2) ? "true" : "false";
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString()=="!=")
                                {
                                    result = (v != v2) ? "true" : "false";
                                }
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==1)
	                    {
                            result = recolecta(raiz.ChildNodes[0]);
	                    }
                        #endregion
                        break;
                    }

                case "Sent":
                    {
                        #region Super Region
                        if(raiz.ChildNodes.Count==2)
                        {
                            result += recolecta(raiz.ChildNodes[0]);
                            result += recolecta(raiz.ChildNodes[1]);
                        }
                        else if (raiz.ChildNodes.Count==1)
                        {
                            result += recolecta(raiz.ChildNodes[0]);
                        }
                        #endregion
                        break;
                    }
                case "Sent'":
                    {
                        #region SUPER sENT
                        result += recolecta(raiz.ChildNodes[0]);
                        #endregion
                        break;
                    }
            }

            return result;
        }

        public static String acciones(ParseTreeNode raiz) 
        {
            String result = null;
            switch (raiz.Term.Name.ToString())
            {
                case "Dvariables":
                    {
                        #region Super Sent
                        if (raiz.ChildNodes.Count == 5)
                        {
                            #region sent
                            //Aqui biene una delacion de una variale y se le asigna varlor de una ves
                            String valor = acciones(raiz.ChildNodes[3]);
                            String id = raiz.ChildNodes[1].Token.Value.ToString();
                            String Tipo = null;
                            if (Char.IsDigit(valor, 0))
                            {
                                Tipo = "int";
                            }
                            else if (Char.IsLetter(valor, 0))
                            {
                                if (valor == "true" || valor == "false")
                                {
                                    Tipo = "bool";
                                }
                                else if (valor.Length != 1)
                                {
                                    Tipo = "String";
                                }
                                else
                                {
                                    Tipo = "Char";
                                }
                            }
                            variables va = new variables();
                            va.Ambito1 = ambito;
                            va.Nombre = id;
                            va.Tipo = Tipo;
                            va.Valor = valor;
                            //result += "\nSe creo : " + id + " Valor: " + valor;
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 4)
                        {
                            #region sent
                            //Aqui biene una varialbe que se le cambia el valor
                            String id = raiz.ChildNodes[0].Token.Value.ToString();
                            String valor = acciones(raiz.ChildNodes[2]);
                            String Tipo = null;
                            //result += "\n" + id + ":" + valor;
                            variables va = new variables();
                            if (Char.IsDigit(valor, 0))
                            {
                                Tipo = "int";
                            }
                            else if (Char.IsLetter(valor, 0))
                            {
                                if (valor == "true" || valor == "false")
                                {
                                    Tipo = "bool";
                                }
                                else if (valor.Length != 1)
                                {
                                    Tipo = "String";
                                }
                                else
                                {
                                    Tipo = "Char";
                                }
                            }
                            foreach (variables item in lisVariables)
                            {
                                if (item.Nombre == id)
                                {
                                    item.Valor = valor;
                                    item.Tipo = Tipo;
                                    //result += " se cambio el valor";
                                }
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 3)
                        {
                            #region sent
                            String ids = acciones(raiz.ChildNodes[1]);
                            //result += "\n" + ids;
                            //Aqui para guardar la variable en la tabla de simbolo esta variable no tiene valor
                            String[] temp = ids.Split(',');
                            for (int i = 0; i < temp.Length; i++)
                            {
                                variables va = new variables();
                                va.Ambito1 = ambito;
                                va.Tipo = null;
                                va.Nombre = temp[i];
                                va.Valor = null;
                                lisVariables.Add(va);
                            }
                            #endregion
                        }
                        #endregion;
                        break;
                    }
                case "Ids":
                    {
                        #region SuperSent
                        if (raiz.ChildNodes.Count == 3)
                        {
                            #region sent
                            //Aque el la posicon 2 tenemos un id y en la pocion 0 tenemos un id o Ids
                            String id = acciones(raiz.ChildNodes[0]);
                            result = id + "," + raiz.ChildNodes[2].Token.Value.ToString();
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 1)
                        {
                            #region sent
                            //Aqui temos solo un id
                            result = raiz.ChildNodes[0].Token.Value.ToString();
                            #endregion
                        }
                        break;
                        #endregion
                    }
                case "E":
                    {
                        #region SuperSent
                        if (raiz.ChildNodes.Count == 1)
                        {
                            #region sent
                            if (raiz.ChildNodes[0].Term.Name.ToString() != "ID")
                            {
                                result = raiz.ChildNodes[0].Token.Value.ToString();
                            }
                            else
                            {
                                String id = raiz.ChildNodes[0].Token.Value.ToString();
                                foreach (variables item in lisVariables)
                                {
                                    if (item.Nombre == id)
                                    {
                                        result = item.Valor;
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 3)
                        {
                            #region sent
                            if (raiz.ChildNodes[0].Term.Name.ToString() == "E")
                            {//se opera numeros jaj
                                #region sent Opracon
                                String valor1 = acciones(raiz.ChildNodes[0]);
                                String valor2 = acciones(raiz.ChildNodes[2]);
                                if (Char.IsDigit(valor1, 0) & Char.IsDigit(valor2, 0))
                                {
                                    double v = Convert.ToDouble(valor1);
                                    double v2 = Convert.ToDouble(valor2);
                                    if (raiz.ChildNodes[1].Term.Name.ToString() == "mas")
                                    {
                                        double re = v + v2;
                                        result = Convert.ToString(re);
                                    }
                                    else if (raiz.ChildNodes[1].Term.Name.ToString() == "menos")
                                    {
                                        double re = v - v2;
                                        result = Convert.ToString(re);
                                    }
                                    else if (raiz.ChildNodes[1].Term.Name.ToString() == "multiplicacion")
                                    {
                                        double re = v * v2;
                                        result = Convert.ToString(re);
                                    }
                                    else if (raiz.ChildNodes[1].Term.Name.ToString() == "divicion")
                                    {
                                        double re = v / v2;
                                        result = Convert.ToString(re);
                                    }
                                }
                                else 
                                { 
                                    if(raiz.ChildNodes[1].Term.Name.ToString() == "mas")
                                    {
                                        result = valor1 + valor2;
                                    }
                                }
                                
                                #endregion
                            }
                            else if (raiz.ChildNodes[0].Term.Name.ToString() == "ID")
                            {//Aqui es porque estoy llamando a una funcion culera jaja

                            }
                            else if (raiz.ChildNodes[0].Term.Name.ToString() == "(")
                            {
                                result = acciones(raiz.ChildNodes[1]);
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 4)
                        {//se llama a una funcion con parmetros
                            #region sent

                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 6)
                        {//se invoco la raiz 
                            #region sent
                            string valor = acciones(raiz.ChildNodes[2]);
                            string valor2 = acciones(raiz.ChildNodes[4]);
                            double v = Convert.ToDouble(valor);
                            double v2 = Convert.ToDouble(valor2);
                            double re = Math.Pow(v, (1.0 / v2));
                            result = "" + re;
                            #endregion
                        }
                        break;
                        #endregion
                    }
                case "Log":
                    {
                        #region SuperSent
                        if (raiz.ChildNodes.Count == 3)
                        {//aqui puede benir lo de and y or y (log)
                            #region sent
                            if (raiz.ChildNodes[0].Term.Name.ToString() == "(")
                            {
                                result = acciones(raiz.ChildNodes[1]);
                            }
                            else if (raiz.ChildNodes[1].Term.Name.ToString() == "or")
                            {
                                String valor = acciones(raiz.ChildNodes[0]);
                                String valor2 = acciones(raiz.ChildNodes[2]);
                                bool v = Convert.ToBoolean(valor);
                                bool v2 = Convert.ToBoolean(valor2);
                                result = (v | v2) ? "true" : "false";
                            }
                            else if (raiz.ChildNodes[1].Term.Name.ToString() == "and")
                            {
                                String valor = acciones(raiz.ChildNodes[0]);
                                String valor2 = acciones(raiz.ChildNodes[2]);
                                bool v = Convert.ToBoolean(valor);
                                bool v2 = Convert.ToBoolean(valor2);
                                result = (v & v2) ? "true" : "false";
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 2)
                        {//aqui es el puto not
                            #region sent
                            string v = acciones(raiz.ChildNodes[1]);
                            bool r = Convert.ToBoolean(v);
                            r = !r;
                            result = r.ToString().ToLower();
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 1)
                        {//valor logico o Ex
                            #region sent
                            if (raiz.ChildNodes[0].Term.Name.ToString() != "Ex")
                            {
                                result = raiz.ChildNodes[0].Token.Value.ToString();
                            }
                            else
                            {
                                result = acciones(raiz.ChildNodes[0]);
                            }
                            #endregion
                        }
                        #endregion
                        break;
                    }
                case "Ex":
                    {
                        #region SuperSente
                        if (raiz.ChildNodes.Count == 3)
                        {
                            #region sent
                            if (raiz.ChildNodes[0].Term.Name.ToString() == "(")
                            {
                                result = acciones(raiz.ChildNodes[1]);
                            }
                            else
                            {
                                String valor = acciones(raiz.ChildNodes[0]);
                                String valor2 = acciones(raiz.ChildNodes[2]);
                                double v = Convert.ToDouble(valor);
                                double v2 = Convert.ToDouble(valor2);
                                if (raiz.ChildNodes[1].Term.Name.ToString() == ">")
                                {
                                    result = (v > v2) ? "true" : "false";
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString() == "<")
                                {
                                    result = (v < v2) ? "true" : "false";
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString() == ">=")
                                {
                                    result = (v >= v2) ? "true" : "false";
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString() == "<=")
                                {
                                    result = (v <= v2) ? "true" : "false";
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString() == "==")
                                {
                                    result = (v == v2) ? "true" : "false";
                                }
                                else if (raiz.ChildNodes[1].Term.Name.ToString() == "!=")
                                {
                                    result = (v != v2) ? "true" : "false";
                                }
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 1)
                        {
                            result = acciones(raiz.ChildNodes[0]);
                        }
                        #endregion
                        break;
                    }

                case "Sent":
                    {
                        #region Super Region
                        if (raiz.ChildNodes.Count == 2)
                        {
                            result += acciones(raiz.ChildNodes[0]);
                            result += acciones(raiz.ChildNodes[1]);
                        }
                        else if (raiz.ChildNodes.Count == 1)
                        {
                            result += acciones(raiz.ChildNodes[0]);
                        }
                        #endregion
                        break;
                    }
                case "Sent'":
                    {
                        #region SUPER sENT
                        if(raiz.ChildNodes.Count==1)
                        {
                            result += acciones(raiz.ChildNodes[0]);
                        }
                        else if (raiz.ChildNodes.Count==5)
                        {
                            #region Sent
                            if (raiz.ChildNodes[0].Term.Name.ToString() == "imprimir")
                            {
                                String valor = acciones(raiz.ChildNodes[2]);
                                result += "\n "+valor;
                            }
                            else if (raiz.ChildNodes[0].Term.Name.ToString()=="ID")
                            {//Aqui es en donde se llama a una funcion con parametros

                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 4)
                        {//Aqui se llama a una funcion sin Parametros yolo
                            String id = raiz.ChildNodes[0].Token.Value.ToString();
                            //result += "\n yoloa entro";
                            foreach (Fun item in lisFunciones)
                            {
                                //result += "\nfuncion: " + item.Nombre + " funciona a buscar :" + id;
                                if (item.Nombre == id)
                                {
                                    result += acciones(item.Acciones1);
                                    break;
                                }
                            }
                        }
                        
                        #endregion
                        break;
                    }
                case "IF":
                    {
                        #region Super Sent
                        if (raiz.ChildNodes.Count == 7)
                        {
                            #region Sent
                            String log = acciones(raiz.ChildNodes[2]);
                            if(Convert.ToBoolean(log))
                            {
                                result += acciones(raiz.ChildNodes[5]);
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==11)
                        {
                            #region Sent
                            String log = acciones(raiz.ChildNodes[2]);
                            if(Convert.ToBoolean(log))
                            {
                                result += acciones(raiz.ChildNodes[5]);
                            }
                            else
                            {
                                result += acciones(raiz.ChildNodes[9]);
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==8)
                        {
                            #region Sent
                            String log = acciones(raiz.ChildNodes[2]);
                            if(Convert.ToBoolean(log))
                            {
                                result += acciones(raiz.ChildNodes[5]);
                            }
                            else
                            {
                                result += acciones(raiz.ChildNodes[7]);
                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count==12)
                        {//Aqui es con el multisino y sino
                            #region Sent

                            #endregion
                        }
                        #endregion
                        break;
                    }

                case "Sent2":
                    {
                        #region Super Sent
                        if (raiz.ChildNodes.Count == 2)
                        {
                            result += acciones(raiz.ChildNodes[0]);
                            result += acciones(raiz.ChildNodes[1]);
                        }
                        else if (raiz.ChildNodes.Count == 1)
                        {
                            result += acciones(raiz.ChildNodes[0]);
                        }
                        #endregion
                        break;
                    }

                case "Sent2'":
                    {
                        #region Super Sent
                        if (raiz.ChildNodes.Count == 1)
                        {
                            result += acciones(raiz.ChildNodes[0]);
                        }
                        else if (raiz.ChildNodes.Count == 5)
                        {
                            #region Sent
                            if (raiz.ChildNodes[0].Term.Name.ToString() == "imprimir")
                            {
                                String valor = acciones(raiz.ChildNodes[2]);
                                result += "\n " + valor;
                            }
                            else if (raiz.ChildNodes[0].Term.Name.ToString() == "ID")
                            {//Aqui es en donde se llama a una funcion con parametros

                            }
                            #endregion
                        }
                        else if (raiz.ChildNodes.Count == 4)
                        {//Aqui se llama a una funcion sin Parametros yolo
                            String id = raiz.ChildNodes[0].Token.Value.ToString();
                            foreach (Fun item in lisFunciones)
                            {
                                //result += "funcion: " + item.Nombre + " funciona a buscar :" + id;
                                if(item.Nombre == id)
                                {
                                    result += acciones(item.Acciones1);
                                    break;
                                }
                            }
                        }
                        #endregion
                        break;
                    }
                case "Mulsinosi":
                    {
                        #region Super Sente
                        if(raiz.ChildNodes.Count==2)
                        {
                            result += acciones(raiz.ChildNodes[0]);
                            result += acciones(raiz.ChildNodes[1]);
                        }
                        else if (raiz.ChildNodes.Count==1)
                        {
                            result += acciones(raiz.ChildNodes[0]);
                        }
                        #endregion
                        break;
                    }
                case "Sinosi":
                    {
                        #region Super Sent
                        if(raiz.ChildNodes.Count==7)
                        {
                            String log = acciones(raiz.ChildNodes[2]);
                            if (Convert.ToBoolean(log))
                            {
                                result += acciones(raiz.ChildNodes[5]);
                            }
                        }
                        #endregion
                        break;
                    }
                case "Interrup":
                    {
                        #region Super Sent
                        if(raiz.ChildNodes.Count==7)
                        {
                            String valor = acciones(raiz.ChildNodes[2]);
                            Expre = valor;
                            result += acciones(raiz.ChildNodes[5]);
                            Expre = null;
                            bo = 0;
                        }
                        #endregion
                        break;
                    }
                case "Casos":
                    {
                        #region Super Sent
                        if(raiz.ChildNodes.Count==2)
                        {
                            result += acciones(raiz.ChildNodes[0]);
                            result += acciones(raiz.ChildNodes[1]);
                        }
                        else if (raiz.ChildNodes.Count==1)
                        {
                            result += acciones(raiz.ChildNodes[0]);
                        }
                        #endregion
                        break;
                        }
                case "Caso":
                    {
                        #region Super Sent 
                        if(raiz.ChildNodes.Count==4)
                        {
                            String valor = acciones(raiz.ChildNodes[1]);
                            if(valor.Equals(Expre))
                            {
                                result += acciones(raiz.ChildNodes[3]);
                                bo = 1;
                            }
                        }
                        else if (raiz.ChildNodes.Count==3)
                        {
                            if(bo!=1){
                                result += acciones(raiz.ChildNodes[2]);
                            }
                        }
                        #endregion
                        break;
                    }
                case "PorCaso":
                    {
                        #region Super Sent
                        if(raiz.ChildNodes.Count==1)
                        {
                            result = raiz.ChildNodes[0].Token.Value.ToString();
                        }
                        #endregion
                        break;
                    }
                case "Mien":
                    {
                        #region Super Sent
                        if(raiz.ChildNodes.Count==7)
                        {
                            String log = acciones(raiz.ChildNodes[2]);
                            while(Convert.ToBoolean(log))
                            {
                                result += acciones(raiz.ChildNodes[5]);
                                log = acciones(raiz.ChildNodes[2]);
                            }
                        }
                        #endregion
                        break;
                    }
                case "Hace":
                    {
                        #region Super Sent
                        if(raiz.ChildNodes.Count==9)
                        {
                            result += acciones(raiz.ChildNodes[2]);
                            String log = acciones(raiz.ChildNodes[6]);
                            while(Convert.ToBoolean(log))
                            {
                                result += acciones(raiz.ChildNodes[2]);
                                log = acciones(raiz.ChildNodes[6]);
                            }
                        }
                        #endregion
                        break;
                    }
            }

            return result;
        }
    }
}