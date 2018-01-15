function a(s)
return(string.gsub(s, "^%s*(.-)%s*$", "%1"));--前后带空格的文字 去掉首尾空格
end
print(a(   432432   ))  



