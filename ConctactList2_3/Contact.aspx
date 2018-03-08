<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<!DOCTYPE html>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<link href="https://fonts.googleapis.com/css?family=PT+Sans" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml">
<script>
    function isNumberKey(evnt) {
        var CharCode = (evnt.which) ? evnt.which : evnt.keyCode;
        if (CharCode > 31 && (CharCode < 48) || CharCode > 57)){
            return (alert("yes"));
        }

        return (alert("no"));
    }
</script>
<head runat="server">
    <title>Contact List</title>
</head>
<body>
 <div class="container-fluid">
     <div class="row">
         <div class="col-xs-2"> </div>
         <div class="col-xs-10"> 
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hf" runat="server" />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtFirst" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtLast" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Phone Number"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtPhone" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblSuccessMessage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gvCotact" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="First" HeaderText="First Name" />
                    <asp:BoundField DataField="Last" HeaderText="Last Name" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone Number" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="lnkOnClick" >View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>

   </div>
  </div>
 </div>
</body>
</html>
