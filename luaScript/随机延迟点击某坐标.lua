
function main()
	for i=1,10 do	
	tm={math.random(1000,10000),math.random(1000,10000)}
	mSleep(tm[2])		
	touchDown(0,100,200);  --用第0根手指点击X,Y坐标
	touchUp(0);  --抬起第0根手指
end
end