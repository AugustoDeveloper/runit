RunIt - It's a simple way to run Windows Applications with configured Windows Authentication
=====================================================

Releases Notes
--------------
1.0.0
- Added in configuration file a section to map which applications the users will run
- Added in configuration file a section to map which credential the applications can use to 'Run as' on Windows
- Added functionality to wait 'Run as' result, this way the user can see if some trouble has occurred at run.

Features
--------
RunIt automate the process of 'Run as' Authentication with simple settings:

Set RunIt section in configuration file
---------------------------------------

~~~xml
<configSections>
    <section name="enviroment" type="RunIt.Infra.Configuration.EnviromentConfigurationSection, RunIt"/>
</configSections>
~~~

Create a enviroment
-------------------
~~~xml
<enviroment>
</enviroment>
~~~

Set a new application in enviroment
-----------------------------------
~~~xml
<applications>
    <add alias="" filename="" name=""/>
</applications>
~~~

- alias: this required attribute allows to RunIt run without directory and identify the application
- filename: this required attribute is a full filename with directory is locate de application
- name: this attribute describe the application.

Exemple Usage:
~~~xml
<enviroment>
    <applications>
        <add alias="sqlm" filename="%programfiles%/.../SqlManaagementStudio.exe" name="SQL Management Studio"/>
    </applications>
</enviroment>
~~~

Set a new credential in enviroment
----------------------------------
~~~xml
<enviroment>
  <credentials>
    <add name="" username="" password="" domain="" />
  </credentials>
</enviroment>
~~~
- name: this required attribute identify a credential
- username: this required attribute is a username to authentication on Windows 'Run as'
- password: this required attribute is a password to authentication on Windows 'Run as'
- domain: this required attribute is a domain to authentication on Windows 'Run as'

Example Usage:
~~~xml
<enviroment>
  <credentials>
    <add name="dev" username="augusto.mesquita" password="l4zyp4ssw0rd" domain="MyWindowsDomain" />
  </credentials>
</enviroment>
~~~

This way we have a configuration

~~~xml
<enviroment>
    <applications>
        <add alias="sqlm" filename="%programfiles%/.../SqlManaagementStudio.exe" name="SQL Management Studio"/>
    </applications>
  <credentials>
        <add name="dev" username="augusto.mesquita" password="l4zyp4ssw0rd" domain="MyWindowsDomain" />
  </credentials>
</enviroment>
~~~

Finally, Run It!
----------------
Open the 'CMD' and go to RunIt assmbly folder, execute a command:
~~~console
bin > RunIt.exe -e sqlm dev
~~~

    
    
















