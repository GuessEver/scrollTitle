<?php
$conn_hostname = "localhost";
$conn_database = "scrollTitle";
$conn_username = "scrollTitle";
$conn_password = "FvzMEcWL6TBEuQRt";
date_default_timezone_set('PRC');
@mysql_connect($conn_hostname, $conn_username, $conn_password)
or die('failed connecting mysql');
@mysql_select_db($conn_database)
or die('failed connecting database');
mysql_query('SET NAMES UTF8');
