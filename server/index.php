<html>
	<head>
		<meta charset="utf-8">
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
		<title>Happy New Year!</title>
	</head>
	<body align="center">
		<h3>数学科学学院2015-2016迎新晚会</h3>
		<h2>弹幕君</h2>
		<hr>
		<p>啥？弹幕？！这么屌？</p>
		<p>对，我们就是这么屌！</p>
		<p><br></p>
		<p>输入文字，点击发射，引爆全场！！！</p>
		<form action="" method="post">
			<p><input type="text" name="text"></p>
			<p><input type="submit" value="发射"></p>
		</form>
	</body>
</html>
<?php
if(isset($_POST['text']) && $_POST['text'] !== '') {
	require_once('connection.php');
	$text = urlencode($_POST['text']);
	@mysql_query('INSERT INTO `title` 
		(`content`, `status`)
		VALUES
		("'.$text.'", "0")
		;');
	echo '<p style="color:red;">弹幕发射成功</p>';
}
$_POST['text'] = '';
