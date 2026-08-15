[Setup]
AppName=StartupSpy
AppVersion=1.0
AppPublisher=Arman Ispiryan
DefaultDirName={autopf}\StartupSpy
DefaultGroupName=StartupSpy
OutputDir=installer
OutputBaseFilename=StartupSpy_Setup
SetupIconFile=app.ico
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "创建桌面快捷方式"; GroupDescription: "附加图标："

[Files]
Source: "bin\Release\net8.0-windows\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "app.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\StartupSpy"; Filename: "{app}\StartupSpy.exe"; IconFilename: "{app}\app.ico"
Name: "{commondesktop}\StartupSpy"; Filename: "{app}\StartupSpy.exe"; IconFilename: "{app}\app.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\StartupSpy.exe"; Description: "启动 StartupSpy"; Flags: shellexec nowait postinstall skipifsilent