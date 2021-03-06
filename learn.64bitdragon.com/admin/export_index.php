﻿<?php
	require("functions.php");
	$conn = connectToDB();
?>

<!DOCTYPE html>
<html>
<head>
    <title>64bitdragon</title>
    <meta charset="utf-8">
    <meta name="description" content="Computer Science and Mathematics Learning Resources">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="author" content="Mark Tanner">
    <meta name="keywords" content="Mathematics,Computer Science">

    <link rel="stylesheet" href="/css/site.css" />
    <link rel="stylesheet" href="/css/home.css" />
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
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-113657998-2');
    </script>

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script src="/js/bootstrap.min.js"></script>

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
        <div class="row">
            <div class="col-md-6">
                <div id="topArticles" class="homePanel" style="margin-bottom:25px">
                    <div class="homePanelHeader">
                        Top Articles
                    </div>
                    <div class="homePanelItem">
                        <a href="/articles/mean">
                            <header>Mean</header>
                            <div>Find the average of a set of numbers.</div>
                        </a>
                    </div>
                    <div class="homePanelItem">
                        <a href="/articles/mean_absolute_error">
                            <header>Mean Absolute Error</header>
                            <div>Determine the error between two sets using the Mean Absolute Error.</div>
                        </a>
                    </div>
                    <div class="homePanelItem">
                        <a href="/articles/root_mean_squared_error">
                            <header>Root Mean Squared Error</header>
                            <div>Determine the error between two sets using the Root Mean Squared Error.</div>
                        </a>
                    </div>
                    <div class="homePanelItem">
                        <a href="/articles/matrices">
                            <header>Matrices</header>
                            <div>Introduction to Matrices.</div>
                        </a>
                    </div>
                    <div class="homePanelItem">
                        <a href="/articles/elements_in_a_matrix">
                            <header>Elements In A Matrix</header>
                            <div>How to refer to elements in a matrix.</div>
                        </a>
                    </div>
                </div>
                <div id="newArticles" class="homePanel" style="margin-bottom:25px">
                    <div class="homePanelHeader">
                        New Articles
                    </div>
					<?php
						$blobs = loadMostRecentBlobs($conn);

						for($i = 0; $i < count($blobs); $i++) {
							$row = $blobs[$i];

							$name = $row["Name"];
							$name = strtolower($name);
							$name = str_replace(" ", "_", $name);

							echo "<div class=\"homePanelItem\">";
							echo "<a href=\"/articles/".$name."\">";
							echo "<header>".$row["Name"]."</header>";
							echo "<div>".$row["Description"]."</div>";
							echo "</a>";
							echo "</div>";
						}
					?>
                </div>
            </div>
            <div class="col-md-6">
                <div id="categories" class="homePanel">
                    <div class="homePanelHeader">
                        Categories
                    </div>
                    <a href="/mathematics">
                        <div class="homePanelItem">
                            Mathematics
                        </div>
                    </a>
                    <a href="/computer_science">
                        <div class="homePanelItem">
                            Computer Science
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
