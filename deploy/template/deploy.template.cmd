@echo off

::��Ҫ���õĲ���
::��������ļ�����pomһ�� /pn ����
set project_name=${projectName}
::������ip /s ����
set server_addr=${ip}
::������ssh���Ӷ˿� /p ����
set server_port=${port}
::�����Ƿ񱸷� /b ����
set server_backup=${serverBackUp}
::�����Ƿ񱸷� /lb ����
set loc_backup=${localBackUp}
::���������ݵĵ�ַ /bl ����
set "server_backup_loc=${backUpLocation}"
::���������� /pw ����
set password=${password}
::��������¼�û� /u ����
set user=${userName}
::����������Ϊ /ds ����
set deploy_as=${deployAs}
::������tomcatĿ¼ /wal ����
set "webapps_loc=${webappsLocation}"
::���� lib ��
set include_lib=${includeLib}
::���������ļ�
set include_config=${includeConfig}
::���� js/css/img/html/jsp�ļ�
set include_web_file=${includeWebFile}
::���� class �ļ�
set include_byte_code=${includeByteCode}
::shell �ű�λ��
set "shell_loc=${shellLocation}"
::���������ʶ
set "stop_tag=${stopTag}"

::�Ƿ�Ҫɾ��ԭ�����ļ�
set cover=n
::���õ�ǰ·�� /cdr (�û�����Ҫ����)
set "cdr=${currentDirectory}"
::�Ƿ�����������
set restart_server=y

:get_param
set param=%1
set value=%2
::ȥ����������
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
    set /p password=������ %server_addr% ������:
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
set /p "confirm_cover=�˲�������ɾ�������� %server_addr%/%deploy_as% �������ļ����Ƿ����(y/n)?"
if not "%confirm_cover%" == "y" (
    echo ����ʧ��
    pause 1>nul
    goto :EOF
)
:after_cover_confirm


::���õ�ǰĿ¼
set "current_dir=%~dp0"
if not "%cdr%" == "" (
    set "current_dir=%cdr%\"
    cd /d "%current_dir%"
)

::�������ļ�
set putty="%current_dir%putty"
set pscp="%current_dir%pscp"
set package="%current_dir%package"

::���ؿɱ����
set final_zip="%current_dir%..\%project_name%.zip"
::Ĭ����Ϊ�����治���� lib �������ļ�


::��ʼ���
call %package%

::�ϴ��ļ�
:upload
::����sh�ļ�
echo ����shell�ű�
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
::��鹤��
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
::ɾ��ԭʼĿ¼
:rm_server_files
::ɾ�ļ�֮ǰҪ�ɵ�����
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
::������ѹ�ļ���Ŀ¼
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

::�����ļ�
:backup_sh
echo #back up project zip file >>%linux_sh%
if not "%cover%" == "y" goto :backup_nolib_sh
echo mv $server_backup_loc/$project_name.zip $server_backup_loc/%project_name%_$(date +^%%Y-^%%m-^%%d_^%%H:^%%M:^%%S).zip >> %linux_sh%
goto :else_sh
:backup_nolib_sh
echo mv $server_backup_loc/$project_name.zip $server_backup_loc/%project_name%_$(date +^%%Y-^%%m-^%%d_^%%H:^%%M:^%%S)_nolib.zip >> %linux_sh%
goto :else_sh

::ɾ���ļ�
:rm_sh
echo #rm project zip file >> %linux_sh%
echo rm -rf $server_backup_loc/$project_name.zip >> %linux_sh%
:else_sh
echo rm -rf $server_backup_loc/$project_name >> %linux_sh%
echo rm -rf $server_backup_loc/readme.txt >> %linux_sh%
if not "%cover%" == "y" goto :else_sh1
::ɾ�ļ�֮��Ҫ�ɵ�����
echo #recover upload files >> %linux_sh%
echo if [ -d "$server_backup_loc/upload" ]; then >> %linux_sh%
echo    mv $server_backup_loc/upload $webapps_loc/$deploy_as >> %linux_sh%
echo fi >> %linux_sh%
:else_sh1
if "%restart_server%" == "y" (
echo %shell_loc%/startup.sh >> %linux_sh%
)
echo ��ʼ�ϴ��ļ�
call %pscp% -pw %password% -P %server_port%  %final_zip% %user%@%server_addr%:%server_backup_loc%
echo ��ʼ���� shell �ű�
call %putty% %user%@%server_addr% -pw %password% -P %server_port% -m %linux_sh%

set log_file="%current_dir%..\log\deploy.log"
echo �����ѳɹ�!
echo %date:~0,10% %time:~0,8% %username% ���� %server_addr% �ɹ� lcjs:%include_lib%%include_config%%include_web_file%%include_byte_code%  >> %log_file%
if "%loc_backup%" == "y" goto :eof
if exist %final_zip% (
    del /f /s %final_zip% 1>nul
)
if exist %linux_sh% (
    del /f /s %linux_sh% 1>nul
)
:eof
exit 1
