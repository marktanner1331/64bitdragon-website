<?php
require("functions.php");
$conn = connectToDB();
?>

<html>
<body>

<h4>Exporting Blobs</h4>
<?php
$blobs = getAllCompletelyFinishedBlobs($conn);

for($i = 0;$i < count($blobs);$i++) {
	$blob = $blobs[$i];

	echo "exporting blob: ".$blob["Name"]."<br />";

	$name = $blob["Name"];
	$name = strtolower($name);
	$name = str_replace(" ", "_", $name);

	$url = "http://localhost/admin/dynamicblob.php?blobid=".$blob["ArticleBlobID"];
    
	file_put_contents("../exportedhtml/".$name.".html", fopen($url, 'r'));
}
?>

<h4>Exporting index</h4>
<?php
$url = "http://localhost/admin/export_index.php"; 
file_put_contents("../index.html", fopen($url, 'r'));
?>

<h4>Exporting Categories</h4>
<?php
$url = "http://localhost/admin/export_category.php?type=1"; 
file_put_contents("../categories/mathematics.html", fopen($url, 'r'));

$url = "http://localhost/admin/export_category.php?type=4"; 
file_put_contents("../categories/computer_science.html", fopen($url, 'r'));
?>

</body>
</html>