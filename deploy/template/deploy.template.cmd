@echo off

::需要配置的参数
::最后打包的文件名与pom一致 /pn 配置
set project_name=${projectName}
::服务器ip /s 配置
set server_addr=${ip}
::服务器ssh连接端口 /p 配置
set server_port=${port}
::服务是否备份 /b 配置
set server_backup=${serverBackUp}
::本地是否备份 /lb 配置
set loc_backup=${localBackUp}
::服务器备份的地址 /bl 配置
set "server_backup_loc=${backUpLocation}"
::服务器密码 /pw 配置
set password=${password}
::服务器登录用户 /u 配置
set user=${userName}
::服务器部署为 /ds 配置
set deploy_as=${deployAs}
::服务器tomcat目录 /wal 配置
set "webapps_loc=${webappsLocation}"
::包含 lib 包
set include_lib=${includeLib}
::包含配置文件
set include_config=${includeConfig}
::包含 js/css/img/html/jsp文件
set include_web_file=${includeWebFile}
::包含 class 文件
set include_byte_code=${includeByteCode}
::shell 脚本位置
set "shell_loc=${shellLocation}"
::结束任务标识
set "stop_tag=${stopTag}"

::是否要删除原来的文件
set cover=n
::设置当前路径 /cdr (用户不需要配置)
set "cdr=${currentDirectory}"
::是否重启服务器
set restart_server=y

:get_param
set param=%1
set value=%2
::去除包裹引号
if not "%value%" == "" (goto :check_value) else (goto :after_check_value)
:check_value
echo %value:~0,1%|findstr /c:" >nul &&goto :check_value_last
goto :after_check_value
:check_value_last
echo %value:~-1%|findstr /c:" >nul &&set value=%value:~1,-1%
:after_check_value
if not "%param%" == "" (
    if "%param%" == "/pn" (
        set project_name=%value%
    ) else if "%param%" == "/s" (
        set server_addr=%value%
    ) else if "%param%" == "/p" (
        set server_port=%value%
    ) else if "%param%" == "/b" (
        set server_backup=%value%
    ) else if "%param%" == "/lb" (
        set loc_backup=%value%
    ) else if "%param%" == "/bl" (
        set server_backup_loc=%value%
    ) else if "%param%" == "/pw" (
        set password=%value%
    ) else if "%param%" == "/u" (
        set user=%value%
    ) else if "%param%" == "/ds" (
        set deploy_as=%value%
    ) else if "%param%" == "/ch" (
        set catalina_home=%value%
    ) else if "%param%" == "/cv" (
       set cover=%value%
    ) else if "%param%" == "/cdr" (
        set "cdr=%value%"
    )
    shift /1
    goto :get_param
)

if "%password%" == "" (
    set /p password=请输入 %server_addr% 的密码:
)

if "%include_lib%" == "n" ( goto :after_check_cover )
if "%include_config%" == "n" ( goto :after_check_cover )
if "%include_web_file%" == "n" ( goto :after_check_cover )
if "%include_byte_code%" == "n" ( goto :after_check_cover )
set cover=y
:after_check_cover

if not "%include_lib%" == "n" ( goto :after_check_restart )
if not "%include_config%" == "n" ( goto :after_check_restart )
if not "%include_byte_code%" == "n" ( goto :after_check_restart )
if "%include_web_file%" == "n" ( goto :after_check_restart )
set restart_server=n
:after_check_restart

if not "%cover%" == "y" goto :after_cover_confirm
set /p "confirm_cover=此操作将会删除服务器 %server_addr%/%deploy_as% 的所有文件，是否继续(y/n)?"
if not "%confirm_cover%" == "y" (
    echo 部署失败
    pause 1>nul
    goto :EOF
)
:after_cover_confirm


::设置当前目录
set "current_dir=%~dp0"
if not "%cdr%" == "" (
    set "current_dir=%cdr%\"
    cd /d "%current_dir%"
)

::可运行文件
set putty="%current_dir%putty"
set pscp="%current_dir%pscp"
set package="%current_dir%package"

::本地可变参数
set final_zip="%current_dir%..\%project_name%.zip"
::默认认为包里面不包含 lib 和配置文件


::开始打包
call %package%

::上传文件
:upload
::生成sh文件
echo 生成shell脚本
set linux_sh="%current_dir%\linux.sh"
if exist %linux_sh% (
    del /f /s %linux_sh% 1>nul
)
echo #!/usr/bin/env bash > %linux_sh%
echo webapps_loc="%webapps_loc%" >> %linux_sh%
echo server_backup_loc="%server_backup_loc%" >> %linux_sh%
echo project_name="%project_name%" >> %linux_sh%
echo deploy_as="%deploy_as%" >> %linux_sh%

