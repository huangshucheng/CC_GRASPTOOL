function LuaReomve(str,remove)  --�������ͣ��������ַ�������ɾ���ַ���
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




local s = LuaReomve("����������������ƻƽ�����Ů����ط�����ʯ����ʯ��������Ȫ��������Һ��תʥˮ��Ȫ��¶��ת��¶����ʯ����","ʯ")  --�������ͣ��������ַ�������ɾ���ַ���
print(s)  --����޸ĺ���ַ���

