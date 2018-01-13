function main()
    -- URL编码
    notifyMessage(lua_util.url_encode("哈哈"))
    mSleep(2000)
    -- URL解码
    notifyMessage(lua_util.url_decode("%E5%93%88%E5%93%88"))
end
lua_util = {};  
function lua_util.url_encode(str)  
  if (str) then  
    str = string.gsub (str, "\n", "\r\n")  
    str = string.gsub (str, "([^%w ])",  
        function (c) return string.format ("%%%02X", string.byte(c)) end)  
    str = string.gsub (str, " ", "+")  
  end  
  return str      
end  
  
function lua_util.url_decode(str)  
  str = string.gsub (str, "+", " ")  
  str = string.gsub (str, "%%(%x%x)",  
      function(h) return string.char(tonumber(h,16)) end)  
  str = string.gsub (str, "\r\n", "\n")  
  return str  
end  