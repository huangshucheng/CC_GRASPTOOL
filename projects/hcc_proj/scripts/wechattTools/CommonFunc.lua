function click(x,y)
    touchDown(0, x, y);
    mSleep(2);
    touchMove(6, x, y)
    mSleep(2);
    touchMove(6, x, y)
    mSleep(2);
    touchUp(0);
end

function clickForTime(x,y,n)
    touchDown(0, x, y);
    mSleep(n);
    touchUp(0);
end 
--x1,y1为起始位置坐标，x2、y2为终点位置坐标，n是每次移动多少个像素
function clickMove(x1,y1,x2,y2,n)
    local w = math.abs(x2-x1);
    local h = math.abs(y2-y1);
    touchDown(0,x1,y1);
    mSleep(50);
    if x1 < x2 then
        w1 = n; 
    else
        w1 = -n;
    end
    if y1 < y2 then
        h1 = n; 
    else
        h1 = -n;
    end
    if w >= h then
        for i = 1 , w,n do 
            x1 = x1 + w1;
            if y1 == y2 then
            else
                y1 = y1 + math.ceil(h*h1/w);
            end
            touchMove(0,x1,y1);
            mSleep(10);
        end
    else
        for i = 1 ,h,n do 
            y1 = y1 + h1;
            if x1 ==x2 then
            else
                x1 = x1 + math.ceil(w*w1/h);
            end
            touchMove(0,x1,y1);
            mSleep(10);
        end
    end
    mSleep(50);
    touchUp(0);
end

function clickHomeKey()
    keyDown('HOME');    -- HOME键按下
    mSleep(100);        --延时100毫秒
    keyUp('HOME');      -- HOME键抬起 
end

function main()
    --logDebug("name:" .. appName_table.wechat);
     --logDebug("account: ".. accountInfo_table.account[1]);
     click(551,1271);
end