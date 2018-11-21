<?php

header('Content-Type: application/json');

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

$sql = "SELECT ArticleBlobID, Name, Description FROM ArticleBlob WHERE name like '%".$_GET["term"]."%'";
$result = mysqli_query($conn, $sql);

echo "[";
$isFirst = true;
while($row = mysqli_fetch_assoc($result)) {
if($isFirst) {
	$isFirst = false;
} else {
	echo ",";
}

	echo "{\"id\":\"".$row["ArticleBlobID"]."\", \"label\":\"".$row["Name"]."\", \"value\":\"".$row["Name"]."\"}";
}

echo "]";
?>