using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Practica2_Compi1_Web_2016.Analisadores;
using Irony.Ast;
using Irony.Parsing;
using System.Collections;

namespace Practica2_Compi1_Web_2016
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (fileUP.HasFile)
            {
                try
                {
                    fileUP.SaveAs("C:\\sub\\" + fileUP.FileName);
                    System.IO.StreamReader file = new System.IO.StreamReader(@"C:\\sub\\"+fileUP.FileName);
                    String text="",line;
                    while((line=file.ReadLine())!=null)
                    {
                        text += line+"\n";
                    }
                    file.Close();
                    txtCodigo.Text = text;
                }
                catch (Exception ex)
                {

                }
            }
            else 
            {
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=JavaScript>alert('No a Seleccionado un archivo')</SCRIPT>");
            }
        }

        protected void btnEjecutar_Click(object sender, EventArgs e)
        {
       
            
            ParseTree resultado = Sintactico.analizar(txtCodigo.Text);
            if(resultado.Root != null)
            {
                //AST.ImageUrl("C:\\sub\\graf.png");
                //AST.ImageUrl = Server.MapPath("C:\\sub\\graf.png");
                ParseTreeNode raiz = resultado.Root;
                Sintactico.arboll(raiz);
                String ejecuto = recolectar.iniciar(raiz);
                txtresultado.Text = ejecuto;
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=JavaScript>alert('Arbol generado')</SCRIPT>");
            }
            else
            {
                String hola = resultado.ParserMessages.ElementAt(0).ToString();
                txtErrores.Text = hola;
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=JavaScript>alert('No se genero el arbol "+hola +"')</SCRIPT>");
            }
        }

        protected void btnArbolAST_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:/sub/graf.png");
        }
    }
}