
# A Simple Grade System 

## Requirements
1. linux system
2. mono
3. Don't need IDE
4. MySQl [How to connect mysql by c#](http://http://www.coderzs.top/2018/04/20/mysql_ssl/)


## Functions
1. 后台添加老师的数据，登录信息，课程信息
2. 教师登录
3. 学生的成绩情况及分析

## Screenshot

### Login
![Login](https://raw.githubusercontent.com/stevenshuang/WinFormDemo/master/images/login.png)

### MainWindow
![MainWindow](https://raw.githubusercontent.com/stevenshuang/WinFormDemo/master/images/MainWindow.png)

### Analysis
![Analysis](https://raw.githubusercontent.com/stevenshuang/WinFormDemo/master/images/Analysis.png)

### Others in images folder


## Compile Program
1. mcs /t:library `cat pdll` Program.cs

## Form, MainWindow, Model, Common编译为动态链接库
1. mcs /t:library `cat Modeldll`  Model.cs
2. mcs /t:library `cat Formdll` Form.cs
3. mcs /t:library `cat Maindll` MainWindow.cs
4. mcs /t:library Common.cs

