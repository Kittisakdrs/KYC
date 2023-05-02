<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Hn.aspx.cs" Inherits="LoxleyOrbit.FaceScan.Web.User_Hn" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<!DOCTYPE html>

<link href="Scripts/bootstrap.min.css" rel="stylesheet" />
<script type="text/javascript" src="Scripts/jquery-3.4.1.min.js"></script>
<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>

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

    .myButton {
        background: url(./images/but.png) no-repeat;
        cursor: pointer;
        border: none;
        width: 200px;
        height: 200px;
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
        height: 660px;
        width: 600px;
    }

    .num_button {
        height: 160px;
        width: 190px;
    }
</style>
<script type="text/javascript">
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

    // Set timeout variables.
    var timeoutNow = 120000;
    //var timeoutNow = 3000;
    var timeoutTimer;
    //const queryString = window.location.search;


    $(document).ready(function () {
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(submit);
        // window.external.StopTimer();

        $('#progress').on('shown.bs.modal', function (e) {
            RegisterUser();
        })

        debugger;
        //submit();
    });


    for (var i = 0; i < 10; i++) {
        var posAt = Math.floor(Math.random() * ctnt.length);

        charList[i] = ctnt[posAt];
        ctnt.splice(posAt, 1);
    }

    function StartTimers() {

        //timeoutTimer = setTimeout('gotoPage("OpenVideo.aspx' + queryString + '")', timeoutNow);
    }

    function resetTimeOut() {
        clearTimeout(timeoutTimer);
        StartTimers();
    }

    function Redirect() {
        window.location.href = 'User_detail.aspx';
    }

    function check_language() {
        if (lan == 'en') {

            $('#tb_input').addClass('hn_input');
            //document.getElementById('tb_input').className = 'hn_input';

            titleInput.innerHTML = 'Please Fill In Hospital Number.';

            txtInput.innerHTML = 'HN : ';

            var bDel = document.getElementById('<%=btn_Delete.ClientID%>');
            bDel.innerHTML = 'Del';
            bDel.src = '../img/login/button_Delete_en.png';
            bDel.onmousedown = function () { this.src = '../img/login/button_Delete_s_over_en.png'; };
            bDel.onmouseup = function () { this.src = '../img/login/button_Delete_en.png'; };
            bDel.onmouseout = function () { this.src = '../img/login/button_Delete_en.png'; };

            var bBack = document.getElementById('<%=btn_back.ClientID%>');
            bBack.innerHTML = 'Back';
            var bConfirm = document.getElementById('<%=btn_login.ClientID%>');
            bConfirm.value = 'Confirm';
            var bCancel = document.getElementById('<%=btn_cancel.ClientID%>');
            bCancel.value = 'Cancel';
        }
    }

    function gotoPage(gotoPage) {
        window.location.href = gotoPage;
    }

    function check_input() {
        var hn = document.getElementById('<%=txt_hn.ClientID%>').value;
        var id = document.getElementById('<%=txt_id_card.ClientID%>').value;
    }

    function checkID(id) {
        if (id.length < 13) return false;
        for (i = 0, sum = 0; i < 12; i++)
            sum += parseFloat(id.charAt(i)) * (13 - i); if ((11 - sum % 11) % 10 != parseFloat(id.charAt(12)))
            return false; return true;
    }

    function show_alert() {

        $("#pn_popup").dialog({

            modal: true,
            width: '50%',
            height: 'auto',
            resizable: false,
            appendTo: 'form',
            resizable: false,
            hide: { effect: 'fadeOut', duration: 10 },
            open: function (event, ui) {

                $(this).parent().css("left", ($(window).width() - $(this).outerWidth()) / 2 + 'px');
                $(this).parent().css("top", ($(window).height() - $(this).outerHeight()) / 2 + 'px');
                $('.ui-dialog-titlebar').hide();

            }

        });

        $("#pn_popup").dialog('open');

        check_language();

        return false;
    }


    function close_progress() {

        window.parent.$('.ui-dialog-content:visible').dialog('close');
    }

    function check_idcard(idcard) {
        if (idcard.value == "") { return false; }
        if (idcard.length < 13) { return false; }

        var num = str_split(idcard); // function เพิ่มเติม
        var sum = 0;
        var total = 0;
        var digi = 13;

        for (i = 0; i < 12; i++) {
            sum = sum + (num[i] * digi);
            digi--;
        }
        total = ((11 - (sum % 11)) % 10);

        if (total == num[12]) { //	alert('รหัสหมายเลขประจำตัวประชาชนถูกต้อง');
            return true;
        } else { //	alert('รหัสหมายเลขประจำตัวประชาชนไม่ถูกต้อง');
            return false;
        }
    }
    function str_split(f_string, f_split_length) {
        f_string += '';
        if (f_split_length == undefined) {
            f_split_length = 1;
        }
        if (f_split_length > 0) {
            var result = [];
            while (f_string.length > f_split_length) {
                result[result.length] = f_string.substring(0, f_split_length);
                f_string = f_string.substring(f_split_length);
            }
            result[result.length] = f_string;
            return result;
        }
        return false;
    }

    function rkey() {
        line = "";

        //document.forms[0].txt_hn.value = line;
        //document.forms[0].txt_id_card.value = line;

        document.getElementById('<%= txt_id_card.ClientID %>').value = "";

        document.getElementById('<%= txt_hn.ClientID %>').disabled = false;
        document.getElementById('<%= txt_id_card.ClientID %>').disabled = false;

        if (selectedField == "2") {
            document.getElementById('<%=txt_id_card.ClientID%>').focus();
        }
    }
    function del() {

        var txtArea;

        if (selectedField == "2") {
            txtArea = document.getElementById('<%=txt_id_card.ClientID%>');
        } else {
            txtArea = document.getElementById('txt_hn');
        }

        var str = txtArea.value;

        var pos = caretPos();

        if (pos > 0) {
            txtArea.value = str.slice(0, pos - 1) + str.slice(pos, str.length);
            setCaretPosition(pos - 1);
        }

        document.getElementById('<%=txt_id_card.ClientID%>').focus();


    }
    function RemoveHN() {
        document.getElementById('txt_hn').value = '';
    }
    function RemoveID() {
        document.getElementById('txt_id_card').value = '';
    }
    function setCaretPosition(caretPos) {
        var elem;
        if (selectedField == "2") {
            elem = document.getElementById('txt_id_card');
        } else {
            elem = document.getElementById('txt_hn');
        }
        if (elem != null) {
            if (elem.createTextRange) {
                var range = elem.createTextRange();
                range.move('character', caretPos);
                range.select();
            }
            else {
                if (elem.selectionStart) {
                    elem.focus();
                    elem.setSelectionRange(caretPos, caretPos);
                }
                else
                    elem.focus();
            }
        }
    }
    function caretPos() {
        var el;
        if (selectedField == "2") {
            el = document.getElementById('<%=txt_id_card.ClientID%>');
        } else {
            el = document.getElementById('txt_hn');
        }

        var pos = 0;
        // IE Support
        if (document.selection) {
            el.focus();
            var Sel = document.selection.createRange();
            var SelLength = document.selection.createRange().text.length;
            Sel.moveStart('character', -el.value.length);
            pos = Sel.text.length - SelLength;
        }
        // Firefox support
        else if (el.selectionStart || el.selectionStart == '0')
            pos = el.selectionStart;

        return pos;

    }
    function insertAtCaretLast(textValue) {
        out = "";
        var txtArea;
        if (selectedField == "2") {
            if (textValue == "/") {
                return false;
            }
            else {
                txtArea = document.getElementById('txt_id_card');
            }
        } else {
            txtArea = document.getElementById('txt_hn');
        }
        if (document.selection) {
            txtArea.focus();
            var range = txtArea.createTextRange();
            range.collapse(false);
            range.select();
            var sel = document.selection.createRange();
            sel.text = textValue;

            return;
        }
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
    function rqCard() {

        $(document).keydown(function (e) {

            if (e.keyCode == "16") {
            }

            // document.getElementById('txt_hn').value = "";
            // document.getElementById('txt_id_card').value = "";

            x_str += ":" + e.keyCode;
            if (e.keyCode == "13") {
                var str_sp = x_str.split(":");
                var tmp = "";
                var str_res = "";
                for (i = 0; i < str_sp.length - 2; i++) {
                    if (str_sp[i + 1] != "191") {
                        tmp = parseInt(str_sp[i + 1]);
                        str_res += String.fromCharCode(tmp);
                    }
                }


                if (str_res.length < 8) {
                    str_res = 0 + str_res;
                }

                document.getElementById('txt_hn').value = str_res;
                document.getElementById('txt_id_card').value = "";
                x_str = "";

                document.getElementById("btn_login").click();

                return false;
            }

        });
    }
    function outform() {
        out = "1";
        return false;
    }

    function GetPrinterStatusResponse(printerStatus, comname) {
        //alert(printerStatus);

        if (printerStatus) {

            if (printerStatus == 'PAPER_NEAR_END') {
                $('#paperNearEnd').show();
                //alert(printerStatus);
            }
            else if (printerStatus == 'PAPER_END') {
                $('#paperEnd').show();
            }
            else if (printerStatus == 'PRINTER_ERROR') {
                $('#printerError').show();
            }
            $.ajax({
                type: "POST",
                url: "login.aspx/CheckPrinterStatus",
                data: "{'printerStatus':'" + printerStatus + "','comname':'" + comname + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });


        }
    }
    function onInsertIdCard(id, name, p_name, l_name, f_name, e_p_name, e_l_name, e_f_name, e_m_name, BirthDate, Sex
        , Address, Moo, Soi, Thanon, Tumbol, Amphur, Province, ExpireDate, IssueDate) {

        var data = "{";
        data += "'id': '" + id + "', ";
        data += "'name': '" + name + "', ";
        data += "'p_name': '" + p_name + "', ";
        data += "'l_name': '" + l_name + "', ";
        data += "'f_name': '" + f_name + "', ";
        data += "'e_p_name': '" + e_p_name + "', ";
        data += "'e_l_name': '" + e_l_name + "', ";
        data += "'e_f_name': '" + e_f_name + "', ";
        data += "'e_m_name': '" + e_m_name + "', ";
        data += "'BirthDate': '" + BirthDate + "', ";
        data += "'Sex': '" + Sex + "', ";
        data += "'Address': '" + Address + "', ";
        data += "'Moo': '" + Moo + "', ";
        data += "'Soi': '" + Soi + "', ";
        data += "'Thanon': '" + Thanon + "', ";
        data += "'Tumbol': '" + Tumbol + "', ";
        data += "'Amphur': '" + Amphur + "', ";
        data += "'Province': '" + Province + "', ";
        data += "'ExpireDate': '" + ExpireDate + "', ";
        data += "'IssueDate': '" + IssueDate + "'";
        data += "}";

        document.getElementById('idjson').value = data;
        document.getElementById('txt_id_card').value = id;
        document.getElementById('btn_login').click();
    }

    function open_progress() {
        try {
            $('#progress').modal({
                keyboard: false
            })

            setTimeout(function () { close_popup('#progress'); }, 1 * 60 * 1000);
        } catch (Error) {
            alert(Error.message);
            show_alert();
        }
    }

    function close_popup() {

        $('#progress').modal('hide');
    }

    function show_alert(data) {

        $('#pn_popup').modal('show');
        return false;
    }

    function close_message_popup() {
        $('#popup_message').modal('hide');
    }



    function RegisterUser() {
        debugger;
        //open_progress();
        var hn = document.getElementById('txt_hn').value;
        $.ajax
            ({
                url: "User_Hn.aspx/RegisterUser",
                data: "{ 'hn': '" + hn + "','id': '" + '' + "' }",
                dataType: "json",
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) {
                    return data;
                },
                success: function (data) {
                    debugger;
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
</script>
<link href="Content/bootstrap.min.css" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
</head>
<body class="bg_chula">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div class="row">
            <div class="col-sm-2 ">
            </div>
            <div class="col-sm-8 " align="center">
                <br />
                <br />
                <asp:Label ID="Label1"
                    runat="server"
                    Text="เด็ก"
                    Style="color: #ff009c; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 150px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                </asp:Label>
                <br />
                <br />
                

            </div>
            <div class="col-sm-2 ">
            </div>


        </div>

        <div class="row">
            <div class="col-sm-1">
            </div>
            <div class="col-sm-5 " align="center">
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
                <br />
                <asp:Label ID="Label2"
                    runat="server"
                    Text="กรอก HN. / Enter HN. "
                    Style="color: black; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 90px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                </asp:Label>
                <br />
                <br />
                <br />
                <br />
                <br />
                <table style="width: 500px">
                    <tr>
                        <th>
                            <asp:Label ID="Label3"
                                runat="server"
                                Text=" HN :"
                                Style="color: black; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 90px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                            </asp:Label>
                        </th>
                        <th>
                            <asp:TextBox ID="txt_hn" runat="server" Style="font-size: 60px; font-weight: 700; font-family: 'Angsana New'; border-color: black; border-radius: 10px; height: 120px; width: 350px"></asp:TextBox>
                        </th>
                    </tr>

                </table>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <%--<asp:ImageButton ID="btn_login" runat="server" ImageUrl="~/img/button_ok.png" Style="width: 250px" OnClick="btn_login_Click" />--%>
                <asp:ImageButton ID="btn_login" runat="server" ImageUrl="~/img/button_ok.png" Style="width: 370px" OnClientClick="open_progress();return false;" />
                <%--<asp:LinkButton ID="btn_login" runat="server" OnClientClick="RegisterUser();">LinkButton</asp:LinkButton>--%>
                <asp:ImageButton ID="btn_cancel" runat="server" ImageUrl="~/img/button_no.png" Style="width: 370px" OnClick="btn_cancel_Click" />
              
               



            </div>
            <div class="col-sm-1 ">
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
                    <div class="modal-content" style="width: 1000px; height: 700px;">
                        <div class="modal-body" style="position: relative; overflow: hidden; padding: 0px">
                            <img src="img/loading.gif" alt="" style="height: 700px; width: 1000px; padding-left: 0px;" />
                        </div>
                        <%--<div class="text-progress">กำลังอ่านข้อมูลจากบัตรประชาชน</div>--%>
                        <div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="popup_message" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel" align="center">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="width: 1000px; height: 700px;">
                        <div class="modal-body" style="position: relative; overflow: hidden; padding: 0px">

                            <%--<div class="text-progress">กรุณาระบุ HN หรือ หมายเลยที่บัตรประจำตัวประชาชน</div>--%>
                            <%--  <div class="pic_bg">
                                <img src="img/balloon_modal popup_size big_v2.png" alt=""
                                    style="width: 100%; height: 100%" />

                                <pic_bg bt>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/button_ok.png"
                                        OnClientClick="close_message_popup();return false;" />
                                </pic_bg>

                            </div>--%>

                            <img src="img/balloon_modal popup_size big_v2.png" alt="" style="height: 700px; width: 1000px; padding-left: 0px;" />
                            <div style="position: absolute; top: 550px; left: 350px; width: 200px; height: 25px">
                                <center>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/button_ok.png"
                                        OnClientClick="close_message_popup();return false;" /></center>
                            </div>




                        </div>
                    </div>
                </div>
            </div>




        </div>
        <div class="row">
                <div class="col-sm-10">
                </div>
                <div class="col-sm-2" align="right">
                 
                        <asp:ImageButton ID="btn_back" runat="server" ImageUrl="~/img/button_previous.png" Style="width: 300px; vertical-align: bottom" OnClick="btn_back_Click" />
                   
                </div>
            </div>
    </form>
</body>
</html>

