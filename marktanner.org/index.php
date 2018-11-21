<?php
	if(isset($_GET["folder"]))
	{
		$folder = $_GET["folder"];
	}
	else
	{
		$folder = "root";
	}
	
	if($folder == "csharp")
	{
		$folder = "c#";
	}
	
	if($folder != "c#" && $folder != "flash" && $folder != "applications" && $folder != "low level" && $folder != "maths" && $folder != "computer vision")
	{
		$folder = "root";
	}
?>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 3.2 Final//EN">
<html>
 <head>
 <meta name="description" content="Mark Tanner Personal Website">
<meta name="keywords" content="c# flash programming source code">
<meta name="author" content="Mark Tanner">
<?php
	if($folder == "root")
	{
		echo "<title>Mark Tanner</title>";
	}
	else
	{
		echo "<title>".$folder."</title>";
	}
?>
 </head>
 <body>
<?php
	if($folder == "root")
	{
		echo "<h1>Index of /Mark Tanner</h1>";
	}
	else
	{
		echo "<h1>Index of /Mark Tanner/".$folder."</h1>";
	}
?>
  <table>
   <tr><th valign="top"><img src="/icons/blank.gif" alt="[ICO]"></th><th><a href="?C=N;O=D">Name</a></th><th><a href="?C=M;O=A">Last modified</a></th><th><a href="?C=S;O=A">Size</a></th><th><a href="?C=D;O=A">Description</a></th></tr>
   <tr><th colspan="5"><hr></th></tr>
<?php
	if($folder == "root")
	{
		printFolderRow("C#", "/index.php?folder=csharp", getLMForFolder("csharp/."));
		printFolderRow("flash", "/index.php?folder=flash", getLMForFolder("flash/."));
		printFolderRow("applications", "/index.php?folder=applications", getLMForFolder("applications/."));
		printFolderRow("low level", "/index.php?folder=low level", getLMForFolder("low level/."));
		printFolderRow("maths", "/index.php?folder=maths", getLMForFolder("maths/."));
		printFolderRow("computer vision", "/index.php?folder=computer vision", getLMForFolder("computer vision/."));
	}
	else if($folder == "flash")
	{
		$files = scandir("flash");
		sort($files, SORT_NATURAL | SORT_FLAG_CASE);
		foreach ($files as $file) 
		{
			if($file == ".")
			{
				printFolderRow($file, "/index.php?folder=flash", getLMForFolder("flash/."));
			}
			else if($file == "..")
			{
				printFolderRow($file, "/index.php", getLMForFolder("/."));
			}
			else
			{
				printFolderRow($file, "/viewflash.php?folder=".$file, getLMForFolder("flash/".$file."/."));
			}
		}
	}
	else if($folder == "c#")
	{
		$files = scandir("csharp");
		sort($files, SORT_NATURAL | SORT_FLAG_CASE);
		foreach ($files as $file) 
		{
			if($file == ".")
			{
				printFolderRow($file, "/index.php?folder=csharp", getLMForFolder("csharp/."));
			}
			else if($file == "..")
			{
				printFolderRow($file, "/index.php", getLMForFolder("/."));
			}
			else
			{
				printFolderRow($file, "/viewcsharp.php?folder=".$file, getLMForFolder("csharp/".$file."/."));
			}
		}
	}
	else if($folder == "computer vision")
	{
		$files = scandir("computer vision");
		sort($files, SORT_NATURAL | SORT_FLAG_CASE);
		foreach ($files as $file) 
		{
			if($file == ".")
			{
				printFolderRow($file, "/index.php?folder=computer vision", getLMForFolder("computer vision/."));
			}
			else if($file == "..")
			{
				printFolderRow($file, "/index.php", getLMForFolder("/."));
			}
			else
			{
				printFolderRow($file, "/viewcv.php?folder=".$file, getLMForFolder("computer vision/".$file."/."));
			}
		}
	}
	else if($folder == "applications")
	{
		$files = scandir("applications");
		sort($files, SORT_NATURAL | SORT_FLAG_CASE);
		foreach ($files as $file) 
		{
			if($file == ".")
			{
				printFolderRow($file, "/index.php?folder=applications", getLMForFolder("applications/."));
			}
			else if($file == "..")
			{
				printFolderRow($file, "/index.php", getLMForFolder("/."));
			}
			else
			{
				printFolderRow($file, "/viewapplication.php?folder=".$file, getLMForFolder("applications/".$file."/."));
			}
		}
	}
	else if($folder == "low level")
	{
		$files = scandir("low level");
		sort($files, SORT_NATURAL | SORT_FLAG_CASE);
		foreach ($files as $file) 
		{
			if($file == ".")
			{
				printFolderRow($file, "/index.php?folder=low level", getLMForFolder("low level/."));
			}
			else if($file == "..")
			{
				printFolderRow($file, "/index.php", getLMForFolder("/."));
			}
			else
			{
				printFolderRow($file, "/viewlowlevel.php?folder=".$file, getLMForFolder("low level/".$file."/."));
			}
		}
	}
	else if($folder == "maths")
	{
		$files = scandir("maths");
		sort($files, SORT_NATURAL | SORT_FLAG_CASE);
		foreach ($files as $file) 
		{
			if($file == ".")
			{
				printFolderRow($file, "/index.php?folder=maths", getLMForFolder("maths/."));
			}
			else if($file == "..")
			{
				printFolderRow($file, "/index.php", getLMForFolder("/."));
			}
			else
			{
				printMathsFolderRow($file, "/maths/".$file."/".$file.".pdf", getLMForFolder("maths/".$file."/."));
			}
		}
	}
?>
   <tr><th colspan="5"><hr></th></tr>
</table>
<address>Apache/2.4.10 (Win32) OpenSSL/1.0.1i PHP/5.5.15 Server at localhost Port 80</address>
</body></html>

<?php
	function getLMForFolder($dir)
	{
		return date ("F d Y H:i:s.", filemtime($dir));
	}

	function printMathsFolderRow($name, $href, $lm)
	{
		echo "   <tr>";
		echo "<td valign=\"center\"><img src=\"/icons/folder.gif\" alt=\"[DIR]\"></td><td><a href=\"".$href."\"><img style=\"padding:5px 25px 5px 10px\" src=\"maths/".$name."/title.jpg\" /> </a></td>";
		echo "<td valign=\"center\" align=\"right\">".$lm."  </td><td valign=\"center\" align=\"right\">  - </td>";
		echo "<td>&nbsp;</td></tr>";
	}
	
	function printFolderRow($name, $href, $lm)
	{
		echo "   <tr>";
		echo "<td valign=\"top\"><img src=\"/icons/folder.gif\" alt=\"[DIR]\"></td><td><a href=\"".$href."\">".$name."/</a></td>";
		echo "<td align=\"right\">".$lm."  </td><td align=\"right\">  - </td>";
		echo "<td>&nbsp;</td></tr>";
	}
?>