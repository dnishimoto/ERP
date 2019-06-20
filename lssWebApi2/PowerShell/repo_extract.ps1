###Overview: This powershell script check for files with the name "repository" and ignores the interface files with "repository" beginning with the ###character "I".  It recursive searches the folder for all files.  Each files content is read a line at a time and a regular expression extracts all public ###functions into a variable called $line. The "public" and "async" keywords are replaced with blank strings. "class" and "dbContext" lines are excluded.  ###The interface file is generated and outputted to the tmp_script folder.

function OutputFile($path,$fileName,$fileAndPath,$content)
{
	$result=Test-Path $fileAndPath -PathType leaf
	if($result -eq $true)
	{
		Remove-Item –Path $fileAndPath
	}
	New-Item -Path $path -Name $fileName -ItemType "file" -Value $content
}

$path="c:\users\owner\tmp_script"
$result=Test-Path $path -PathType Container

if ($result -eq $false)
{
New-Item -ItemType directory -Path $path
}

set-location c:\users\owner\source\repos\millenniumerp\lsswebapi2

Get-ChildItem –Path "." -Directory | foreach-object{ 

$folder = $_.FullName +"*"

Write-Host "Folder: '$folder'..." -foregroundColor "green"

Get-ChildItem -Path $folder  -Recurse -Filter *.cs -ErrorAction SilentlyContinue -Force| foreach-object{
	$fileAndPath=$_.FullName;
	$fileName=$_.Name;



	if (($fileAndPath -like "*Repository*")  -and ($fileName.Substring(0,1) -ne "I")) {
		
		Write-Host "File: $fileName '..." -foregroundColor "Magenta"

		$pattern=@("\s*public\s+[a-zA-Z0-9\<\>\s]+\({1}[a-zA-Z0-9\<\>\s,]+\){1}")

		$interface="I" + $fileName -replace ".cs" ,""

		$content="public interface " + $interface + " {"+"`r`n";

		Get-Content $fileAndPath | Where-Object {$_ -match $pattern} | foreach{

			$line=$_;
			### exclude data view class definitions
			### exclude the constructor
			if ($line.Contains("class") -or ($line.Contains("DbContext"))){}
			else
			{
				$line=$line -replace "public", "";
				$line=$line -replace "async", "";


				$content=$content+$line+";"+"`r`n";
			}

		}

		$content=$content+"}"

		$content;

		$fileName="I"+$fileName;

		$newFileAndPath=$path+"\"+$fileName;

		OutputFile $path $fileName $newFileAndPath  $content
	
	}
   }

}


set-location c:\users\owner
