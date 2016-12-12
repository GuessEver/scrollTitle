<?php
require_once('connection.php');
$token = 'hnf4i3y58wn932e432'; // change it if you want

if(!isset($_GET['token']) || $_GET['token'] !== $token) return;
$sql = $pdo->prepare('SELECT `id`, `content` FROM `title` WHERE `status` = 0;');
$sql->execute();
$titles = $sql->fetchall(PDO::FETCH_ASSOC);
$res = [];
foreach ($titles as $title) {
	array_push($res, urldecode($title['content']));
	$sql2 = $pdo->prepare('UPDATE `title` SET `status` = 1 WHERE `id` = :id;');
	$sql2->bindValue(':id', $title['id']);
	$sql2->execute();
}
echo join($res, "\n");
