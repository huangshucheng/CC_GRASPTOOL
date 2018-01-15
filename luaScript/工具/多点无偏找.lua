--t={ x,y,0,0, pointNumber, //x,y  是找到图后点击的位置坐标或找图前需点击的按钮坐标， pointNumber是本次搜索中有效点的个数, 0未用保留字段
--	x1,y1,r1,g1,b1,  		  //x1,y1是图中点的坐标， r1，g1，b1是参考颜色
--	x2,y2,r2,g2,b2,
--	...
--	xn,yn,rn,gn,bn
--	};
--调用为:ddzs1(a,sim)   a为数组名，sim精确度（一般用30）
function ddzs1(t,sim)
	local i = 6;--定义局部（local为局部）变量i 并赋值为6
	local lr,lg,lb;	 --定义局部（local为局部）变量lr.lg.lb 并赋值为6
	
	
	while (i+4) <= (t[5]*5 + 5) do		--当i加4小于等于数组下特征数（t[5]是数组下特征数（也就是除第一行外的，行数））时
		lr,lg,lb = getColorRGB(t[i],t[i+1]);--局部变量lt.lg.lb被赋上数组第一个和第二个坐标相对应点的rgb值
		if math.abs(lr-t[i+2]) > sim then--如果lr（lr为屏幕上的真实r） 减去预设的（i为6,加上2,等于8。数组第8个为r）值，的绝对值（100-120的绝对值为20），大于预设的sim（就是30）则
			return false;	--返回假（一返回假就意味这次找多点已经失败）
		end--结束当前if
		if math.abs(lg-t[i+3]) > sim then --如果lg（lg为屏幕上的真实g） 减去预设的（i为6,加上3,等于9。数组第9个为g）值，的绝对值（100-120的绝对值为20），大于预设的sim（就是30）则 
			return false;	 --返回假 （一返回假就意味这次找多点已经失败）
		end--结束当前if
		if math.abs(lb-t[i+4]) > sim then --如果lb（lb为屏幕上的真实b） 减去预设的（i为6,加上3,等于10。数组第10个为b）值，的绝对值（100-120的绝对值为20），大于预设的sim（就是30）则 
			return false;	 --返回假 （一返回假就意味这次找多点已经失败）
		end	--结束当前if 
		i = i + 5;--把当前的i加上五（这样下一个找色时就会跳过一行）
	end;--结束上面while（这个结束和上面的while里面的算式结果有关，结果成立就结束）
	return true;--返回真（意味着所有找到的rgb和预设的rgb都是在误差范围内的）
end--结束找色