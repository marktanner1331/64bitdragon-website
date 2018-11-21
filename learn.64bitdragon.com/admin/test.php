<?php

	require("functions.php");
	$conn = connectToDB();
	$blobID = 2;

	$testArray = loadPrerequisites($conn, $blobID);
	echo sizeof($testArray);
	echo "<br>";
	echo join(", ", $testArray);

?>