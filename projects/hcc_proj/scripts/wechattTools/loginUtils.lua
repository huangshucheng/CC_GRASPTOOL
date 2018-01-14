package.path=package.path .. ";/var/touchelf/scripts/?.lua"
require("staticConf");
require("CommonFunc");
require("PosConf");
local iphone = iphone6s;

function openAppByPkgName(pkgName)
    if not pkgName then return end
    appRun(pkgName);
end

function killAppByPkgName(pkgName)
     if not pkgName then return end
    appKill(pkgName);
end

function login(account,password,phone)
    if not account or not password then 
            logDebug("请输入正确的帐号密码!");
    end
    phone = phone or "null"
    openAppByPkgName(appName_table.wechat);
    mSleep(1000);
    click(iphone.pos_login_more_option_btn.x ,iphone.pos_login_more_option_btn.y);
    mSleep(1000);
    click(iphone.pos_switch_account_btn.x,iphone.pos_switch_account_btn.y);
    mSleep(1000);
    click(iphone.pos_use_wechat_option_btn.x,iphone.pos_use_wechat_option_btn.y);
    mSleep(1000);
    click(iphone.pos_account_inputbox.x,iphone.pos_account_inputbox.y);
    mSleep(500);
    inputText(accountInfo_table.account[1]);
    mSleep(500);
    click(iphone.pos_empty_input_square.x,iphone.pos_empty_input_square.y);
    mSleep(500);
    click(iphone.pos_pswd_inputbox.x,iphone.pos_pswd_inputbox.y);
    mSleep(500);
    inputText(accountInfo_table.password[1]);
    mSleep(500);
    click(iphone.pos_empty_input_square.x,iphone.pos_empty_input_square.y);
    mSleep(500);
    click(iphone.pos_login_btn.x,iphone.pos_login_btn.y);
end

function loginOut()
    openAppByPkgName(appName_table.wechat);
    mSleep(1000);
    click(iphone.pos_me_btn.x ,iphone.pos_me_btn.y);
    mSleep(500);
    click(iphone.pos_setting_btn.x,iphone.pos_setting_btn.y);
    mSleep(1000);
    click(iphone.pos_exit_btn.x,iphone.pos_exit_btn.y);
    mSleep(1000);
    click(iphone.pos_exit_sub_btn.x,iphone.pos_exit_sub_btn.y);
end

function main()
    --killAppByPkgName(appName_table.wechat);
    --logDebug("account: ".. accountInfo_table.account[1] .. "  ," .. accountInfo_table.password[1]);
    login(accountInfo_table.account[1],accountInfo_table.password[1],"");
    --loginOut();
end