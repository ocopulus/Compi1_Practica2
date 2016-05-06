using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica2_Compi1_Web_2016.Analisadores
{
    public class variables
    {
        private String Ambito;

        public String Ambito1
        {
            get { return Ambito; }
            set { Ambito = value; }
        }
        
        private String tipo;

        public String Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private String valor;

        public String Valor
        {
            get { return valor; }
            set { valor = value; }
        }
    }
}