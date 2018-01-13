-- 初始化LUA脚本文件的加载路径,若你不明白，可百度搜索LUA导入模块接口 require 的加载原理
local Win32LuaScriptPath = {
	"./Script/?.lua",
	--"Script/util/init.lua",
}
for k, v in pairs(Win32LuaScriptPath) do
	package.path = package.path .. ';'..v
end

-- 加载牌公共接口
require  "Getcard"--与

mycard   = Getcard.mycard

for k,v in pairs(mycard) do
	print(k,"----------",v)
end



--print(#mycard)


