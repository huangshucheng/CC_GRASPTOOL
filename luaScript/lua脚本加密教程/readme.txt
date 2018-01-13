====================
 lua脚本加密方法
====================
1. 安装luac
    1.1 使用RE浏览器，将/目录“挂载读写”
    1.2 使用RE浏览器将luac制到手机的/system/bin目录下
    1.3 在RE浏览器中长按luac文件，在弹出的菜单中选中“权限”
    1.4 将“执行”权限打勾
2. 加密脚本
    2.1 安装“终端模拟器”
    2.2 加密脚本，假设脚本为a.lua，生成的加密脚本保存到SD卡根目录的a.luac
        命令为：luac  -s  -o  /mnt/sdcard/a.luac  /mnt/sdcard/Touchelper/scripts/v2/a.lua  

====================
 lua脚本绑定机器方法
====================
lua脚本加密后只是隐藏了代码，如果需要绑定手机的话可以做如下处理，假设设置该脚本只能在MAC地址为aa:bb:cc:dd:ee:ff的手机上运行

function main()
        f = io.open("/sys/class/net/wlan0/address");
        m = f:read();

        -- 计算MAC地址包含的17个字符的ascii码之和
        sum = 0;
        for i=1,17,1 do
                sum = sum + m:byte(i);
        end

        -- aa:bb:cc:dd:ee:ff的和为1484，如果不等于则退出，卖脚本时，将1484修改为客户的MAC之和，然后用luac加密该脚本
        if sum ~= 1484 then
                os.exit();
        end
end

上面代码是比较播放该脚本的手机的MAC地址各个字符的ascii码之和，不要直接比较m与aa:bb:cc:dd:ee:ff这2个字符串是否相等，因为luac加密后的二进制文件中，字符串还是明文的，依然可以被修改。

附带ascii对照表，如a为97，:为58，A为65

          2 3 4 5 6 7       30 40 50 60 70 80 90 100 110 120
        -------------      ---------------------------------
       0:   0 @ P ` p     0:    (  2  <  F  P  Z  d   n   x
       1: ! 1 A Q a q     1:    )  3  =  G  Q  [  e   o   y
       2: " 2 B R b r     2:    *  4  >  H  R  \  f   p   z
       3: # 3 C S c s     3: !  +  5  ?  I  S  ]  g   q   {
       4: $ 4 D T d t     4: "  ,  6  @  J  T  ^  h   r   |
       5: % 5 E U e u     5: #  -  7  A  K  U  _  i   s   }
       6: & 6 F V f v     6: $  .  8  B  L  V  `  j   t   ~
       7: ′ 7 G W g w     7: %  /  9  C  M  W  a  k   u  DEL
       8: ( 8 H X h x     8: &  0  :  D  N  X  b  l   v
       9: ) 9 I Y i y     9: ′  1  ;  E  O  Y  c  m   w
       A: * : J Z j z
       B: + ; K [ k {
       C: , < L \ l |
       D: - = M ] m }
       E: . > N ^ n ~
       F: / ? O _ o DEL