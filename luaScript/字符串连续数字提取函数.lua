-- 点击函数封装
function click(x, y)
	touchDown(0, x, y)
	mSleep(100)
	touchUp(0)
end

-- 主入口函数
function main()
b1='微信验证码234567哈哈哈哈的辅导辅导辅导辅导辅导'

shuzi(b1,6)--数字提取 验证码提取专用 第一个参数是要处理的字符串   第二个参数 是字符串中连续的N未数字  只提取连续的N未数
--第一位和最后一位比提取
end







------------------------------------------------从字符串中提取连续n位数数字
function shuzi(ss,nn100000)--ss要处理的字符串  nn100000为要提取数字的位数
local  n100000=1

while nn100000==8 do
	
local a,s,d,f,g,h,j,k,l,z=string.byte(ss,n100000,n100000+9)
	if a~=nil and s~=nil and d~=nil and f~=nil and g~=nil and h~=nil and j~=nil and k~=nil and l~=nil and z~=nil then	
     if (58<a or a<47) and (58>s and s>47) and (58>d and d>47) 
	 and (58>f and f>47) and(58>g and g>47) and(58>h and h>47) 
	 and(58>j and j>47)  and(58>k and k>47) and(58>l and l>47)	 
	 and (58<z or z<47)                                then			
     sdfghjkz=string.char(s,d,f,g,h,j,k,l)
     return sdfghjkz
     end
    n100000=n100000+1	 
   else
	 return nil	
	end		
end



while nn100000==7 do
	
local a,s,d,f,g,h,j,k,l=string.byte(ss,n100000,n100000+9)
	if a~=nil and s~=nil and d~=nil and f~=nil and g~=nil and h~=nil and j~=nil and k~=nil and l~=nil then	
     if (58<a or a<47) and (58>s and s>47) and (58>d and d>47) 
	 and (58>f and f>47) and(58>g and g>47) and(58>h and h>47) 
	 and(58>j and j>47)  and(58>k and k>47) 	 
	 and (58<l or l<47)                                then			
     sdfghjk=string.char(s,d,f,g,h,j,k)
     return sdfghjk
     end
    n100000=n100000+1	 
   else
	 return nil	
	end		
end


while nn100000==6 do
	
local a,s,d,f,g,h,j,k=string.byte(ss,n100000,n100000+8)
	if a~=nil and s~=nil and d~=nil and f~=nil and g~=nil and h~=nil and j~=nil and k~=nil then	
     if (58<a or a<47) and (58>s and s>47) and (58>d and d>47) 
	 and (58>f and f>47) and(58>g and g>47) and(58>h and h>47) 
	 and(58>j and j>47) 	 
	 and (58<k or k<47)                                then			
     sdfghj=string.char(s,d,f,g,h,j)
     return sdfghj 
     end
    n100000=n100000+1	 
   else
	 return nil	
	end		
end


while nn100000==5 do
	
local a,s,d,f,g,h,j=string.byte(ss,n100000,n100000+7)
	if a~=nil and s~=nil and d~=nil and f~=nil and g~=nil and h~=nil and j~=nil  then	
     if (58<a or a<47) and (58>s and s>47) and (58>d and d>47) 
	 and (58>f and f>47) and(58>g and g>47) and(58>h and h>47) 	 
	 and (58<j or j<47)                                then			
     sdfgh=string.char(s,d,f,g,h)
     return sdfgh 
     end
    n100000=n100000+1	 
   else
	 return nil	
	end		
end


while nn100000==4 do
	
local a,s,d,f,g,h=string.byte(ss,n100000,n100000+6)
	if a~=nil and s~=nil and d~=nil and f~=nil and g~=nil and h~=nil   then	
     if (58<a or a<47) and (58>s and s>47) and (58>d and d>47) 
	 and (58>f and f>47) and(58>g and g>47) 	 
	 and (58<h or h<47)                                then			
     sdfg=string.char(s,d,f,g)
     return sdfg 
     end
    n100000=n100000+1	 
   else
	 return nil	
	end		
end


end

-------------------------------------------------------------------
