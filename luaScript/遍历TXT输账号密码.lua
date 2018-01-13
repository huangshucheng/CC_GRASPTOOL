function main()
	file = io.open("/mnt/sdcard/zhanghao.txt","r");
	m_file = io.open("/mnt/sdcard/mima.txt","r");
	zh={}
	i=0
	ma={}
	m=0
	for l in file:lines() do
		i=i+1
		zh[i]=l
		inputText(zh[i])  --输入第一个账号		
		for p in m_file:lines() do
			m=m+1
			ma[m]=p			
			if i==m then
			inputText(ma[m])
			  break				
			end
		
		end
	end
	file:close()
	m_file:close()
end