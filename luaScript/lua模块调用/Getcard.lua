
-- 定义牌模块
module "Getcard"--得到牌
mycard     = {} --自己手上的牌

for i = 1,54 do
	mycard[i]=i-1
end

for i = 1,54 do
	mycard[i+54]=i-1
end