echo= >> %linux_sh%
echo= >> %linux_sh%
echo if [ ! -f "$server_backup_loc/$project_name.zip" ]; then >> %linux_sh%
echo    echo "$server_backup_loc/$project_name.zip does not exist!" >> %linux_sh%
echo    echo "deploy shutdown !" >> %linux_sh%
echo    read -n1 var >> %linux_sh%
echo    exit 0 >>%linux_sh%
echo fi >>%linux_sh%
echo rm -rf $server_backup_loc/$project_name >> %linux_sh%
echo rm -rf $server_backup_loc/$deploy_as >> %linux_sh%
echo rm -rf $server_backup_loc/readme.txt >> %linux_sh%
::检查工程
if "%cover%" == "y" goto :after_check_project
echo #check project >> %linux_sh%
echo if [ ! -f "$webapps_loc/$deploy_as/WEB-INF/web.xml" ]; then >> %linux_sh%
echo    echo "server does not please redeploy user all option!" >> %linux_sh%
echo    read -n1 var >> %linux_sh%
echo    exit 0 >> %linux_sh%
echo fi >> %linux_sh%
:after_check_project
echo unzip -q $server_backup_loc/$project_name.zip -d $server_backup_loc >> %linux_sh%
if "%restart_server%" == "y" (
echo ps -ef^|grep tomcat ^|grep %stop_tag%^|grep -v grep^|grep -v PPID^|grep -v tail^|awk '{print $2}'^|xargs kill -9 >> %linux_sh%
)
if "%cover%" == "y" (
    goto :rm_server_files
) else (
    goto :copy_files
)
::删除原始目录
:rm_server_files
::删文件之前要干的事情
echo #back up upload files >> %linux_sh%
echo if [ -d "$server_backup_loc/upload" ]; then >> %linux_sh%
echo    echo directory "$server_backup_loc/upload" is already exist please ensure the files and operation stop ! >> %linux_sh%
echo    read -n1 var >> %linux_sh%
echo    exit 0 >> %linux_sh%
echo fi >> %linux_sh%
echo if [ -d "$webapps_loc/$deploy_as/upload" ]; then >> %linux_sh%
echo    mv $webapps_loc/$deploy_as/upload $server_backup_loc >> %linux_sh%
echo fi >> %linux_sh%
echo #rm original >>%linux_sh%
echo rm -rf $webapps_loc/$deploy_as >>%linux_sh%
)
::拷贝解压文件到目录
:copy_files
if "%cover%" == "y" goto :put_files
echo #cover files >>%linux_sh%
if not "%project_name%" == "%deploy_as%" (
echo mv $server_backup_loc/$project_name $server_backup_loc/$deploy_as >>%linux_sh%
)
echo echo y^|cp -rf $server_backup_loc/$deploy_as $catalina_home/webapps >>%linux_sh%
goto :after_put_files
:put_files
echo #put files >>%linux_sh%
echo mv $server_backup_loc/$project_name $webapps_loc/$deploy_as >>%linux_sh%
:after_put_files
if "%server_backup%" == "y" (
    goto :backup_sh
) else (
    goto :rm_sh
)

::备份文件
:backup_sh
echo #back up project zip file >>%linux_sh%
if not "%cover%" == "y" goto :backup_nolib_sh
echo mv $server_backup_loc/$project_name.zip $server_backup_loc/%project_name%_$(date +^%%Y-^%%m-^%%d_^%%H:^%%M:^%%S).zip >> %linux_sh%
goto :else_sh
:backup_nolib_sh
echo mv $server_backup_loc/$project_name.zip $server_backup_loc/%project_name%_$(date +^%%Y-^%%m-^%%d_^%%H:^%%M:^%%S)_nolib.zip >> %linux_sh%
goto :else_sh

::删除文件
:rm_sh
echo #rm project zip file >> %linux_sh%
echo rm -rf $server_backup_loc/$project_name.zip >> %linux_sh%
:else_sh
echo rm -rf $server_backup_loc/$project_name >> %linux_sh%
echo rm -rf $server_backup_loc/readme.txt >> %linux_sh%
if not "%cover%" == "y" goto :else_sh1
::删文件之后要干的事情
echo #recover upload files >> %linux_sh%
echo if [ -d "$server_backup_loc/upload" ]; then >> %linux_sh%
echo    mv $server_backup_loc/upload $webapps_loc/$deploy_as >> %linux_sh%
echo fi >> %linux_sh%
:else_sh1
if "%restart_server%" == "y" (
echo %shell_loc%/startup.sh >> %linux_sh%
)
echo 开始上传文件
call %pscp% -pw %password% -P %server_port%  %final_zip% %user%@%server_addr%:%server_backup_loc%
echo 开始运行 shell 脚本
call %putty% %user%@%server_addr% -pw %password% -P %server_port% -m %linux_sh%

set log_file="%current_dir%..\log\deploy.log"
echo 部署已成功!
echo %date:~0,10% %time:~0,8% %username% 部署 %server_addr% 成功 lcjs:%include_lib%%include_config%%include_web_file%%include_byte_code%  >> %log_file%
if "%loc_backup%" == "y" goto :eof
if exist %final_zip% (
    del /f /s %final_zip% 1>nul
)
if exist %linux_sh% (
    del /f /s %linux_sh% 1>nul
)
:eof
exit 1
