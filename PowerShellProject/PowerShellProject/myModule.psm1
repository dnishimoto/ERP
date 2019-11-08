function OutputFile($path,$fileName,$fileAndPath,$content)
{
	$result=Test-Path $fileAndPath -PathType leaf
	if($result -eq $true)
	{
		Remove-Item –Path $fileAndPath
	}
	Write-Host "File: $fileName '..." -foregroundColor "Magenta"
	New-Item -Path $path -Name $fileName -ItemType "file" -Value $content
}

Export-ModuleMember -Function OutputFile