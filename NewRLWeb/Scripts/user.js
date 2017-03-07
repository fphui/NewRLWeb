
    $(function () {
        $("#Publicationtime").datepicker();
    });
var PArray = "";
var count = 1;
var coo = count;
var temp=null;//存储本来图片的地址
function showPreview(source) {
    if (!isPicFile(source)) {
        alert("您选择的不是一个图片！");
        source.outerHTML = source.outerHTML;//清空file的value
        return false;
    }
}
function isPicFile(filename) {
    for (var countFile = 0; filename.files[countFile] != null; countFile++) {
        var file1 = filename.files[countFile];
        if (file1.type == "image/png" || file1.type == "image/jpeg") {
            temp = document.getElementById("HeadImage").src;
            document.getElementById("UserId").value = filename.files[0].name;
            document.getElementById("HeadImage").src = window.URL.createObjectURL(filename.files[0]);
        }
        else {
            return false;
        }
    }
    return true;
}
//function clickImage(Img) {
//    PArray = PArray + Img.id + "/";
//    //alert(PArray);
//    Img.style.display = 'none';
//    document.getElementById("HeadImage").src = temp;
//}
function ClickSub() {
    //    document.getElementById("form1").submit();
    // document.getElementById("UserId").value = document.getElementById("HeadImage").value;
    alert("jajaja");
    document.getElementById("form2").submit();
//    if(@TempData["user"]==1)
        document.getElementById("form0").submit();       
}
