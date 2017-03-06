<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebParser.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="headTag">
  <title>Products List</title>
  <link href="css/uikit.min.css" rel="stylesheet"/>
  <link href="css/uikit.gradient.css" rel="stylesheet"/>
  <link href="css/components/form-select.css" rel="stylesheet"/>
  <link href="css/components/notify.gradient.css" rel="stylesheet"/>
  <link href="css/components/upload.css" rel="stylesheet"/>
  <link href="css/components/notify.css" rel="stylesheet"/>
  <link href="css/page.css" rel="stylesheet"/>

  <script src="scripts/jquery-3.1.1.min.js"></script>
  <script src="scripts/uikit.js"></script>
  <script src="scripts/components/notify.js"></script>
  <script src="scripts/components/upload.js"></script>
  <script src="scripts/components/pagination.min.js"></script>
  <script src="scripts/viewer.js"></script>
</head>

<body>
<div class="uk-container uk-container-center">
  <header class="uk-margin-top">
    <div class="uk-grid uk-grid-small">
      <h2 class="uk-width-1-2 caption" style="text-transform: uppercase;">Products page</h2>
    </div>
  </header>
  <nav class="uk-navbar uk-hidden-small">
    <ul id="Tabs" class="uk-navbar-nav">
      <li class="uk-active">
        <a class="uk-text-primary uk-text-bold" href="View.aspx">Products</a>
      </li>
    </ul>
  </nav>
  <form id="form1" runat="server" class="uk-margin-top">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server">
      <ContentTemplate>
        <div id="filtersDiv" class="uk-panel uk-panel-box uk-grid uk-grid-small uk-form uk-margin-remove divHead uk-width-1-1">
          <div class="min-width-200 uk-width-1-6 uk-float-right">
            <label class="uk-text-bold uk-margin-small-right">Products on page: </label>
            <asp:DropDownList CssClass="uk-width-1-10 min-width-60" ID="ProductsOnPage" AutoPostBack="true" runat="server"></asp:DropDownList>
          </div>
        </div>
        <div id="DivMain" class="uk-width-1-1 uk-margin-top" runat="server">
        </div>
        <asp:Button runat="server" CssClass="uk-hidden" ID="btnPage"/>
        <ul id="pages" class="uk-pagination" data-uk-pagination></ul>
      </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox AutoPostBack="True" EnableViewState="False" ValidateRequestMode="Disabled" ViewStateMode="Disabled" runat="server" ID="pageNumber" Text="0" class="uk-hidden"></asp:TextBox>
    <asp:TextBox AutoPostBack="True" EnableViewState="False" ValidateRequestMode="Disabled" ViewStateMode="Disabled" runat="server" ID="imageBase64" class="uk-hidden"></asp:TextBox>
    <asp:TextBox runat="server" ID="productID" class="uk-hidden"></asp:TextBox>
    <div runat="server" id="gridGlobal" class="uk-modal">
      <div class="uk-margin uk-width-1-1 uk-modal-dialog-large uk-modal-dialog">
        <a class="uk-close uk-modal-close"></a>
        <div class="uk-clearfix uk-margin-top uk-margin-bottom">
          <h2 id="HeadProduct" class="uk-float-left uk-margin-small-bottom">Product</h2>
        </div>
        <div class="uk-width-1-1 uk-grid uk-margin-bottom">
          <div class="uk-width-1-1 uk-width-medium-1-4">
            <div class="wrapper_h uk-margin-bottom uk-thumbnail">
              <div class="uk-thumbnail uk-container-center">
                <asp:Image ID="ImageProduct" runat="server"/>
              </div>
            </div>
          </div>
          <div class="uk-width-1-1 uk-width-medium-3-4 uk-form-row">
            <div class="uk-form-row uk-grid uk-grid-small uk-margin-remove">
              <div class="uk-width-1-1">
                <div class="uk-form-row">
                  <div class="uk-form uk-grid">
                    <label class="uk-form-label uk-text-bold uk-width-small-1-10 uk-width-1-10">Title: </label>
                    <input name="TitleProduct" runat="server" id="TitleProduct" class="uk-width-small-9-10 uk-width-9-10"/>
                  </div>
                </div>
                <div class="uk-form-row">
                  <div class="uk-form uk-grid">
                    <label class="uk-form-label uk-text-bold uk-width-small-1-10 uk-width-1-10">Link: </label>
                    <input name="LinkProduct" type="text" id="LinkProduct" placeholder="http://www.example.com" class="uk-width-9-10" autocomplete="off" runat="server"/>
                  </div>
                </div>
              </div>
            </div>
            <div class="uk-form-row uk-form">
              <label class="uk-form-label uk-text-bold">Description: </label>
              <textarea name="DescriptionProduct" rows="10" cols="20" id="DescriptionProduct" class="uk-width-1-1" autocomplete="off" runat="server" style="resize: none;"></textarea>
              <div class="uk-form uk-margin-top">
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </form>
  <footer id="footer">
    <a class="uk-icon-hover uk-icon-github uk-float-right uk-margin-bottom uk-text-bold">Copyright info</a>
  </footer>
</div>
</body>
</html>
