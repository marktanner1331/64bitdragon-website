<?php
	if(isset($_GET["folder"]))
	{
		$folder = $_GET["folder"];
	}
	else
	{
		$folder = "";
	}
?>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 3.2 Final//EN">
<html>
 <head>
<?php
	echo "<title>".$folder."</title>";
	echo "<h1>Index of /Mark Tanner/applications/".$folder."</h1>";
?>
 </head>
 <body>
 <table>
   <tr><th valign="top"><img src="/icons/blank.gif" alt="[ICO]"></th><th><a href="?C=N;O=D">Name</a></th><th><a href="?C=M;O=A">Last modified</a></th><th><a href="?C=S;O=A">Size</a></th><th><a href="?C=D;O=A">Description</a></th></tr>
   <tr><th colspan="5"><hr></th></tr>
<?php
	printFolderRow(".", "/viewapplication.php?folder=".$folder, getLMForFolder("applications/".$folder."/."));
	printFolderRow("..", "/index.php?folder=applications", getLMForFolder("applications/."));
	printFolderRow("download source", "/download.php?file=applications/".$folder."/".$folder." source.zip", getLMForFolder("applications/".$folder."/".$folder." source.zip"));
?>

   <tr><th colspan="5"><hr></th></tr>

<tr><td colspan="5">
<img width="500pz" src="<?php echo "applications/".$folder."/image.png"?>" />
</td>
</tr>

   <tr><th colspan="5"><hr></th></tr>
</table>

<address>Apache/2.4.10 (Win32) OpenSSL/1.0.1i PHP/5.5.15 Server at localhost Port 80</address>
</body></html>

<?php
	function getLMForFolder($dir)
	{
		return date ("F-d-Y H:i", filemtime($dir));
	}

	function printFolderRow($name, $href, $lm)
	{
		echo "   <tr>";
		echo "<td valign=\"top\"><img src=\"/icons/folder.gif\" alt=\"[DIR]\"></td><td><a href=\"".$href."\">".$name."/</a></td>";
		echo "<td align=\"right\">".$lm."  </td><td align=\"right\">  - </td>";
		echo "<td>&nbsp;</td></tr>";
	}
?>