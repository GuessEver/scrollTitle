# scrollTitle
windows桌面弹幕

new version https://github.com/GuessEver/WeDanmaku

## 如何开始
下载完整的zip文件，包含client和server

### server
先导入server/title.sql数据库，然后把server文件夹下connection.php配置好

在server/getData.php中修改token

在server/index.php中修改title等属性

### client
/client/scrollTitle/bin/Release即为可用版本

打开scrollTitle.exe即可

主机名填入你自己的主机地址或ip，以http://或者https://开头（如http://guessever.me/danmu ）  
可以直接在软件目录加入一个名为`info.txt`的文件，第一行为hostname，第二行为token，比如
```
http://guessever.me/danmu
tokendsafdsfdsr222
```

token填入你在服务器上的配置

发射弹幕的地址即为你所填的主机名（如http://guessever.me/danmu ）

## Client使用说明
1. 填入信息 
2. 按下 `保存数据` 按钮
3. 按下 `初始化弹幕` 按钮

若中途修改了数据，再依次按 `保存数据` 和 `初始化弹幕` 按钮即可

## 注意事项

