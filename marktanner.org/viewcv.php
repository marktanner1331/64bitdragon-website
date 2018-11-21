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
	echo "<h1>Index of /Mark Tanner/computer vision/".$folder."</h1>";
	echo getMetaTags($folder);
?>
<script type="text/javascript">
function switchToFile(file)
{
	var iframe = document.getElementById("codeframe");
	iframe.src = "/computer vision/<?php echo $folder; ?>/" + file + ".html";
}
function switchToImage(file)
{
	var iframe = document.getElementById("codeframe");
	iframe.src = "/computer vision/<?php echo $folder; ?>/" + file;
}
</script>
 </head>
 <body>
 <table>
   <tr><th valign="top"><img src="/icons/blank.gif" alt="[ICO]"></th><th><a href="?C=N;O=D">Name</a></th><th><a href="?C=M;O=A">Last modified</a></th><th><a href="?C=S;O=A">Size</a></th><th><a href="?C=D;O=A">Description</a></th></tr>
   <tr><th colspan="5"><hr></th></tr>
<?php
	printFolderRow(".", "/viewcv.php?folder=".$folder, getLMForFolder("computer vision/".$folder."/."));
	printFolderRow("..", "/index.php?folder=computer vision", getLMForFolder("computer vision/."));
	printFolderRow("download source", "/download.php?file=computer vision/".$folder."/".$folder." source.zip", getLMForFolder("computer vision/".$folder."/".$folder." source.zip"));
?>

   <tr><th colspan="5"><hr></th></tr>
   <tr><td colspan="5">
   
<?php
	$files = scandir("computer vision/".$folder);
?>

<div style="float:left;">
<?php
	foreach ($files as $file) 
	{
		if(strpos($file, ".cs") !== false && strpos($file, ".cs.html") === false)
		{
			echo "<div onClick=switchToFile(\"".$file."\") style=\"cursor:pointer\"><img src=\"/icons/folder.gif\" alt=\"[DIR]\"> ".$file."</div>";
		}
		else if(strpos($file, ".jpg") !== false)
		{
			echo "<div onClick=switchToImage(\"".$file."\") style=\"cursor:pointer\"><img src=\"/icons/folder.gif\" alt=\"[DIR]\"> ".$file."</div>";
		}
	}
?>
</div>
<iframe id="codeframe" style="height: 550px;width:600px;margin-left: 24px;"></iframe>
</td>
</tr>
   <tr><th colspan="5"><hr></th></tr>
</table>

<address>Apache/2.4.10 (Win32) OpenSSL/1.0.1i PHP/5.5.15 Server at localhost Port 80</address>

<script type="text/javascript">
<?php
	if (in_array("result.jpg", $files))
	{
		echo "switchToImage(\"result.jpg\");";
	}
	else if (in_array("Program.cs", $files))
	{
		echo "switchToFile(\"Program.cs\");";
	}
	else
	{
		echo "switchToFile(\"".$files[2]."\");"; 
	}
?>
</script>
</body>
</html>

<?php
	function getMetaTags($dir)
	{
		if(file_exists("computer vision/".$dir."/meta.txt") === false)
		{
			return "";
		}
		
		$myfile = fopen("computer vision/".$dir."/meta.txt", "r");
		$meta = fread($myfile,filesize("computer vision/".$dir."/meta.txt"));
		fclose($myfile);
		return $meta;
	}

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
