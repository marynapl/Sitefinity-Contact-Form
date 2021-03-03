<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile Name="Styles/Ajax.css" />
</sitefinity:ResourceLinks>
<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView" style="max-height: 400px; overflow: auto; ">
    <ol>        
        <li class="sfFormCtrl">
            <asp:Label runat="server" AssociatedControlID="InternalRecipients" CssClass="sfTxtLbl">InternalRecipients</asp:Label>
            <asp:TextBox ID="InternalRecipients" runat="server" CssClass="sfTxt" />
            <div class="sfExample">email@merrithew.com</div>
        </li>
    
        <li class="sfFormCtrl">
            <asp:Label runat="server" AssociatedControlID="EmailSubjectLine" CssClass="sfTxtLbl">EmailSubjectLine</asp:Label>
            <asp:TextBox ID="EmailSubjectLine" runat="server" CssClass="sfTxt" />
            <div class="sfExample">Form submission</div>
        </li>
    
    </ol>
</div>