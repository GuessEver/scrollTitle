# scrollTitle
windows桌面弹幕

下载完整的zip文件，包含client和server

如何定制？
### server
先导入server/title.sql数据库，然后把server文件夹下connection.php配置好

在server/getData.php中修改token

在server/index.php中修改title等属性

### client
/client/scrollTitle/bin/Release即为可用版本

打开scrollTitle.exe即可

主机名填入你自己的主机地址或ip，以http://或者https://开头（如http://guessever.me/danmu）

token填入你在服务器上的配置

发射弹幕的地址即为你所填的主机名（如http://guessever.me/danmu）

