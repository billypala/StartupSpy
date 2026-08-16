# StartupSpy（汉化版）

> 一个现代化的 Windows 启动项管理器，使用 C# 与 WPF（.NET 8）构建

![Platform](https://img.shields.io/badge/platform-Windows-blue)
![Framework](https://img.shields.io/badge/.NET-8.0-purple)
![Language](https://img.shields.io/badge/language-C%23-green)
![License](https://img.shields.io/badge/license-MIT-lightgrey)

StartupSpy 是一款界面简洁、采用深色主题风格的 Windows 桌面应用程序，可扫描并展示开机时自动启动的所有项目——包括注册表项、启动文件夹、计划任务与服务，并以颜色标记进行风险评估。

---

![StartupSpy 汉化版截图](Screenshot-zh.png)

## 汉化说明

本仓库是 [Arman-Ispiryan/StartupSpy](https://github.com/Arman-Ispiryan/StartupSpy) 的**简体中文汉化版**（fork 自上游，仅替换展示文本，程序逻辑与上游保持一致）。

- **汉化范围**：界面文案、按钮、菜单、表单标签、占位符、提示与错误信息、通知、窗口标题、安装脚本、README 文档等所有面向用户的字符串均已中文化。
- **保持不变**：命名空间、类名、变量名、枚举、注册表键、文件路径、配置键、API 字段、环境变量、第三方库调用等代码标识符一律原样保留。
- **统一术语**：注册表 / 启动文件夹 / 计划任务 / 服务；安全 / 低 / 中 / 高 / 未知；发布者 / 路径 / 命令 / 风险等级 等。
- **中文适配**：UTF-8 编码、中文字体回退（`Segoe UI, Microsoft YaHei`）、文本溢出处理（`TextTrimming` / `TextWrapping`）。
- **预编译单文件可执行程序**：见本仓库 **Releases** 页面，提供两个版本：
  - `StartupSpy-nat8.exe`：自包含单文件（内置 .NET 8 运行时），**免安装、双击即跑**，约 156 MB。
  - `StartupSpy-nonat8.exe`：依赖框架单文件，需本机已装 **.NET 8 桌面运行时**，体积仅约 1.5 MB。

## 功能特性

- **全量启动项扫描**，覆盖全部 4 类来源：
  - `HKLM` 与 `HKCU` 注册表 Run 键
  - 用户与公共「启动」文件夹
  - 登录触发的计划任务
  - 自动启动的 Windows 服务
- **风险评估** —— 依据发布者与文件是否存在，划分为 安全 / 低 / 中 / 高 / 未知
- **实时筛选** —— 按类别筛选，并支持按名称、发布者、路径进行自由文本搜索
- **详情面板** —— 完整路径、命令、注册表位置、描述、状态
- **快捷操作** —— 在资源管理器中打开文件位置、将路径复制到剪贴板
- **高风险快捷筛选** —— 一键列出所有中高风险启动项
- **现代化深色界面** —— 完全使用 WPF 构建，不依赖任何第三方 UI 库

---

## 快速开始

### 环境要求

- Windows 10/11
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022（Community 社区版免费）

### 构建与运行

```bash
git clone https://github.com/billypala/StartupSpy-zh-CN.git
cd StartupSpy
dotnet build
dotnet run
```

> **提示：** 请以管理员身份运行，以获得对所有注册表项、服务与计划任务的完全访问权限。应用程序清单已配置为自动请求提权。

### 构建发布版可执行文件

**方式一：自包含单文件（内置 .NET 8 运行时，免安装）**

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true
```

输出单个 `.exe`，位于 `bin/Release/net8.0-windows/win-x64/publish/StartupSpy.exe`。该 `.exe` 已内嵌 .NET 8 运行时与 WPF 原生依赖，首次运行自动解压原生库到临时目录，目标机无需安装 .NET。

**方式二：依赖框架单文件（体积小，需本机已装 .NET 8 桌面运行时）**

```bash
dotnet publish -c Release -p:PublishSingleFile=true --self-contained false
```

> **WPF 重要提示**：必须加 `IncludeNativeLibrariesForSelfExtract=true`（方式一）才能打包为真正单文件；否则 WPF 原生 DLL 会作为独立文件放在 `.exe` 旁边。

---

## 项目结构

```
StartupSpy/
├── Models/
│   └── StartupEntry.cs          # 数据模型，含风险/类别枚举
├── ViewModels/
│   └── MainViewModel.cs         # MVVM 视图模型、扫描逻辑、筛选逻辑
├── Services/
│   └── StartupScannerService.cs # 全部扫描逻辑（注册表、任务、服务）
├── Converters/
│   └── Converters.cs            # WPF 值转换器（风险配色、图标等）
├── MainWindow.xaml              # 完整界面布局
├── MainWindow.xaml.cs           # 后台代码（极简 —— 仅含筛选事件）
├── App.xaml / App.xaml.cs
└── StartupSpy.csproj
```

---

## 风险评估逻辑

| 等级 | 判定条件 |
|------|----------|
| 安全 | 发布者位于已知安全列表（Microsoft、Google 等） |
| 低   | 已知发布者，但不在安全列表中 |
| 中   | 名称包含「updater」「agent」等关键字 |
| 高   | 文件在磁盘上不存在 |
| 未知 | 无法识别发布者 |

---

## 技术栈

| 技术 | 用途 |
|------|------|
| C# 12 | 编程语言 |
| .NET 8 | 运行时 |
| WPF | UI 框架 |
| MVVM 模式 | 架构模式 |
| TaskScheduler NuGet | 计划任务枚举 |
| WMI / Win32 API | 服务与注册表访问 |

---

## 后续计划

- [ ] 将报告导出为 CSV 或 HTML
- [ ] 启动影响评分（开机延迟估算）
- [ ] 系统托盘与后台监控
- [ ] 针对单项进行 VirusTotal 查询

---

## 许可证

MIT —— 欢迎自由使用、修改与分发。
