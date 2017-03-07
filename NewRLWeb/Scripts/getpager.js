﻿function getParameter(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function GetArticlesData(pageIndex , action) {
    var ajaxUrl = '/'+action+'/AjaxPaging?pageIndex=' + pageIndex;
    var ajaxType = 'post';
    var ajaxDataType = 'text';
    var sucFun = function (data, status) {
        if (data == null && status != "success") {
            alert("获取数据失败！");
            return false;
        }
        else {
            $("#result").html(data);
            //定义分页样式
            var totalCount = parseInt('@ViewBag.TotalCount');
            var pageSize = parseInt('@ViewBag.PageSize');
            var pageNo = getParameter('pno');//该参数为插件自带，不设置好，调用数据的时候当前页码会一直显示在第一页
            if (!pageNo) {
                pageNo = 1;
            }
            var totalPages = totalCount % pageSize == 0 ? totalCount / pageSize : (parseInt(totalCount / pageSize) + 1);
            kkpager.generPageHtml({
                pno: pageNo,
                total: totalPages,
                totalRecords: totalCount,
                mode: 'click',
                click: function (n) {
                    this.selectPage(n);//插件自带的方法，手动调用某一页码
                    searchPage(n,action);
                    return false;
                }
            });
        }
    };
    //ajax参数设置
    var Option =
                {
                    url: ajaxUrl,
                    type: ajaxType,
                    dataType: ajaxDataType,
                    cache: false, //设置为 false 将不会从浏览器缓存中加载请求信息。
                    async: true, //(默认: true)，所有请求均为异步请求。发送同步请求，请将此选项设置为 false。同步请求将锁住浏览器，用户其它操作必须等待请求完成才可以执行。
                    timeout: 3600, //设置请求超时时间（毫秒）。此设置将覆盖全局设置。
                    error: function () { },
                    success: sucFun,
                    beforeSend: function () { }
                };
    $.ajax(Option);
    return false;
}
//ajax翻页
function searchPage(n,action) {
    GetArticlesData(n,action);
}