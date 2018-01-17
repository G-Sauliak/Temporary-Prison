cd %windir%\system32\inetsrv

set WEB_SITE=c:\site
set WCF_SERVICE=c:\service

appcmd add site /name:"service" /bindings:http://*:3333 /physicalPath:"%WCF_SERVICE%"
appcmd add site /name:"PrisonSite" /bindings:http://*:4444 /physicalPath:"%WEB_SITE%"
pause