@echo off

::需要配置的参数
::最后打包的文件名与pom一致 /pn 配置
set "project_name=${projectName}"
::需要替换的目录replace目录下的文件夹 /s 配置
set "server_ip=${ip}"
::是否包含库 /il 配置
set include_lib=${includeLib}
::是否包含所有的配置文件 /ic 配置
set include_config=${includeConfig}
::是否包含js/jsp/html文件 /iwf 配置
set include_web_file=${includeWebFile}
::是否包含class文件 /ibc 配置
set include_byte_code=${includeByteCode}
::设置当前目录 /cdr 配置
set "cdr=${currentDirectory}"
set "current_dir=%~dp0"
::服务器 ip 检测用于替换配置文件 /srs 配置
set "servers=${servers}"

set has_replace=n
for %%i in (%servers%) do (
	if %%i == %server_ip% ( set has_replace=y )
) 
if "%has_replace%" == "n" (
	echo 服务器ip校验失败！请确认配置正确
	pause 1>nul
	goto :eof
)

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

::设置可运行文件路径
set mvn="%current_dir%apache-maven-3.3.9\bin\mvn"
set seven_z="%current_dir%7-Zip\7z"
::非固定参数
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
	echo /s 打包名称 选要替换的文件
	echo /il y/n 选择是否包 lib 包
	echo /ic y/n 选择是否包含配置文件
	echo /iwf y/n 选择是否包含 js/jsp/html 文件
	echo /ibc y/n 选择是否包含 class 文件
	echo /cdr 设置当前目录
	echo /? /help 查看帮助
	goto :eof
:next

::开始打包
cd "%project_home%"
call %mvn% clean install -Dmaven.test.skip=true

::需要替换的文件目录
set "setting_dir=%replace_dir%\%server_ip%"

if not exist %original_war% (
	echo 打包失败！
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
	echo %setting_dir%配置不存在！
	pause 1>nul
	goto :eof
)

echo 开始替换 %server_ip% 配置文件
echo 此包包含 %server_ip% 配置 > %read_me%
md "%project_fd%"
cd "%project_fd%"
call %seven_z% x %deploy_zip% 1>nul
del /f /s %deploy_zip% 1>nul
xcopy /y /e "%setting_dir%" "%project_fd%" 1>nul

::当包含lib包 未勾选时
if "%include_lib%" == "n" ( goto :del_lib ) else ( goto :after_del_lib)
:del_lib
	echo 删除 lib 包
	echo 不包含 lib 包 >> %read_me%
	rd /s /q "%project_fd%\WEB-INF\lib" 1>nul
:after_del_lib

::当包含配置文件 未勾选时
if "%include_config%" == "n" ( goto :del_config ) else ( goto :after_del_config )
:del_config
	echo 删除 配置文件
	echo 不包含 所有配置文件 >> %read_me%
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

::当包jsp/js/html文件 未勾选时
if "%include_web_file%" == "n" ( goto :del_web_file) else ( goto :after_del_web_file)
:del_web_file
	echo 删除 jsp/js/html/css/img文件
	echo 不包含 所有jsp/js/html/css/img文件 >> %read_me%
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

::当包class文件 未勾选时
if "%include_byte_code%" == "n" ( goto :del_byte_code) else ( goto :after_del_byte_code)
:del_byte_code
	echo 删除 class文件
	echo 不包含 所有class文件 >> %read_me%
	if exist "%project_fd%\WEB-INF\classes\com" (
		rd /s /q "%project_fd%\WEB-INF\classes\com"
	)
:after_del_byte_code

echo 打包：%username% >> %read_me%
echo 打包时间：%date:~0,10% %time:~0,8% >> %read_me%
call %SEVEN_Z% a %final_zip% "%project_fd%" 1>nul
call %SEVEN_Z% a %final_zip% %read_me% 1>null
cd "%current_dir%"
rd /s /q "%webapps%" 2>nul

echo 打包成功
:eof
cd "%current_dir%"
