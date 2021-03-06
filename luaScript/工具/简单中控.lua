
local function PlayFile(act,ip,name)
    local ftp = require("socket.ftp")
    local ltn12 = require("ltn12")
    if act == "play" then
        act = ".__STARTSCRIPT__.";
        ftp.get(string.format("ftp://%s:50021/%s %s",ip,act,name))
    elseif act == "stop" then
        act = ".__STOPPLAY__";
        ftp.get(string.format("ftp://%s:50021/%s",ip,act))
    elseif act == "up" then
        ftp.put{
              host = ip, 
              port = "50021",
              argument = string.format("scripts/%s",name),
              source = ltn12.source.file(io.open(string.format("/var/touchelf/scripts/%s",name), "r"))
            }
    end
end
function main()
   PlayFile("up","192.168.31.125","api.lua") --上传api.lua脚本到指定ip机器
   PlayFile("play","192.168.31.125","api.lua")--运行指定ip上的api.lua脚本
   PlayFile("stop","192.168.31.125")--结束脚本
end

