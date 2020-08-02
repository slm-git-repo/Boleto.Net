<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EnvioEmail.aspx.cs" Inherits="EnvioEmail" Title="Untitled Page" %>

<%@ Register Assembly="Boleto.Net" Namespace="BoletoNet" TagPrefix="boletonet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        Para o envio do boleto por email é necessário configurar &lt;system.net&gt; no web.config.
    </p>
    <div class="boxinfo">
        <span class="info">&lt;system.net&gt;<br />
            &nbsp;&nbsp;&lt;mailSettings&gt;<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&lt;smtp from=&quot;mymail@mymail.com&quot;&gt;<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;network host=&quot;smtp.myhost.com&quot;
            port=&quot;25&quot; defaultCredentials=&quot;true&quot; /&gt;<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&lt;/smtp&gt;<br />
            &nbsp;&nbsp;&lt;/mailSettings&gt;<br />
            &lt;/system.net&gt; </span>
    </div>
    <p>
        O exemplo abaixo somente funciona para envio de boleto com site on-line, ou seja,
        devido as imagens que fazem parte do boleto, o caminho das imagens que é enviado
        por email é referenciado o site de origem.
    </p>
    <p>
        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="tipo" Text="On-Line" /><asp:RadioButton ID="RadioButton2" GroupName="tipo"
            runat="server" Text="Off-Line" Checked="True" />
        <br />
    <br />
        Email para envio:<br />
        <asp:TextBox ID="TextBox1" runat="server" Width="300px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Enviar" OnClick="Button1_Click" /><br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
    </p>
</asp:Content>
