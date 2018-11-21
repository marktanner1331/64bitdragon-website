<?php
function connectToDB() {
	$servername = "localhost";
    $username = "root";
    $password = "";
    $dbname = "learn";

    // Create connection
    $conn = new mysqli($servername, $username, $password, $dbname);

    // Check connection
    if ($conn->connect_error) {
        die("Connection failed: " . $conn->connect_error);
    }

	return $conn;
}

//returns a list of blobs that are both finished, and have prerequisites that are completely finished
function getCompletelyFinishedBlobsWithType($conn, $type) {
	$sql = "SELECT ArticleBlobID, Name, Description FROM ArticleBlob WHERE finished = 1 AND Type = ".$type;
    $result = mysqli_query($conn, $sql);

	$blobs = array();
	while($row = mysqli_fetch_assoc($result)) {
		if(hasUnfinishedPrerequisites($conn, $row["ArticleBlobID"]) == false) {
			array_push($blobs, $row);
		}
	}

	return $blobs;
}

//returns a list of blobs that are both finished, and have prerequisites that are completely finished
function getAllCompletelyFinishedBlobs($conn) {
	$sql = "SELECT ArticleBlobID, Name, Description FROM ArticleBlob WHERE finished = 1";
    $result = mysqli_query($conn, $sql);

	$blobs = array();
	while($row = mysqli_fetch_assoc($result)) {
		if(hasUnfinishedPrerequisites($conn, $row["ArticleBlobID"]) == false) {
			array_push($blobs, $row);
		}
	}

	return $blobs;
}

function loadMostRecentBlobs($conn) {
	$sql = "SELECT ArticleBlobID, Name, Description FROM articleblob where Finished = 1 ORDER BY ArticleBlobID DESC";
	$result = mysqli_query($conn, $sql);
	
	$blobs = array();
	while($row = mysqli_fetch_assoc($result)) {
		if(hasUnfinishedPrerequisites($conn, $row["ArticleBlobID"]) == false) {
			array_push($blobs, $row);
		}

		if(count($blobs) == 5) {
			break;
		}
	}

	return $blobs;
}

function fetchBlob($conn, $blobID) {
	$sql = "SELECT Name, Path, Description, Keywords, Type FROM ArticleBlob WHERE ArticleBlobID = ".$blobID;
    $result = mysqli_query($conn, $sql);
	   
    return mysqli_fetch_assoc($result);
}

function hasUnfinishedPrerequisites($conn, $blobID) {
	return sizeof(loadUnfinishedPrerequisites($conn, $blobID)) > 0;
}

function loadUnfinishedPrerequisites($conn, $blobID) {
	$sql = "SELECT ChildBlobID, Finished FROM blobprerequisite INNER JOIN articleblob ON blobprerequisite.ChildBlobID=articleblob.ArticleBlobID WHERE blobprerequisite.ParentBlobID = ".$blobID;
    $result = mysqli_query($conn, $sql);

	$blobs = array();

	while($row = mysqli_fetch_assoc($result)) {
		if($row["Finished"]) {
			$subBlobs = loadUnfinishedPrerequisites($conn, $row["ChildBlobID"]);
			foreach ($subBlobs as $subBlob) {
				if(in_array($subBlob, $blobs) == false) {
					array_push($blobs, $subBlob);
				}
			}
		} else {
			if(in_array($row["ChildBlobID"], $blobs) == false) {
				array_push($blobs, $row["ChildBlobID"]);
			}
		}
    }

	return $blobs;
}

function loadPrerequisites($conn, $blobID) {
    $sql = "SELECT ChildBlobID FROM blobprerequisite INNER JOIN articleblob ON blobprerequisite.ChildBlobID=articleblob.ArticleBlobID WHERE articleblob.Finished = 1 AND blobprerequisite.ParentBlobID = ".$blobID;
    $result = mysqli_query($conn, $sql);
    
	$blobs = array();

	while($row = mysqli_fetch_assoc($result)) {
		if(in_array($row["ChildBlobID"], $blobs) == false) {
			$childBlobs = loadPrerequisites($conn, $row["ChildBlobID"]);

			foreach ($childBlobs as $childBlob) {
				if(in_array($childBlob, $blobs) == false) {
					array_push($blobs, $childBlob);
				}
			}

			array_push($blobs, $row["ChildBlobID"]);
		}
    }

	return $blobs;
}

function getRelated($conn, $blobID) {
	$sql = "SELECT ParentBlobID FROM blobprerequisite INNER JOIN articleblob ON blobprerequisite.ParentBlobID=articleblob.ArticleBlobID WHERE articleblob.Finished = 1 AND blobprerequisite.ChildBlobID = ".$blobID;

	$result = mysqli_query($conn, $sql);
    $blobs = array();

    while($row = mysqli_fetch_assoc($result)) {
		array_push($blobs, $row["ParentBlobID"]);
	}

	return $blobs;
}
?>