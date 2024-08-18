<%@ Page Title="Film List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="film-container">
        <asp:Repeater ID="FilmRepeater" runat="server">
            <ItemTemplate>
                <div class="film-card">
                    <div class="film-card-content">
                        <asp:Image ID="FilmImage" runat="server" CssClass="film-poster" ImageUrl='<%# GetImageUrl(Eval("KodeFilm")) %>' />
                        <div class="film-info">
                            <h3><%# Eval("NamaFilm") %></h3>
                            <p><%# Eval("DeskripsiFilm") %></p>
                            <p>Tanggal Tayang: <%# Eval("TanggalTayang", "{0:dd MMMM yyyy}") %></p>
                            <asp:Panel runat="server" Visible='<%# Eval("Status").ToString() == "N" %>' CssClass="watchlist-panel">
                            <asp:Button ID="btnWatchlist" runat="server" Text="Add to Watchlist" OnClick="btnWatchlist_Click" CommandArgument='<%# Eval("KodeFilm") %>' />
                         </asp:Panel>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <style>
        .film-container {
            display: flex;
            flex-wrap: wrap; /* Membungkus kartu film ke baris berikutnya */
            gap: 16px; /* Jarak antar kartu film */
            width: 150%;
            overflow-y: auto;
            height: 600px;
        }

        .film-card {
            border: 1px solid #ccc;
            padding: 16px;
            width: calc(33.333% - 32px); /* Atur lebar kartu film, mengurangi jarak antar kartu */
            background-color: #f9f9f9;
            box-sizing: border-box; /* Termasuk padding dan border dalam ukuran lebar */
            display: flex;
            flex-direction: column;
            margin-bottom: 16px;
        }

        .film-card-content {
            display: flex;
            flex-direction: row;
            margin-bottom: 16px;
        }

        .film-poster {
            width: 150px;
            height: auto;
            margin-right: 16px;
        }

        .film-info {
            flex: 1;
        }

        .watchlist-panel {
            margin-top: auto; /* Pushes the button to the bottom of the card */
        }

    </style>
</asp:Content>
