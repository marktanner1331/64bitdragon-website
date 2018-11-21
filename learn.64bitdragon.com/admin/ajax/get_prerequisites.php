<?php
	header('Content-Type: application/json');
	require("../functions.php");
    $conn = connectToDB();
	
	$blobID = $_GET["blobid"];
	$sql = "SELECT blobprerequisite.ChildBlobID, articleblob.Name, articleblob.Finished FROM blobprerequisite INNER JOIN articleblob ON blobprerequisite.ChildBlobID=articleblob.ArticleBlobID WHERE blobprerequisite.ParentBlobID = ".$blobID;
    $result = mysqli_query($conn, $sql);
    
	echo "[";
	$isFirst = true;
	while($row = mysqli_fetch_assoc($result)) {
		if($isFirst == false) {
			echo ",";
		} else {
			$isFirst = false;
		}

		echo "{";
		echo "\"blobID\":".$blobID.",";
		echo "\"name\":\"".$row["Name"]."\",";
		echo "\"finished\":".($row["Finished"] ? 'true' : 'false');
		echo "}";
	}

	echo "]";
?>