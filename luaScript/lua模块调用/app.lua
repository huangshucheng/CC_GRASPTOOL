-- ��ʼ��LUA�ű��ļ��ļ���·��,���㲻���ף��ɰٶ�����LUA����ģ��ӿ� require �ļ���ԭ��
local Win32LuaScriptPath = {
	"./Script/?.lua",
	--"Script/util/init.lua",
}
for k, v in pairs(Win32LuaScriptPath) do
	package.path = package.path .. ';'..v
end

-- �����ƹ����ӿ�
require  "Getcard"--��

mycard   = Getcard.mycard

for k,v in pairs(mycard) do
	print(k,"----------",v)
end



--print(#mycard)


