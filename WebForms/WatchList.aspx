<%@ Page Title="WatchList" Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WatchList.aspx.cs" Inherits="WebForms.WatchList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h3 id="title"><%: Title %></h3>

    <br />

    <div class="col-lg-12 mb-4">
    <!-- Illustrations -->
        <div class="card shadow mb-4">
           <div class="card-header py-3">
            <h6 class="m-0 font-weight-bold text-primary">WatchList</h6>
           </div>
           

             <!-- OPEN TAG BUTTON-->
            <div class="container-fluid">
                <div id="BtnAction" runat="server">
                    <br />
                    <span class="text-center text-primary">Cari</span>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="form-check-label btn btn-secondary" OnClick="btnSearch_Click" Text="Search" />
                </div>
            </div>
            <br />
            <!-- CLOSED TAG BUTTON-->
           

            <!-- OPEN TAG VIEW -->
            <div id="ViewForm" class="container-fluid" runat="server">
                <asp:GridView runat="server" ID="DGView" CssClass="table table-striped table-bordered table-hover" AllowPaging="true"
                    AllowSorting="true" AutoGenerateColumns="false" DataKeyNames="KodeFilm" EmptyDataText="Tidak Ada Data" PageSize="10" PagerStyle-CssClas="pagination-ys"
                    ShowHeaderWhenEmpty="true" OnPageIndexChanging="DGView_PageIndexChanging" OnRowCommand="DGView_RowCommand" OnSorting="DGView_Sorting">
                <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" LastPageText=">>" />
                <Columns>
                    <asp:TemplateField HeaderText="No." ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    <asp:BoundField DataField="NamaFilm" HeaderText="Nama Film" NullDisplayText="-" ItemStyle-HorizontalAlign="left" SortExpression="NamaFilm" />
                    <asp:BoundField DataField="DeskripsiFilm" HeaderText="Deskripsi Film" NullDisplayText="-" ItemStyle-HorizontalAlign="left" SortExpression="DeskripsiFilm" />
                    <asp:BoundField DataField="TanggalTayang" HeaderText="Tanggal Tayang" NullDisplayText="-" ItemStyle-HorizontalAlign="left" SortExpression="TanggalTayang" />
                    <asp:TemplateField HeaderText="Aksi" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="center-header">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="linkHapus" CommandName="Hapus" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                ToolTip="Hapus Data Table" OnClientClick="return confirm('Yakin Hapus?');"><span class="fa fa-trash" aria-hidden="true"></span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>                
                
                </asp:gridview>
            </div>
            <!-- CLOSED TAG VIEW -->

           </div>
        <!-- Close Illustrations -->
        </div>

    </main>
</asp:Content>