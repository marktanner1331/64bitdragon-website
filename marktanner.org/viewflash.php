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
	echo "<h1>Index of /Mark Tanner/flash/".$folder."</h1>";
?>
 </head>
 <body>
 <table>
   <tr><th valign="top"><img src="/icons/blank.gif" alt="[ICO]"></th><th><a href="?C=N;O=D">Name</a></th><th><a href="?C=M;O=A">Last modified</a></th><th><a href="?C=S;O=A">Size</a></th><th><a href="?C=D;O=A">Description</a></th></tr>
   <tr><th colspan="5"><hr></th></tr>
<?php
	printFolderRow(".", "/viewflash.php?folder=".$folder, getLMForFolder("flash/".$folder."/."));
	printFolderRow("..", "/index.php?folder=flash", getLMForFolder("flash/."));
	printFolderRow("view fullscreen", "flash/".$folder."/index.swf", getLMForFolder("flash/".$folder."/index.swf"));
	printFolderRow("download source", "/download.php?file=flash/".$folder."/".$folder." source.zip", getLMForFolder("flash/".$folder."/".$folder." source.zip"));
?>

   <tr><th colspan="5"><hr></th></tr>

<tr><td colspan="5">
<div id="flashContent">
	<object type="application/x-shockwave-flash" data="<?php echo "/flash/".$folder."/index.swf"; ?>" width="550" height="400" id="index" style="float: none; vertical-align:middle">
		<param name="movie" value="<?php echo "/flash/".$folder."/index.swf"; ?>" />
		<param name="quality" value="high" />
		<param name="play" value="true" />
		<param name="loop" value="true" />
		<param name="wmode" value="window" />
		<param name="scale" value="showall" />
		<param name="menu" value="true" />
		<param name="devicefont" value="false" />
		<param name="salign" value="" />
		<param name="allowScriptAccess" value="sameDomain" />
		<a href="http://www.adobe.com/go/getflash">
			<img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" />
		</a>
	</object>
</div>
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