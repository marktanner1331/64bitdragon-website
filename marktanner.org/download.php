<?php
	$file_url = $_GET["file"];
	if(strpos($file_url, ".zip") !== false)
	{
		header('Content-Description: File Transfer');
		header('Content-Type: application/zip');
		header('Content-Disposition: attachment; filename='.basename($file_url));
		header('Expires: 0');
		header('Cache-Control: must-revalidate');
		header('Pragma: public');
		header('Content-Length: ' . filesize($file_url));
		readfile($file_url);
	}
?>