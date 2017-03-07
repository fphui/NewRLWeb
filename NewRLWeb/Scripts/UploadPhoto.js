
var PArray = "";
var count = 1;
var coo = count;
function showPreview(source) {
    if (!isPicFile(source)) {
        alert("您选择的文件中有非图片文件");
        source.outerHTML = source.outerHTML;//清空file的value
        return false;
    }
    source.style.display = 'none';
    var co;
    var cooo = count;
    var picSrc = "" + co + "" + "img";
    for (co = coo - 1; source.files[co] != null; co++) {
        var div = document.getElementById("divMain");
        var img1 = document.createElement("div");
        img1.style.width = "20%";
        img1.style.float = "left";
        img1.innerHTML = "<img display='' id='" + cooo + "'  style='width:100%;height:150px;border-color:black;border:2px;border-style:dashed;' onclick='clickImage(this)' />";
        divMain.appendChild(img1);
        var img = document.getElementById("" + cooo + "");
        img.src = window.URL.createObjectURL(source.files[co]);
        cooo++;
        count++;
    }
    var Picfile = document.createElement("div");
    Picfile.style.width = "20%";
    Picfile.style.float = "left";
    Picfile.innerHTML = "<div style='height150px; width:229px'><input type='file' id='" + picSrc + "' multiple='multiple' style='margin-left:40px; margin-top:30px' name='files' value='继续选择照片'  onchange='showPreview(this)' /></div>";
    divMain.appendChild(Picfile);
}
function isPicFile(filename) {
    for (var countFile = 0; filename.files[countFile] != null; countFile++) {
        var file1 = filename.files[countFile];
        if (file1.type == "image/jpeg" || file1.type == "image/png") {
        }
        else {
            return false;
        }
    }
    return true;
}
function clickImage(Img) {
    PArray = PArray + Img.id + "/";
    //    alert(PArray);

    Img.style.display = 'none';
}
function clickSub() {
    document.getElementById("txt1").value = PArray;
    //if (document.getElementById("AlbumName").value == "" || document.getElementById("AlbumType").value == "") {
    //    alert("请在每个输入框输入合法内容");
    //    return 0;
    //}
    //  var temp = document.getElementById("txt1").value;
    //  alert(PArray);
   // document.getElementById("txt2").value="@Model.UniqueID";
    var a = document.getElementById("txt2").value;
    //if (temp == 0) {
    //    CreateAlbum();
    //}
    document.getElementById("formMain").submit();
}
function remove() {
    var temp = document.getElementById("UploadBox");
    temp.parentNode.removeChild(temp);;
    document.body.style.backgroundColor = color;

}
var color;
function UploadPhoto() {
    color = document.body.style.backgroundColor;
    // var iframe = document.createElement('iframe');
    // // iframe.src = "http://www.jb51.net";
    // iframe.style.width = 400;
    // iframe.style.height = 300;
    // iframe.id = "233";
    // document.body.appendChild(iframe);
    // var button = document.createElement('input');
    // button.type = 'button';
    // button.value = "haha";
    // //  button.innerText = "content";
    //// iframe.innerHTML = "<input type='button;value='gaga'/>";
    // iframe.body.appendChild(button);
    // document.getElementById('2').innerHTML =  "<input type='button'value='gaga'/>";
    // alert("haha");

    //表单

    //上传图片框
    document.body.style.backgroundColor = '#d8d8dd';
    var div = document.createElement('div');
    div.id = "UploadBox";
    div.style.width = '70%';
    div.style.height = '70%';
    div.style.position = "absolute";
    div.style.top = '15%';
    div.style.left = '15%';
    div.style.backgroundColor = "white";
    document.body.appendChild(div);

    //var form = document.createElement("form");
    //form.id = "form1";
    //form.appendChild(div);
    //顶部区域
    var top = document.createElement('div');
    top.style.width = '100%';
    top.style.height = '10%';

    top.style.position = "absolute";
    div.appendChild(top);
    top.innerHTML = "<a href='#' onclick='remove();'style='width:5%;height:auto;left:95%;position:absolute;' ><img src='../Images/guanbi.jpg' style='  background-size:100%;  display:block;width:100%;height:100%;'/></a>";
    //图片显示区
    var div2 = document.createElement('div');
    div2.id = "divMain";
    div2.style.width = '100%';
    div2.style.height = '80%';
    div2.style.top = '10%';
    div2.style.position = "absolute";
    // div2.style.backgroundColor = "red";
    div.appendChild(div2);
    div2.innerHTML = "<input type='text' id='txt1'name='cancelImg' readonly='readonly' style='display:none;' />"
    div2.innerHTML = div2.innerHTML + "  <input type='text' name='UniqueID' id='txt2' readonly='readonly' style='display:none;' />";
    //  div2.innerHTML = " <input type='text' id='txt1' readonly='readonly' style='display:none;' />";
    // <input type="text" id="txt1" readonly="readonly" style="display:none;" />

    //var button1 = document.createElement("input");
    //button1.type = "file";
    //button1.multiplee = "multiple";
    //button1.required = "required";
    //button1.name = "files"
    //button1.onclick = "showPreview(this)";
    //div.appendChild(button1);

    //按钮框
    var div3 = document.createElement('div');
    //div2.id = "divMain";
    /*2017.2.25 BLM start*/
    div3.style.marginLeft = '10%';
    div3.style.marginRight = '10%';
    /*2017.2.25 BLM end*/
    div3.style.width = '100%';
    div3.style.height = 'auto';
    div3.style.top = "70%";
    div3.style.position = "absolute";
    div3.style.border = "1px";
    // div2.style.backgroundColor = "red";
    div.appendChild(div3);

    //选择图片按钮
    div3.innerHTML = div3.innerHTML + "<input type='file' multiple='multiple' required='required' name='files' id='0img' onchange='showPreview(this)'style='width:30% ;height:100%;border:1px;display=inline-block;position:relative;' />";
    //提交按钮
    // var button2 = document.createElement("input");
    // button2.type = "button";
    // button2.value = "上传";
    //// button2.onchange = clickSub;
    // button2.style.float = "right";
    // button2.style.width = "20%";
    // button2.style.height = "100%";
    // button2.style.position = "relative";
    // button2.style.top = "0%";
    // button2.style.display = "inline-block";
    // div3.appendChild(button2);
    /*---2017.2.25 BLM start*/
    //div3.innerHTML = div3.innerHTML + "<input type='button' onclick='clickSub();' value='上传' style='float:right;width:15%;height:100%;position:relative;display:inline-block;top:0%;;'/>";
    div3.innerHTML = div3.innerHTML + "<input type='button' onclick='clickSub();' value='上传' style='float:left; margin-top:5%;width:15%;height:100%;position:relative;display:inline-block;top:0%;;'/>";
    /*---2017.2.25 BLM end*/
    //  button2.innerHTML = "onclick=clickSub();";
    div.innerHTML = "<form  action='/Album/CreatePersonAblum' enctype='multipart/form-data' id='formMain' method='post'>" + div.innerHTML + "</form>";
}