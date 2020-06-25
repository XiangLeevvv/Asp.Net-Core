//写cookie
function setCookie(name,value)
{
    console.log("set cookie:" + name + ":" + value);
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days*24*60*60*1000);
    document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString();
}
//读取cookie
function getCookie(name)
{
    console.log("get Cookie")
    var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)");
    if(arr=document.cookie.match(reg)) return unescape(arr[2]);
    else return null;
}
//删除cookie
function delCookie(name)
{
    console.log('delCookie')
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval=this.getCookie(name);
    if(cval!=null) document.cookie= name + "="+cval+";expires="+exp.toGMTString();
}

export default {
    setCookie: setCookie,
    getCookie: getCookie,
    delCookie: delCookie
}
