# EasyAdminBlazor 

## 一、项目概述
### 项目简介
`EasyAdminBlazor` 是一个基于 .NET 9.0 的项目，用于开发管理后台相关功能。此项目运用 `BootstrapBlazor` 组件库搭建用户界面，借助 `FreeSql` 进行数据访问操作，本项目借鉴了AdminBlazor底层代码，前端全面使用BootstrapBlazor，前台使用Razor pages，可快速完成中小型项目，个人接单利器。

### 主要功能
- **用户管理**：包括用户信息的增删改查、导出。
- **角色管理**：管理用户角色，包括角色信息的增删改查，角色菜单权限的分配。
- **菜单管理**：管理系统菜单，包括菜单信息的增删改查。
- **部门管理**：管理部门信息，包括部门信息的增删改查。
- **参数配置**：管理系统参数，包括参数信息的增删改查。
- **字典管理**：管理字典信息，包括字典信息的增删改查。
- **日志管理**：管理系统登录日志。
- **代码生成**：根据实体类生成对应管理页面。
- **多数据库**：支持多种数据库。

## 二、运行说明
### 运行环境
- **.NET SDK**：9.0

### 运行步骤
vs2022打开EasyAdminBlazor.sln，然后在EasyAdminBlazor.Test项目下的wwwroot文件夹右键“在浏览器中查看”

## 三、系统截图

![login](https://gitee.com/gudufy/EasyAdminBlazor/raw/master/images/login.png)
![user](https://gitee.com/gudufy/EasyAdminBlazor/raw/master/images/user.png)
![setting](https://gitee.com/gudufy/EasyAdminBlazor/raw/master/images/setting.png)
![role](https://gitee.com/gudufy/EasyAdminBlazor/raw/master/images/role.png)
![org](https://gitee.com/gudufy/EasyAdminBlazor/raw/master/images/org.png)
![menu](https://gitee.com/gudufy/EasyAdminBlazor/raw/master/images/menu.png)
![loginlog](https://gitee.com/gudufy/EasyAdminBlazor/raw/master/images/loginlog.png)
![dict](https://gitee.com/gudufy/EasyAdminBlazor/raw/master/images/dict.png)

## 四、本项目在以下项目基础上开发
[FreeSql](https://freesql.net/)
[BootstrapBlazor](https://www.blazor.zone/)
[AdminBlazor](https://freesql.net/guide/AdminBlazor.html)