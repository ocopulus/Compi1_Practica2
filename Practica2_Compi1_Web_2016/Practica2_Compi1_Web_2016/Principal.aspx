<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Practica2_Compi1_Web_2016.Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">

            <div class="col-ms-12">
                <asp:Label ID="lblCodigo" runat="server" Text="Codigo:" CssClass="control-label" Font-Size="X-Large"></asp:Label>
                <br />
                <div class="col-md-10 col-md-offset-1">
                    <asp:TextBox ID="txtCodigo" runat="server" TextMode="MultiLine" ToolTip="Codigo yolo" CssClass="form-control" Height="400" ></asp:TextBox>
                </div>
                <asp:Button ID="btnEjecutar" runat="server" Text="Ejecutar" CssClass="btn btn-primary" OnClick="btnEjecutar_Click" />
                <br /><br />
                <asp:Button ID="btnArbolAST" runat="server" Text="Arbol AST" CssClass="btn btn-primary" OnClick="btnArbolAST_Click" />
            </div>
            
            <div class="col-md-12">
                <br />
                <asp:FileUpload ID="fileUP" runat="server" />
                <br />
                <asp:Button ID="btnCargar" runat="server" Text="Cargar" CssClass="btn btn-primary" OnClick="btnCargar_Click" />
            </div>

            <div class="col-md-12">
                <br />
                <asp:Label ID="lblResultado" runat="server" Text="Resultado: " CssClass="control-label" Font-Size="X-Large"></asp:Label>
                <div class="col-md-10 col-md-offset-1">
                    <asp:TextBox ID="txtresultado" runat="server" TextMode="MultiLine" CssClass="form-control" Height="200"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12">
                <br />
                <asp:Label ID="lblErrores" runat="server" Text="Errores :" CssClass="control-label" Font-Size="X-Large"></asp:Label>
                <br />
                <div class="col-md-10 col-md-offset-1">
                    <asp:TextBox ID="txtErrores" runat="server" TextMode="MultiLine" ToolTip="Errores yolo" CssClass="form-control" Height="200" ></asp:TextBox>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
