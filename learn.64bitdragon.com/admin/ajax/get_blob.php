<?php
    header('Content-Type: application/json');
    require("../functions.php");

    $conn = connectToDB();

    $blobID = $_GET["blobid"];
	$sql = "SELECT ArticleBlobID, Name, Path, Description, Keywords, Type FROM ArticleBlob WHERE ArticleBlobID = ".$blobID;
    $result = mysqli_query($conn, $sql);
	                
    $blob = mysqli_fetch_assoc($result);
	echo "{";
	echo "\"blobID\":".$blob["ArticleBlobID"].",";
    echo "\"Name\":\"".$blob["Name"]."\"";
    echo "}";
?>