function LuaReomve(str,remove)  --参数类型（待处理字符串，待删除字符）
    local lcSubStrTab = {}  
   while true do  
       local lcPos = string.find(str,remove)  
        if not lcPos then  
            lcSubStrTab[#lcSubStrTab+1] =  str      
            break  
       end  
       local lcSubStr  = string.sub(str,1,lcPos-1)  
        lcSubStrTab[#lcSubStrTab+1] = lcSubStr  
        str = string.sub(str,lcPos+1,#str)  
    end  
   local lcMergeStr =""  
    local lci = 1  
   while true do  
        if lcSubStrTab[lci] then  
           lcMergeStr = lcMergeStr .. lcSubStrTab[lci]   
            lci = lci + 1  
        else   
            break  
        end  
    end  
    return lcMergeStr  
end  




local s = LuaReomve("风灵珠解封符绿玉令牌黄金令牌女娲跳关符黑曜石黄天石晖明丹玉泉琼浆三花玉液九转圣水玉泉仙露九转仙露炎阳石寒月","石")  --参数类型（待处理字符串，待删除字符）
print(s)  --输出修改后的字符串

