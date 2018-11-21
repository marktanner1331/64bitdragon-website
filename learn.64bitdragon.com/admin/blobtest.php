<html>
<head>
	<meta charset="utf-8">
	<meta name="description" content="blob test">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<title>Blob Test</title>

	<link rel="stylesheet" href="/css/blob.css" />
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">

	<!--[if lt IE 9]>
		<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
		<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
	<![endif]-->
	<script src="https://code.jquery.com/jquery-1.11.3.min.js" crossorigin="anonymous"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
</head>
<body>
<div class="container" style="max-width:768px;">
	<div class="row">
		<?php
		if(isset($_GET["path"])) {
			include 'blobs/'.$_GET["blob"].'.html';
		}
		else {
			require("functions.php");
			$conn = connectToDB();
			$blob = fetchBlob($conn, $_GET["blobid"]);
			include 'blobs/'.$blob["Path"].'.html';
		}
		?>
	</div>
</div>
</body>
</html>