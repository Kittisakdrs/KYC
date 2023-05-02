<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectUserType.aspx.cs" Inherits="LoxleyOrbit.FaceScan.Web.SelectUserType" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<!DOCTYPE html>
<style>
    .bg_chula {
        background-image: url(img/screenbg1366.png);
        background-size:1920px 1080px;
        background-repeat: no-repeat, repeat;
        position: relative;
        height: 1080px;
        width: 1920px;
        margin: 0px;
    }

    .color_3btn {
        background-image: linear-gradient(white, #5099FF );
        font-family: 'Angsana New';
        cursor: pointer;
        border-radius: 20px;
        color:black ;
        
    }
</style>


<link href="Scripts/bootstrap.min.css" rel="stylesheet" />
<script src="Scripts/jquery-3.4.1.min.js"></script>
<script src="Scripts/bootstrap.min.js"></script>

<script type="text/javascript">

    $(document).ready(function () {
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(to_complete);
        window.external.StartTimer();
        //to_complete("?id=1570800044928&com_name=&hn=123456");
    });

    function open_progress() {
        try {
            $('#progress').modal({
                keyboard: false
            });

            setTimeout(function () { close_popup(); }, 1 * 60 * 1000);
        } catch (Error) {
            alert(Error.message);
            show_alert();
        }
    }

    function close_popup() {

        $('#progress').modal('hide');
        //if (querySelector != undefined) {
        //    $(querySelector).delay(8).dialog('close');
        //} else {
        //    $('.ui-dialog-content').delay(8).dialog('close');
        //}
    }

    function redirect_to_User_Hn()
    {
        window.location.href = 'User_Hn.aspx';
    }

    function show_alert(data) {

        $("#pn_popup").dialog({

            modal: true,
            width: 700,
            height: 430,
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
        return false;
    }

    function to_complete(param) {
        debugger;
        var hn = getParamValue("hn", param);
        var id = getParamValue("id", param);
        //alert(id.toString());

        $.ajax
            ({
                url: "SelectUserType.aspx/RegisterUser",
                data: "{ 'hn': '" + hn + "','id': '" + id + "' }",
                dataType: "json",
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) {
                    return data;
                },
                success: function (data) {
                    if (data.d.StatusCode != -1) {
                        window.location.href = 'User_detail.aspx';
                    } else {
                        window.location.href = 'User_Hn.aspx';
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });

        //$.ajax({
        //    type: "POST",
        //    url: "SelectUserType.aspx/RegisterUser",
        //    //data: "{'hn':'" + $hn + "', 'id':'" + $id + "' }", //Pass the parameter names and values
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    async: false,
        //    error: function (jqXHR, textStatus, errorThrown) {
        //        alert("Error- Status: " + textStatus + " jqXHR Status: " + jqXHR.status + " jqXHR Response Text:" + jqXHR.responseText)
        //    },
        //    success: function (msg) {
        //        if (msg.d == true) {
        //            window.location.href = 'User_detail.aspx' + param + '&requiredhn=' + hn;
        //        }
        //        else {
        //            //show error
        //            alert('login failed');
        //        }
        //    }
        //});

        //PageMethods.RegisterUser(hn, id, onSucess, onError);

        //function onSucess(result) {

        //}

        //function onError(result) {
        //    alert('Cannot process your request at the moment, please try later.');
        //}

        //if (hn != undefined) {
        //    window.location.href = 'User_detail.aspx' + param + '&requiredhn=' + hn;
        //}
        //else if (id != undefined) {
        //    window.location.href = 'User_detail.aspx' + param + '&requiredid=' + id;
        //} else {
        //    window.location.href = 'NotSuccess.aspx?com_name=' + getParamValue("com_name");
        //}
    }

    function to_notsuccess() {
        window.location.href = 'NotSuccess.aspx?com_name=' + getParamValue("com_name");
    }

    function getParamValue(paramName, param) {
        var startindex = param.indexOf(paramName + "=") + paramName.length + 1;
        if (startindex != -1) {
            var tmpStr = param.substring(startindex);
            var endindex = tmpStr.indexOf("&");
            var returnVal;
            if (endindex != -1) {
                returnVal = tmpStr.substring(0, endindex);
            } else {
                returnVal = tmpStr.substring(0);
            }
            return returnVal;
        } else {
            return undefined;
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
        <div class="row" style="width: 1920px; height: 200px;">
            <div class="col-md-1" align="center">
            </div>
            <div class="col-md-10" align="center">
                <br />
                <asp:Label ID="Label1"
                    runat="server"
                    Text="ผู้ใช้งานทั่วไป"
                    Style="color: #ff009c; vertical-align: text-bottom; font-weight: bold; font-weight: 900; font-size: 150px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                </asp:Label>
            </div>
            <div class="col-md-1" align="center">
            </div>
        </div>
        <div class="row" style="width:  1920px; height: 400px;">
          
            <div class="col-md-12" align="center">
                <asp:Image ID="Image1"
                    runat="server"
                    ImageUrl="~/img/SelectUserType.PNG"
                    Style="border-color: white; border-width: 5px; border-style: solid; height: 550px; width: 800px; border-radius: 50px;" />
                <br />
                <asp:Label ID="Label3"
                    runat="server"
                    Text="กรุณาเสียบบัตรประชาชน"
                    Style="color: black; vertical-align: text-bottom; font-weight: bold; font-weight: 700; font-size: 80px; text-shadow: 1px 1px gray; font-family: 'Angsana New';">
                </asp:Label>

            </div>
          
        </div>


        <table style="width:  1920px; height: 170px;">
            <tr>
                <th style="width: 250px"></th>
                <th style="width: 200px ;padding-right: 30px;">
                    <asp:Button ID="btn_child"
                        Height="160px"
                        Width="100%"
                        runat="server"
                        Text="สำหรับเด็ก"
                        Font-Size="80px"
                        Font-Bold="true"
                        class="color_3btn" OnClick="btn_child_Click" />
                </th>
                <th style="width: 300px; padding-right: 30px;">
                    <asp:Button ID="btn_foreign"
                        Height="160px"
                        Style="line-height: 60px; "
                         Width="100%"
                        runat="server"
                        Font-Size="70px"
                        Font-Bold="true"
                        Text="สำหรับต่างชาติ 
/Foreigner"
                        class="color_3btn" OnClick="btn_foreign_Click" />
                </th>
                <th style="width: 470px">
                    <asp:Button ID="btn_none_card"
                        Style="line-height: 60px"
                        Height="160px"
                         Width="100%"
                        runat="server"
                       
                        Font-Bold="true"
                        Font-Size="60px"
                        OnClick="btn_noncard_Click"
                        Text="ไม่ได้นำบัตรประชาชนมา
/บัตรอ่านไม่ได้(ผ่านการยืนยันตัวแล้ว)"
                        class="color_3btn" /></th>
                <th style="width: 250px"></th>
            </tr>
        </table>
     




        <div id="progress" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <%--                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="gridSystemModalLabel">Modal title</h4>
                    </div>--%>
                    <div class="modal-body">
                    </div>
                    <div class="text-progress">กำลังอ่านข้อมูลจากบัตรประชาชน</div>
                    <div>
                        <img src="img/loading.gif" alt="" />
                    </div>
                    <%--                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary">Save changes</button>
                    </div>--%>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
    </form>
</body>
</html>
