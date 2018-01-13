--lua ×Ö·û´®·Ö¸îº¯Êý  
-------------------------------------------------------  
-- ²ÎÊý:´ý·Ö¸îµÄ×Ö·û´®,·Ö¸î×Ö·û  
-- ·µ»Ø:×Ó´®±í.(º¬ÓÐ¿Õ´®)  
function lua_string_split(str, split_char)      
local sub_str_tab = {};  
   
while (true) do          
local pos = string.find(str, split_char);    
 if (not pos) then              
  local size_t = table.getn(sub_str_tab)  
  table.insert(sub_str_tab,size_t+1,str);  
  break;    
 end  
   
local sub_str = string.sub(str, 1, pos - 1);                
 local size_t = table.getn(sub_str_tab)  
 table.insert(sub_str_tab,size_t+1,sub_str);  
 local t = string.len(str);  
str = string.sub(str, pos + 1, t);     
 end      
 return sub_str_tab;  
end  
  
local str = "1,2,3,4,5,6,7,8,9"  
local ta  = lua_string_split(str,",")  
  
local size = table.getn(ta)  
for i = 1,size ,1 do  
 print(ta[i])  
end  
