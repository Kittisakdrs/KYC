<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_detail.aspx.cs" Inherits="Face_scan.User_detail" %>

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
        height: 650px;
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
<link href="Content/bootstrap.min.css" rel="stylesheet" />
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
                    Text="รายละเอียดผู้ใช้งาน"
                    Style="color: #ff009c; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 150px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                </asp:Label>
                <br />
                <br />
                <br />
                <div class="mid_box">
                    <asp:Label ID="Label2"
                        runat="server"
                        Text="กรุณาตรวจสอบความถูกต้องชื่อ-นามสกุล"
                        Style="color: #2c64ff; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 90px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                    </asp:Label>
                    <br />
                    <div align="left" style="width: 1100px;">
                        <asp:Label ID="Label3" Style="color: #2c64ff; font-weight: bold; font-weight: 900; font-size: 75px;" runat="server" Text="HN : "></asp:Label>
                        <asp:Label ID="lb_hn"
                            runat="server" Style="color: black; font-weight: bold; font-weight: 900; font-size: 75px;" Text="-"></asp:Label>
                        <br />
                        <asp:Label ID="Label5" Style="color: #2c64ff; font-weight: bold; font-weight: 900; font-size: 75px;" runat="server" Text="หมายเลขบัตรประชาชน : "></asp:Label>
                        <asp:Label ID="lb_id_card" Style="color: black; font-weight: bold; font-weight: 900; font-size: 75px;" runat="server" Text="-"></asp:Label>
                        <br />
                        <asp:Label ID="Label7" Style="color: #2c64ff; font-weight: bold; font-weight: 900; font-size: 75px;" runat="server" Text="ชื่อ-นามสกุล : "></asp:Label>
                        <asp:Label ID="lb_name" Style="color: black; font-weight: bold; font-weight: 900; font-size: 75px;" runat="server" Text="-"></asp:Label>
                        <br />
                        <asp:Label ID="Label9" Style="color: #2c64ff; font-weight: bold; font-weight: 900; font-size: 75px;" runat="server" Text="สิทธิ : "></asp:Label>
                        <asp:Label ID="lb_rights" Style="color: black; font-weight: bold; font-weight: 900; font-size: 70px;" runat="server" Text="-"></asp:Label>
                    </div>
                    <br />
                
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="ImageButton1" ImageUrl="img/button_ok.png" runat="server" OnClick="ImageButton1_Click" />
                    <asp:ImageButton ID="ImageButton2" ImageUrl="img/button_no.png" runat="server" OnClick="ImageButton2_Click" />
                </div>
            </div>
            <div class="col-sm-2 ">
            </div>


        </div>
    </form>
</body>
</html>
