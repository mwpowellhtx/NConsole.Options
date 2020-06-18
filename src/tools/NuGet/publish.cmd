@echo off

setlocal

rem We do not publish the API key as part of the script itself.
if "%my_nuget_api_key%"=="" (
    echo You are prohibited from making these sorts of changes.
    goto :end
)

rem Default list delimiter is Comma (,).
:redefine_delim
if "%delim%" == "" (
    set delim=,
)
rem Redefine the delimiter when a Dot (.) is discovered.
rem Anticipates potentially accepting Delimiter as a command line arg.
if "%delim%" == "." (
    set delim=
    goto :redefine_delim
)

rem Go ahead and pre-seed the Projects up front.
:set_projects
set projects=
rem Setup All Projects
set all_projects=NConsole.Options
rem Setup Options Projects.
set options_projects=NConsole.Options


:parse_args

rem Done parsing the args.
if "%1" == "" (
    goto :end_args
)

:set_destination
if "%1" == "--local" (
    set dest=local
    goto :next_arg
)
if "%1" == "--nuget" (
    set dest=nuget
    goto :next_arg
)

:set_drive_letter
if "%1" == "--drive" (
    set xcopy_destination_drive=%2
    shift
    goto :next_arg
)
if "%1" == "--drive-letter" (
    set xcopy_destination_drive=%2
    shift
    goto :next_arg
)

:set_dry_run
if "%1" == "--dry" (
    set dry=true
    goto :next_arg
)
if "%1" == "--dry-run" (
    set dry=true
    goto :next_arg
)
if "%1" == "--no-dry" (
    set dry=false
    goto :next_arg
)
if "%1" == "--no-dry-run" (
    set dry=false
    goto :next_arg
)

:set_should_pause
if "%1" == "--pause" (
    set should_pause=true
    goto :next_arg
)
if "%1" == "--no-pause" (
    set should_pause=false
    goto :next_arg
)

:set_config
if "%1" == "--config" (
    set config=%2
    shift
    goto :next_arg
)

:add_options_projects
if "%1" == "--options" (
    if "%projects%" == "" (
        set projects=%options_projects%
    ) else (
        set projects=%projects%%delim%%options_projects%
    )
	goto :next_arg
)
if "%1" == "--opts" (
    if "%projects%" == "" (
        set projects=%options_projects%
    ) else (
        set projects=%projects%%delim%%options_projects%
    )
	goto :next_arg
)
if "%1" == "--o" (
    if "%projects%" == "" (
        set projects=%options_projects%
    ) else (
        set projects=%projects%%delim%%options_projects%
    )
	goto :next_arg
)

:add_all_projects
if "%1" == "--all" (
    rem Prepare to publish All Projects.
    set projects=%all_projects%
    goto :next_arg
)

:add_project
if "%1" == "--project" (
    rem Add a Project to the Projects list.
    if "%projects%" == "" (
        set projects=%2
    ) else (
        set projects=%projects%%delim%%2
    )
    shift
    goto :next_arg
)

:next_arg

shift

goto :parse_args

:end_args

:verify_args

:verify_projects

if "%projects%" == "" (
    rem In which case, there is nothing else to do.
    echo Nothing to process.
    goto :end
)

:verify_config
if "%config%" == "" (
    rem Assumes Release Configuration when not otherwise specified.
    set config=Release
)

:verify_destination
if "%dest%" == "" (
    set dest=local
)

:verify_should_pause
if "%should_pause%" == "" (
    set should_pause=true
)

:publish_projects

:set_vars
set xcopy_exe=xcopy.exe
rem Have to re-set the options for whatever reason.
set xcopy_opts=
set xcopy_opts=%xcopy_opts% /Y /F
if "%xcopy_destination_drive%" == "" set xcopy_destination_drive=F:
set xcopy_destination_dir=%xcopy_destination_drive%\Dev\NuGet\local\packages

rem Expecting NuGet to be found in the System Path.
set nuget_exe=NuGet.exe
set nuget_push_verbosity=detailed
set nuget_push_source=https://api.nuget.org/v3/index.json

rem Ditto clearing the value first.
set nuget_push_opts=
set nuget_push_opts=%nuget_push_opts% %my_nuget_api_key%
set nuget_push_opts=%nuget_push_opts% -Verbosity %nuget_push_verbosity%
set nuget_push_opts=%nuget_push_opts% -NonInteractive
set nuget_push_opts=%nuget_push_opts% -Source %nuget_push_source%

rem Do the main areas here.
pushd ..\..

if not "%projects%" == "" (
    echo Processing '%config%' configuration for '%projects%' ...
)
:next_project
if not "%projects%" == "" (
    for /f "tokens=1* delims=%delim%" %%p in ("%projects%") do (
        if "%dest%"=="local" (
            call :copy_local %%p
        )
        if "%dest%"=="nuget" (
            call :publish_nuget %%p
        )
        set projects=%%q
        goto :next_project
    )
)

popd

goto :end

:copy_local
for %%f in ("%1\bin\%config%\%1.*.nupkg") do (
    if "%dry%" == "true" (
        echo Dry run: %xcopy_exe% "%%f" "%xcopy_destination_dir%" %xcopy_opts%
    ) else (
        echo Running: %xcopy_exe% "%%f" "%xcopy_destination_dir%" %xcopy_opts%
        %xcopy_exe% "%%f" "%xcopy_destination_dir%" %xcopy_opts%
    )
)
exit /b

:publish_nuget
for %%f in ("%1\bin\%config%\%1.*.nupkg") do (
    if "%dry%" == "true" (
        echo Dry run: %nuget_exe% push "%%f" %nuget_push_opts%
    ) else (
        echo Running: %nuget_exe% push "%%f" %nuget_push_opts%
        %nuget_exe% push "%%f" %nuget_push_opts%
    )
)
exit /b

:end

endlocal

if "%should_pause%" == "true" (
    pause
)
