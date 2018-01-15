function findColorInRegion_Multi(t,sim, x1,y1, x2, y2) ---多点区域模糊找色。1，为多点数组 ，2，为相似度（0-255）3，4，5，6，左上右下坐标
	local x,y,i;
	local r,g,b;	
	local flag;
	y = y1-t[7];
	while y <= y2-t[7] do
		x = x1-t[6];
		while x<= x2-t[6] do
			flag = true;
			i = 6;
			while (i+4) <= (t[5]*5 + 5) do		
				r,g,b = getColorRGB(t[i] + x,t[i+1] + y);
				if math.abs(r-t[i+2]) > sim then 
					flag = false;
					break;	
				end
				if math.abs(g-t[i+3]) > sim then 
					flag = false;
					break;	
				end
				if math.abs(b-t[i+4]) > sim then 
					flag = false;
					break;	
				end				
				i = i + 5;
			end
			if flag then
				return t[6] + x,t[7] + y,true;
			end
			x = x + 1;
		end
		y = y + 1;
	end
	return -1,-1,false;
end
---车夫两个字的点参数
g_t_c_chefu = {730, 220, 830,600, 9, --1，2 ,3, 4 字段，为车夫区域坐标 ,5为有效点数
754, 499, 230, 235, 230,    --1
797, 500, 255, 255, 255,    --2
756, 506, 255, 255, 255,    --3
798, 506, 255, 255, 255,    --4
754, 511, 255, 255, 255,    --5
792, 512, 255, 255, 255,    --6
765, 517, 255, 255, 255,    --7
798, 516, 255, 255, 255,    --8
788, 511, 107, 109, 107,    --9
};



function main()
	x,y=findColorInRegion_Multi(g_t_c_chefu, 40, g_t_c_chefu[1],g_t_c_chefu[2],g_t_c_chefu[3],g_t_c_chefu[4]);
	notifyMessage(x.."--"..y);
end