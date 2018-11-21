<?php
	require("functions.php");

	$typeID = $_GET["type"];

	$conn = connectToDB();

	$sql = "SELECT Name FROM articleblobtype WHERE ArticleBlobTypeID = ".$typeID;
    $result = mysqli_query($conn, $sql);

	$type = mysqli_fetch_assoc($result);
?>
<!DOCTYPE html>
<html>
<head>
    <title>64bitdragon - <?php echo $type["Name"]; ?></title>
    <meta charset="utf-8">
    <meta name="description" content="Computer Science and Mathematics Learning Resources">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link rel="stylesheet" href="/css/site.css" />
    <link rel="stylesheet" href="/css/home.css" />
    <link rel="stylesheet" href="/css/bootstrap.min.css">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!--<script src="/js/jquery-1.11.3.min.js" crossorigin="anonymous"></script>-->
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script src="/js/bootstrap.min.js"></script>

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-113657998-2"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-113657998-2');
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#search").autocomplete({
                source: "search_ajax.php",
                minLength: 2,
                select: function (event, ui) {
                    var url = "/articles/" + ui.item.value.replace(/ /g, "_").toLowerCase();
                    window.location.href = url;
                }
            });
        });
    </script>

	<style type="text/css">
        .row {
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <div class="siteHead">
        <span style="margin-right:15px;" class="logo">
            64bitdragon
        </span>
        <a href="/">
            <span id="home" class="siteHeadButton">
                HOME
            </span>
        </a>
        <a href="/mathematics">
            <span id="statistics" class="siteHeadButton">
                Mathematics
            </span>
        </a>
        <a href="/computer_science">
            <span id="statistics" class="siteHeadButton">
                Computer Science
            </span>
        </a>
        <input type="text" id="search" placeholder="Search..">
    </div>
    <div class="container" style="top:70px;position:relative;">
	<?php
		$sql = "SELECT ArticleBlobTypeID, Name FROM articleblobtype WHERE ParentTypeID = ".$typeID;
		$result = mysqli_query($conn, $sql);
        
		while($row = mysqli_fetch_assoc($result)) {
			printArticlesWithType($conn, $row["Name"], $row["ArticleBlobTypeID"]);
		}

		function printArticlesWithType($conn, $typeName, $typeID) {
			$blobs = getCompletelyFinishedBlobsWithType($conn, $typeID);
			if(count($blobs) == 0) {
				return;
			}

			echo "<div class=\"row\">";
			echo "<div class=\"homePanelHeader\">";
			echo $typeName;
			echo "</div>";

			for($i = 0; $i < count($blobs); $i++) {
				$blob = $blobs[$i];
				printArticle($blob["Name"], $blob["Description"]);
			}

			echo "</div>";
		}

		function printArticle($name, $description) {
			$path = "/articles/".strtolower(str_replace(" ", "_", $name));

			echo "<div class=\"homePanelItem\">";
			echo "<a href=\"".$path."\">";
			echo "<header>".$name."</header>";
			echo "<div>".$description."</div>";
			echo "</a>";
			echo "</div>";
		}
	?>
        <div style="padding-bottom:25px;"></div>
    </div>
</body>
</html>
