<?php
if(isset($_POST['text']) && $_POST['text'] !== '') {
	require_once('connection.php');
	if(strlen($text) > 30) $text = substr($text, 0, 30);
	$text = urlencode($_POST['text']);
	$sql = $pdo->prepare('INSERT INTO `title` (`content`, `status`) VALUES (:content, :status);');
	$sql->bindValue(':content', $text, PDO::PARAM_STR);
	$sql->bindValue(':status', 0, PDO::PARAM_INT);
	$sql->execute();
	header('Location: index.php?result=success');
}
?>
<!DOCTYPE html>
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
		<p>输入文字，点击发射，引爆全场！！！</p>
		<form action="" method="post">
			<p><input type="text" name="text" maxlength="30" autofocus></p>
			<p><input type="submit" value="发射"></p>
		</form>
		<p>
		<?php
		$messages = json_decode(file_get_contents('quickMessages.json'), true);
		foreach($messages as $message) { ?>
			<button type="button" onclick="send('<?php echo $message;?>')"><?php echo $message; ?></button>
		<?php } ?>
		</p>
		<?php
		if(isset($_GET['result']) && $_GET['result'] === 'success') {
			echo '<p style="color:red;">弹幕发射成功, ' . date('Y-m-d H:i:s', time()) . '</p>';
		}
		?>
		<script>
		function send(message) {
			document.querySelector('form input[type=text]').value = message;
			document.querySelector('form').submit();
		}
		</script>

	</body>
</html>
