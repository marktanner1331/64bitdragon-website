<?php
	require("functions.php");

	$blobID = $_GET["blobid"];
    
    $conn = connectToDB();
	$blob = fetchBlob($conn, $blobID);
?>
<html>
<head>
<title><?php echo $blob["Name"]; ?></title>
	<meta charset="utf-8">
	<meta name="description" content="<?php echo $blob["Description"]; ?>">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta name="keywords" content="<?php echo $blob["Keywords"]; ?>">
	<meta name="author" content="Mark Tanner">

	<link rel="stylesheet" href="/css/site.css" />
	<link rel="stylesheet" href="/css/blob.css" />
	<link rel="stylesheet" href="/css/bootstrap.min.css">
	<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

	<!--[if lt IE 9]>
		<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
		<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
	<![endif]-->
	<!--<script src="/js/jquery-1.11.3.min.js" crossorigin="anonymous"></script>-->
	<!-- Global site tag (gtag.js) - Google Analytics -->
	<script async src="https://www.googletagmanager.com/gtag/js?id=UA-113657998-2"></script>
	<script>
	  window.dataLayer = window.dataLayer || [];
	  function gtag(){dataLayer.push(arguments);}
	  gtag('js', new Date());

	  gtag('config', 'UA-113657998-2');
	</script>

	<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
	<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

	<script src="/js/bootstrap.min.js"></script>

	<script type="text/javascript">
	$(document).ready(function() {
		$( "#search" ).autocomplete({
			source: "/search_ajax.php",
			minLength: 2,
			select: function( event, ui ) {
				var url = "/articles/" + ui.item.value.replace(/ /g, "_").toLowerCase();
				window.location.href = url;
			}
		});
	});

		function showHide(sender) {
			var button = $(sender);
			var container = $(button.parent().parent().children()[1]);
			
			if(container.css("display") == "block") {
				container.css("display", "none");
			} else {
				container.css("display", "block");
			}
		}

		function showAll() {
			$("#prerequisites").find(".blob").find(".container").css("display", "block");
		}

		function hideAll() {
			$("#prerequisites").find(".blob").find(".container").css("display", "none");
		}
	</script>
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
<div 
<div class="container" style="max-width:768px;padding-top:70px;">
	<div class="row">
		<?php
			$prerequisites = loadPrerequisites($conn, $blobID);
			
			if(count($prerequisites) > 0) {
				echo "<div class=\"pageHeading\"><header>".$blob["Name"]."</header></div>";
				echo "<div class=\"pageSubHeading\" style=\"padding-bottom:15px\">";
				echo "<span>Prerequisites</span>";
				echo "<div class=\"showHide\" onclick=\"showAll();\">Show All</div>";
				echo "<div class=\"showHide\" style=\"margin-right:7px;\" onclick=\"hideAll();\">Hide All</div>";
				echo "</div>";

				echo "<div id=\"prerequisites\">";
				for($i = 0; $i < count($prerequisites); ++$i) {
					showBlob($conn, $prerequisites[$i], true);
				}

				echo "</div>";

				echo "<div style=\"border-bottom: 1px solid #a2a9b1;\"></div>";
				echo "<br />";
			} else {
				echo "<div class=\"pageHeading\"><header>".$blob["Name"]."</header></div>";
			}
		   
            showBlob($conn, $blobID, false);
            
			$related = getRelated($conn, $blobID);

			if(count($related) > 0) {
				echo "<div class=\"pageSubHeading\" style=\"padding-bottom:15px\">";
				echo "<span>Related</span>";
				echo "</div>";

				for($i = 0; $i < count($related); ++$i) {
					showRelated($conn, $related[$i]);
				}
			}
			
            mysqli_close($conn);
            
            function showBlob($conn, $blobID, $isPrerequisite) {
                $sql = "SELECT Name, Path, Type FROM ArticleBlob WHERE ArticleBlobID = ".$blobID;
                $result = mysqli_query($conn, $sql);
                
                while($row = mysqli_fetch_assoc($result)) {
					echo "<article>";
                    echo "<div class=\"blob\">";
					
					if($isPrerequisite) {
						echo "<div class=\"prerequisiteHeading\">";
					} else {
						echo "<div class=\"blobHeading\">";
					}
					
					echo "<header>".$row["Name"]."</header>";

					if($isPrerequisite) {
						echo "<div class=\"showHide\" onclick=\"showHide(this);\">Show/Hide</div>";
					}

					echo "</div>";
                    include "../blobs/".$row["Path"].".html";
                    echo "</div>";
					echo "</article>";
                }
            }

			function showRelated($conn, $blobID) {
				$sql = "SELECT Name, Description FROM ArticleBlob WHERE ArticleBlobID = ".$blobID;
                $result = mysqli_query($conn, $sql);
                
                $row = mysqli_fetch_assoc($result);
				echo "<div class=\"related\">";
				echo "<a href=\"/articles/".getURLForBlob($row["Name"])."\">";
				echo "<header>".$row["Name"]."</header>";
				echo "<div>".$row["Description"]."</div>";
				echo "</a>";
				echo "</div>";
			}

			function getURLForBlob($blobName) {
				return strtolower(str_replace(" ", "_", $blobName));
			}
		?>
	</div>
</div>
<div style="margin-bottom:25px;"></div>
</body>
</html>