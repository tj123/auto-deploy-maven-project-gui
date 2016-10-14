@echo off

::��Ҫ���õĲ���
::��������ļ�����pomһ�� /pn ����
set "project_name=${projectName}"
::��Ҫ�滻��Ŀ¼replaceĿ¼�µ��ļ��� /s ����
set "server_ip=${ip}"
::�Ƿ������ /il ����
set include_lib=${includeLib}
::�Ƿ�������е������ļ� /ic ����
set include_config=${includeConfig}
::�Ƿ����js/jsp/html�ļ� /iwf ����
set include_web_file=${includeWebFile}
::�Ƿ����class�ļ� /ibc ����
set include_byte_code=${includeByteCode}
::���õ�ǰĿ¼ /cdr ����
set "cdr=${currentDirectory}"
set "current_dir=%~dp0"
::������ ip ��������滻�����ļ� /srs ����
set "servers=${servers}"

set has_replace=n
for %%i in (%servers%) do (
	if %%i == %server_ip% ( set has_replace=y )
) 
if "%has_replace%" == "n" (
	echo ������ipУ��ʧ�ܣ���ȷ��������ȷ
	pause 1>nul
	goto :eof
)

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
	if "%param%" == "/s" (
		set server_ip=%value%
	) else if "%param%" == "/il" (
		set include_lib=%value%
	) else if "%param%" == "/ic" (
		set include_config=%value%
	) else if "%param%" == "/iwf" (
		set include_web_file=%value%
	) else if "%param%" == "/ibc" (
		set include_byte_code=%value%
	) else if "%param%" == "/srs" (
		set "servers=%value%"
	) else if "%param%" == "/?" (
		goto :help
	) else if "%param%" == "/help" (
		goto :help
	) else if "%param%" == "/cdr" (
		set "cdr=%value%"
	)
	shift /1
	goto :get_param
)

if not "%cdr%" == "" (
	set "current_dir=%cdr%\"
	cd /d "%current_dir%"
)

::���ÿ������ļ�·��
set mvn="%current_dir%apache-maven-3.3.9\bin\mvn"
set seven_z="%current_dir%7-Zip\7z"
::�ǹ̶�����
set "project_home=%current_dir%..\..\..\..\.."
set "replace_dir=%current_dir%..\replace"
set original_war="%project_home%\target\%project_name%.war"
set "webapps=%current_dir%..\webapps"
set deploy_zip="%webapps%\deploy.zip"
set final_zip="%current_dir%\..\%project_name%.zip"
set "project_fd=%webapps%\%project_name%"
set read_me="%webapps%\readme.txt"

goto :next

:help
	echo /s ������� ѡҪ�滻���ļ�
	echo /il y/n ѡ���Ƿ�� lib ��
	echo /ic y/n ѡ���Ƿ���������ļ�
	echo /iwf y/n ѡ���Ƿ���� js/jsp/html �ļ�
	echo /ibc y/n ѡ���Ƿ���� class �ļ�
	echo /cdr ���õ�ǰĿ¼
	echo /? /help �鿴����
	goto :eof
:next

::��ʼ���
cd "%project_home%"
call %mvn% clean install -Dmaven.test.skip=true

::��Ҫ�滻���ļ�Ŀ¼
set "setting_dir=%replace_dir%\%server_ip%"

if not exist %original_war% (
	echo ���ʧ�ܣ�
	pause 1>nul
	goto :eof
)

rd /s /q "%webapps%" 2>nul
md "%webapps%"
del /f /s %deploy_zip% 2>nul
del /f /s %read_me% 2>nul
copy %original_war% %deploy_zip% 2>nul
if exist %final_zip% (
	del /f /s %final_zip% 1>nul
)

if not exist "%setting_dir%" (
	echo %setting_dir%���ò����ڣ�
	pause 1>nul
	goto :eof
)

echo ��ʼ�滻 %server_ip% �����ļ�
echo �˰����� %server_ip% ���� > %read_me%
md "%project_fd%"
cd "%project_fd%"
call %seven_z% x %deploy_zip% 1>nul
del /f /s %deploy_zip% 1>nul
xcopy /y /e "%setting_dir%" "%project_fd%" 1>nul

::������lib�� δ��ѡʱ
if "%include_lib%" == "n" ( goto :del_lib ) else ( goto :after_del_lib)
:del_lib
	echo ɾ�� lib ��
	echo ������ lib �� >> %read_me%
	rd /s /q "%project_fd%\WEB-INF\lib" 1>nul
:after_del_lib

::�����������ļ� δ��ѡʱ
if "%include_config%" == "n" ( goto :del_config ) else ( goto :after_del_config )
:del_config
	echo ɾ�� �����ļ�
	echo ������ ���������ļ� >> %read_me%
	if exist "%project_fd%\WEB-INF\classes\spring" (
		rd /s /q "%project_fd%\WEB-INF\classes\spring"
	)
    if exist "%project_fd%\WEB-INF\spring" (
        rd /s /q "%project_fd%\WEB-INF\spring"
    )
	del /f /s /q "%project_fd%\WEB-INF\classes\*.properties"
	del /f /s /q "%project_fd%\WEB-INF\classes\*.xml"
	del /f /s /q "%project_fd%\WEB-INF\classes\*.xsd"
	if exist "%project_fd%\WEB-INF\web.xml" (
		del /f /s "%project_fd%\WEB-INF\web.xml"
	)
	if exist "%project_fd%\META-INF" (
		rd /s /q "%project_fd%\META-INF"
	)
:after_del_config

::����jsp/js/html�ļ� δ��ѡʱ
if "%include_web_file%" == "n" ( goto :del_web_file) else ( goto :after_del_web_file)
:del_web_file
	echo ɾ�� jsp/js/html/css/img�ļ�
	echo ������ ����jsp/js/html/css/img�ļ� >> %read_me%
	if exist "%project_fd%\css" (
		rd /s /q "%project_fd%\css"
	)
	if exist "%project_fd%\js" (
		rd /s /q "%project_fd%\js"
	)
	if exist "%project_fd%\img" (
		rd /s /q "%project_fd%\img"
	)
	if exist "%project_fd%\WEB-INF\pages" (
		rd /s /q "%project_fd%\WEB-INF\pages"
	)
	if exist "%project_fd%\favicon.ico" (
		del /f /s "%project_fd%\favicon.ico"
	)

:after_del_web_file

::����class�ļ� δ��ѡʱ
if "%include_byte_code%" == "n" ( goto :del_byte_code) else ( goto :after_del_byte_code)
:del_byte_code
	echo ɾ�� class�ļ�
	echo ������ ����class�ļ� >> %read_me%
	if exist "%project_fd%\WEB-INF\classes\com" (
		rd /s /q "%project_fd%\WEB-INF\classes\com"
	)
:after_del_byte_code

echo �����%username% >> %read_me%
echo ���ʱ�䣺%date:~0,10% %time:~0,8% >> %read_me%
call %SEVEN_Z% a %final_zip% "%project_fd%" 1>nul
call %SEVEN_Z% a %final_zip% %read_me% 1>null
cd "%current_dir%"
rd /s /q "%webapps%" 2>nul

echo ����ɹ�
:eof
cd "%current_dir%"
