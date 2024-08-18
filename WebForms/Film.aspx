<%@ Page Title="Film" Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Film.aspx.cs" Inherits="WebForms.Film" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h3 id="title"><%: Title %></h3>
    <br />

    <div class="col-lg-12 mb-4">
    <!-- Illustrations -->
        <div class="card shadow mb-4">
           <div class="card-header py-3">
            <h6 class="m-0 font-weight-bold text-primary">Kelola Data Film</h6>
           </div>
           

             <!-- OPEN TAG BUTTON-->
            <div class="container-fluid">
                <div id="BtnAction" runat="server">
                    <br />
                    <span class="text-center text-primary">Cari</span>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="form-check-label btn btn-secondary" OnClick="btnSearch_Click" Text="Search" />
                    <asp:Button ID="btnTambah" runat="server" CssClass="form-check-label btn btn-primary" OnClick="btnTambah_Click" Text="Tambah" />
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
                            <asp:LinkButton runat="server" ID="linkEdit" CommandName="Ubah" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                ToolTip="Ubah Data Table"><span class="fa fa-edit" aria-hidden="true"></span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="linkHapus" CommandName="Hapus" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                ToolTip="Hapus Data Table" OnClientClick="return confirm('Yakin Hapus?');"><span class="fa fa-trash" aria-hidden="true"></span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>                
                
                </asp:gridview>
            </div>
            <!-- CLOSED TAG VIEW -->

            <!-- OPEN TAG FORM-->
            <div class="container-fluid">
                <div class="row-cols-2" id="Form" runat="server">
                    <div class="col">
                        <div class="form-group">
                            <!-- TEXTBOX -->
                            <asp:Label id="lblId" Visible="false" runat="server"></asp:Label>
                            <asp:Label ID="Label3" Text="*" ForeColor="Red" runat="server"></asp:Label>
                            <label>Nama Film</label>
                            <asp:TextBox ID="TxtNamaFilm" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtNamaFilm" runat="server"
                                display="Dynamic" ForeColor="Red" Text="(Required)" SetFocusOnError="true" ValidationGroup="ValForm" EnableClientScript="False">
                            </asp:RequiredFieldValidator>
                            <br />
                            <!-- TEXTBOX -->
                            <asp:Label ID="Label11" Text="*" ForeColor="Red" runat="server"></asp:Label>
                            <label>Deskripsi Film</label>
                            <asp:TextBox ID="TxtDeskripsiFilm" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="TxtDeskripsiFilm" runat="server"
                                display="Dynamic" ForeColor="Red" Text="(Required)" SetFocusOnError="true" ValidationGroup="ValForm" EnableClientScript="False">
                            </asp:RequiredFieldValidator>
                            <br />
                            <!-- DATE -->
<%--                            <asp:Label ID="Label4" Text="*" ForeColor="Red" runat="server"></asp:Label>
                            <label>Tanggal Tayang</label>
                            <asp:TextBox ID="TxtTanggalTayang" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="TxtTanggalTayang" runat="server"
                                display="Dynamic" ForeColor="Red" Text="(Required)" SetFocusOnError="true" ValidationGroup="ValForm" EnableClientScript="False">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtTanggalTayang"
                                ValidationExpression="^\d{4}-\d{2}-\d{2}$" ErrorMessage="Format tanggal harus yyyy-mm-dd" ForeColor="Red">
                            </asp:RegularExpressionValidator>--%>
                            <asp:Label ID="Label4" Text="*" ForeColor="Red" runat="server"></asp:Label>
                            <label>Tanggal dan Jam Tayang</label>
                            <asp:TextBox ID="TxtTanggalTayang" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="TxtTanggalTayang" runat="server"
                                Display="Dynamic" ForeColor="Red" Text="(Required)" SetFocusOnError="true" ValidationGroup="ValForm" EnableClientScript="False">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtTanggalTayang"
                                ValidationExpression="^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$" ErrorMessage="Format tanggal dan jam harus yyyy-mm-ddTHH:mm" ForeColor="Red">
                            </asp:RegularExpressionValidator>
                            <br />
                            <!-- FILE UPLOAD -->
                            <asp:Label ID="Label5" Text="*" ForeColor="Red" runat="server"></asp:Label>
                            <label>Upload Poster</label>
                            <asp:FileUpload ID="FileUploadPoster" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="FileUploadPoster" runat="server"
                                display="Dynamic" ForeColor="Red" Text="(Required)" SetFocusOnError="true" ValidationGroup="ValForm" EnableClientScript="False">
                            </asp:RequiredFieldValidator>
                            <br />
                        </div>
                    </div>
                    <div class="w-100"></div>
                    <div class="col">
                        <asp:Button ID="BtnSubmit" runat="server" Text="Save" CssClass="btn btn-success" OnClick="BtnSubmit_Click" ValidationGroup="ValForm" />
                        &nbsp;
                        &nbsp;
                        &nbsp;
                        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="BtnCancel_Click" />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
            <!-- CLOSED TAG FORM-->

           </div>
        <!-- Close Illustrations -->
        </div>

    </main>
</asp:Content>