<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Hn2.aspx.cs" Inherits="LoxleyOrbit.FaceScan.Web.User_Hn2" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<!DOCTYPE html>
<style>
    .bg_chula {
        background-image: url(img/screenbg1366.png);
        background-repeat: no-repeat, repeat;
        position: relative;
        height: 768px;
        width: 1366px;
        margin: 0px;
    }

    .myButton {
        background: url(./images/but.png) no-repeat;
        cursor: pointer;
        border: none;
        width: 100px;
        height: 100px;
    }

        .myButton:active /* use Dot here */ {
            background: url(./images/but2.png) no-repeat;
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
        height: 470px;
        width: 500px;
    }

    .num_button {
        height: 110px;
        width: 130px;
    }
</style>


<link href="Scripts/bootstrap.min.css" rel="stylesheet" />
<script src="Scripts/jquery-3.4.1.min.js"></script>
<script src="Scripts/bootstrap.min.js"></script>

<script type="text/javascript">

    $(document).ready(function () {
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(to_complete);
        alert('ff');
    });


    var line = "";
    var lock = 1;
    var uca = 0;
    var initial = 0;
    var selectedField = "";
    var selectedField2 = "";
    var ctnt = new Array();
    var charList = new Array();
    var out = "";
    ctnt[0] = "1";
    ctnt[1] = "2";
    ctnt[2] = "3";
    ctnt[3] = "4";
    ctnt[4] = "5";
    ctnt[5] = "6";
    ctnt[6] = "7";
    ctnt[7] = "8";
    ctnt[8] = "9";
    ctnt[9] = "0";
    ctnt[10] = "/";

    var timeoutNow = 120000;

    function RegisterUser() {

        open_progress();
        var hn = document.getElementById('txt_hn').value;
        $.ajax
            ({
                url: "User_Hn2.aspx/RegisterUser",
                data: "{ 'hn': '" + hn + "','id': '" + '' + "' }",
                dataType: "json",
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) {
                    return data;
                },
                success: function (data) {
                    if (data != null)
                        if (data.d.StatusCode != -1) {
                            window.location.href = 'User_detail.aspx';
                        } else {
                            close_popup();
                            try {
                                $('#popup_message').modal({
                                    keyboard: false
                                })

                                setTimeout(function () { close_popup('#popup_message'); }, 1 * 60 * 1000);
                            } catch (Error) {
                                alert(Error.message);
                                show_alert();
                            }
                        }
                    else {
                        close_popup();
                        try {
                            $('#popup_message').modal({
                                keyboard: false
                            })

                            setTimeout(function () { close_popup('#popup_message'); }, 1 * 60 * 1000);
                        } catch (Error) {
                            alert(Error.message);
                            show_alert();
                        }
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
    }

    function insertAtCaret(textValue) {
        //Get textArea HTML control

        resetTimeOut();


        var txtArea;
        if (selectedField == "2") {
            if (textValue == "/" && lan != 'en') {
                return false;
            }
            else {
                txtArea = document.getElementById('<%=txt_id_card.ClientID%>');
            }
        } else {
            txtArea = document.getElementById('txt_hn');
        }


        //IE
        if (document.selection) {
            if (out == "1") {
                setCaretPosition(txtArea.value.length);

                var sel = document.selection.createRange();
                txtArea.selectionEnd = textValue.length;

                sel.text = textValue;
                out = "";
            } else {
                txtArea.focus();
                var sel = document.selection.createRange();
                txtArea.selectionEnd = textValue.length;

                sel.text = textValue;
            }
            return;
        }

        //Firefox, chrome, mozilla
        else if (txtArea.selectionStart || txtArea.selectionStart == '0') {

            var startPos = txtArea.selectionStart;
            var endPos = txtArea.selectionEnd;
            var scrollTop = txtArea.scrollTop;
            txtArea.value = txtArea.value.substring(0, startPos) + textValue + txtArea.value.substring(endPos, txtArea.value.length);
            txtArea.focus();
            txtArea.selectionStart = startPos + textValue.length;
            txtArea.selectionEnd = startPos + textValue.length;
        }
        else {

            txtArea.value += textArea.value;
            txtArea.focus();
        }
    }

    </script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function StopTimer() {
            return window.external.StopTimer();
        }
    </script>
</head>
<body class="bg_chula">
    <form id="form1" runat="server">
<%--        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>--%>

        <div class="row">
            <div class="col-sm-2 ">
            </div>
            <div class="col-sm-8 " align="center">
                <br />
                <br />
                <asp:Label ID="Label1"
                    runat="server"
                    Text="เด็ก"
                    Style="color: #ff009c; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 100px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                </asp:Label>
                <br />
                <br />
                <br />

            </div>
            <div class="col-sm-2 ">
            </div>
        </div>

        <div class="row">
            <div class="col-sm-1 ">
            </div>
            <div class="col-sm-4 " align="center">
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/img/button_1.png" CssClass="num_button" OnClientClick="insertAtCaret('1');return false;" />
                <asp:ImageButton ID="Image2" runat="server" ImageUrl="~/img/button_2.png" CssClass="num_button" OnClientClick="insertAtCaret('2');return false;" />
                <asp:ImageButton ID="Image3" runat="server" ImageUrl="~/img/button_3.png" CssClass="num_button" OnClientClick="insertAtCaret('3');return false;" />
                <br />
                <asp:ImageButton ID="Image4" runat="server" ImageUrl="~/img/button_4.png" CssClass="num_button" OnClientClick="insertAtCaret('4');return false;" />
                <asp:ImageButton ID="Image5" runat="server" ImageUrl="~/img/button_5.png" CssClass="num_button" OnClientClick="insertAtCaret('5');return false;" />
                <asp:ImageButton ID="Image6" runat="server" ImageUrl="~/img/button_6.png" CssClass="num_button" OnClientClick="insertAtCaret('6');return false;" />
                <br />
                <asp:ImageButton ID="Image7" runat="server" ImageUrl="~/img/button_7.png" CssClass="num_button" OnClientClick="insertAtCaret('7');return false;" />
                <asp:ImageButton ID="Image8" runat="server" ImageUrl="~/img/button_8.png" CssClass="num_button" OnClientClick="insertAtCaret('8');return false;" />
                <asp:ImageButton ID="Image9" runat="server" ImageUrl="~/img/button_9.png" CssClass="num_button" OnClientClick="insertAtCaret('9');return false;" />
                <br />
                <asp:ImageButton ID="button_slash" runat="server" ImageUrl="~/img/button_slash.png" CssClass="num_button" OnClientClick="insertAtCaret('/');return false;" />
                <asp:ImageButton ID="Image0" runat="server" ImageUrl="~/img/button_0.png" CssClass="num_button" OnClientClick="insertAtCaret('0');return false;" />
                <asp:ImageButton ID="btn_Delete" runat="server" ImageUrl="~/img/button_Delete.png" CssClass="num_button" OnClientClick="del();return false;" />
            </div>

            <div class="col-sm-5 mid_box" align="center">
                <asp:Label ID="Label2"
                    runat="server"
                    Text="กรอก HN. / Enter HN. "
                    Style="color: black; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 65px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                </asp:Label>
                <br />
                <br />
                <br />
                <table style="width: 500px">
                    <tr>
                        <th>
                            <asp:Label ID="Label3"
                                runat="server"
                                Text=" HN :"
                                Style="color: black; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 60px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                            </asp:Label>
                        </th>
                        <th>
                            <asp:TextBox ID="txt_hn" runat="server" Style="font-size: 60px; font-weight: 700; font-family: 'Angsana New'; border-color: black; border-radius: 10px; height: 90px; width: 350px"></asp:TextBox>
                        </th>
                    </tr>

                </table>

                <br />
                <br />
                <br />
                <br />
                <%--<asp:ImageButton ID="btn_login" runat="server" ImageUrl="~/img/button_ok.png" Style="width: 250px" OnClick="btn_login_Click" />--%>
                <asp:ImageButton ID="btn_login1" runat="server" ImageUrl="~/img/button_ok.png" Style="width: 250px" OnClientClick="RegisterUser();" />
                <%--<asp:LinkButton ID="btn_login" runat="server" OnClientClick="RegisterUser();">LinkButton</asp:LinkButton>--%>
                <asp:ImageButton ID="btn_cancel" runat="server" ImageUrl="~/img/button_no.png" Style="width: 250px" OnClick="btn_cancel_Click" />



            </div>
            <div class="col-sm-1 ">
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:ImageButton ID="btn_back" runat="server" ImageUrl="~/img/button_previous.png" Style="width: 200px; vertical-align: bottom" OnClick="btn_back_Click" />
            </div>



            <div style="display: none;">

                <asp:Label ID="Label4"
                    runat="server"
                    Text=" ID Card :"
                    Style="color: black; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 60px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                </asp:Label>

                <asp:TextBox ID="txt_id_card" runat="server" Style="font-size: 60px; font-weight: 700; font-family: 'Angsana New'; border-color: black; border-radius: 10px; height: 60px; width: 300px"></asp:TextBox>

            </div>

            <div id="progress" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-body">
                        </div>
                        <div class="text-progress">กำลังอ่านข้อมูลจากบัตรประชาชน</div>
                        <div>
                            <img src="img/loading.gif" alt="" />
                        </div>
                    </div>
                </div>
            </div>

            <div id="popup_message" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-body">
                        </div>
                        <div class="text-progress">กรุณาระบุ HN หรือ หมายเลยที่บัตรประจำตัวประชาชน</div>
                        <div>
                             <img src="img/balloon_modal popup_size big_v2.png" alt="" />
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/button_ok.png" Style="width: 250px" OnClientClick="close_message_popup();return false;" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>



