using Irony.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica2_Compi1_Web_2016.Analisadores
{
    public class Fun
    {
        public Fun(String nombre, String tipo, ArrayList parametos, ParseTreeNode acciones) 
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.Parametros = parametos;
            this.Acciones = acciones;
        }

        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private String tipo;

        public String Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        private ArrayList Parametros;

        public ArrayList Parametros1
        {
            get { return Parametros; }
            set { Parametros = value; }
        }
        private ParseTreeNode Acciones;

        public ParseTreeNode Acciones1
        {
            get { return Acciones; }
            set { Acciones = value; }
        }
    }
}