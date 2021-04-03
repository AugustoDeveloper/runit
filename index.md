RunIt - It's a simple way to run Windows Applications with configured Windows Authentication
=====================================================

Releases Notes
--------------
[Releases](https://github.com/AugustoDeveloper/repository-runit/releases)

Features
--------
RunIt automate the process of 'Run as' Authentication with simple settings:

Create a enviroment
-------------------

~~~json
"Env": {
  
}
~~~

Set a new application in enviroment
-----------------------------------
~~~json
"Applications": [{
            "Name": "Application name",
            "Alias": "Application alias",
            "Filename": "Application path with file name"
        }]
~~~

- Alias: this required attribute allows to RunIt run without directory and identify the application
- Filename: this required attribute is a full filename with directory is locate de application
- Name: this attribute describe the application.

Exemple Usage:
~~~json
"Env": {
  "Applications": [{
          "Name": "Visual Studio 2019",
          "Alias": "vs",
          "Filename": "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Professional\\Common7\\IDE\\devenv.exe"
      }]
}
~~~

Set a new domain in enviroment
----------------------------------
~~~json
"Domains" : [{
          "Name": "Domain name",
          "Username": "Username to authentication on domain",
          "Password": "Password to authentication on domain",
          "Alias": "Unique identification of domain"
      }],
~~~
- name: this required attribute identify a credential
- username: this required attribute is a username to authentication on Windows 'Run as'
- password: this required attribute is a password to authentication on Windows 'Run as'
- domain: this required attribute is a domain to authentication on Windows 'Run as'

Example Usage:
~~~json
"Env": {
        "Domains" : [{
            "Name": "test",
            "Username": "test",
            "Password": "teste",
            "Alias": "tt"
        }],
}
~~~

This way we have a configuration

~~~json
{
    "Env": {
        "Domains" : [{
            "Name": "test",
            "Username": "test",
            "Password": "teste",
            "Alias": "tt"
        }],
        "Applications": [{
            "Name": "Visual Studio 2019",
            "Alias": "vs",
            "Filename": "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Professional\\Common7\\IDE\\devenv.exe"
        },{
            "Name": "SQL Management",
            "Alias": "sql",
            "Filename": "C:\\Program Files (x86)\\Microsoft SQL Server Management Studio 18\\Common7\\IDE\\Ssms.exe"
        }]
    }
}
~~~

Finally, Run It!
----------------
Open the 'CMD' and go to RunIt assmbly folder, execute a command:
~~~console
bin > RunIt.exe -d tt -a sql
~~~

If need see log of the app use verbose arg `-v`:
~~~console
bin > RunIt.exe -v -d tt -a sql
trce: RunIt.Program[0]
      Domain St0n3.123123Zxcasqw...
trce: RunIt.Program[0]
      Aplication Visual Studio 2019...
trce: RunIt.Program[0]
      Authenticating the Visual Studio 2019 with NPADSTONE\augusto.mesquita...
File: C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\devenv.exe...
trce: RunIt.Program[0]
      Executing ...C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\devenv.exe
~~~

    
    
















