<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_detail_Eng.aspx.cs" Inherits="Face_scan.User_detail_Eng" %>

<!DOCTYPE html>
<style>
    .bg_chula {
        background-image: url(img/screenbg1366.png);
        background-size: 1920px 1080px;
        background-repeat: no-repeat, repeat;
        position: relative;
        height: 1080px;
        width: 1920px;
        margin: 0px;
    }

    .pic_bg {
        position: relative;
        overflow: hidden;
    }

        .pic_bg bt {
            position: absolute;
            text-align: center;
            width: 150px;
        }

    .color_3btn {
        background-image: linear-gradient(white, #5099FF );
        color: aliceblue;
        font-family: 'Angsana New';
        cursor: pointer;
        border-radius: 10px;
    }

    .mid_box {
        background-color: azure;
        border-color: black;
        font-family: 'Angsana New';
        cursor: pointer;
        border-radius: 10px;
         height: 560px;
        width: 1100px;
    }
</style>

<script type="text/javascript">

    $(document).ready(function () {
        window.external.StopTimer();
    });

    function LaunchCamera(hn, base64) {
        return window.external.LaunchCamera(hn, base64);
    }
    //function LaunchCamera(hn) {
    //    return window.external.LaunchCamera(hn);
    //}
    </script>
<link href="css/bootstrap.min.css" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body class="bg_chula">
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-sm-2 ">
            </div>
            <div class="col-sm-8 " align="center">
                <br />
                <br />
                <asp:Label ID="Label1"
                    runat="server"
                    Text="ต่างชาติ/Foreigner"
                    Style="color: #ff009c; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 150px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                </asp:Label>
                <br />
                <br />
                <br />
                <br />
                <div class="mid_box">
                    <br />
                    <asp:Label ID="Label2"
                        runat="server"
                        Text="Please verify your personal information "
                        Style="color: #2c64ff; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 90px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                    </asp:Label>
                    <br />
                    <br />
                    <div align="left" style="width: 800px;">
                        <asp:Label ID="Label3" Style="color: #2c64ff; font-weight: bold; font-weight: 900; font-size: 90px;" runat="server" Text="HN : "></asp:Label>
                        <asp:Label ID="lb_hn"
                            runat="server" Style="color: black; font-weight: bold; font-weight: 900; font-size: 60px;" Text="-"></asp:Label>
                        <br />

                        <asp:Label ID="Label7" Style="color: #2c64ff; font-weight: bold; font-weight: 900; font-size: 90px;" runat="server" Text="NAME : "></asp:Label>
                        <asp:Label ID="lb_name" Style="color: black; font-weight: bold; font-weight: 900; font-size: 60px;" runat="server" Text="-"></asp:Label>

                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                             <br />
                        <br />
                        <br />
                        <asp:ImageButton ID="ImageButton1" ImageUrl="img/button_ok.png"  Style="width: 370px" runat="server" OnClick="ImageButton1_Click" />
                        <asp:ImageButton ID="ImageButton2" ImageUrl="img/button_no.png"  Style="width: 370px" runat="server" OnClick="ImageButton2_Click" />
                    </div>
                </div>
            </div>
            <div class="col-sm-2 ">
            </div>
        </div>
    </form>
</body>
</html>
