<?php
require_once('connection.php');
$token = 'hnf4i3y58wn932e432'; // change it if you want

if(!isset($_GET['token']) || $_GET['token'] !== $token) return;
$sql = @mysql_query('SELECT `tid`, `content` FROM `title` WHERE `status` = "0";');
$res = [];
while(($response = @mysql_fetch_assoc($sql)) !== false) {
	$response['content'] = urldecode($response['content']);
	array_push($res, $response['content']);
	@mysql_query('UPDATE `title` SET `status` = "1" WHERE `tid` = "'.$response['tid'].'";');
}
echo join($res, "\n");
