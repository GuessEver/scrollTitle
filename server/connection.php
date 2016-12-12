<?php
date_default_timezone_set('PRC');
$conn_hostname = 'localhost';
$conn_database = 'scrollTitle';
$conn_username = 'root';
$conn_password = 'root';
try {
	$pdo = new PDO('mysql:host='.$conn_hostname.';dbname='.$conn_database, $conn_username, $conn_password);
	$pdo->exec('SET NAMES UTF8');
}
catch(Exception $e) {
    echo '数据库链接错误！';
    return;
}
