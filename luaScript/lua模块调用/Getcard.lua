
-- ������ģ��
module "Getcard"--�õ���
mycard     = {} --�Լ����ϵ���

for i = 1,54 do
	mycard[i]=i-1
end

for i = 1,54 do
	mycard[i+54]=i-1
end
