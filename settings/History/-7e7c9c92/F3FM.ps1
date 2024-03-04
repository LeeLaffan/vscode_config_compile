$input_path = "L:\Code\Python\vscode_config_compile\"
$git_path = "L:\Code\Python\vscode_config_compile_git\"

Copy-Item -Path $input_path -Destination $git_path -Recurse -Force
Write-Output "Copied to $git_path"



