$input_dir = "{PATH}\vscode_config_compile\input"
$compiled_path = "{PATH}\vscode_config_compile\output\output.json"
$vscode_path = "C:\Users\{USER}\AppData\Roaming\Code\User\keybindings.json"

$script_path = "{PATH}\vscode_config_compile\main.py"

python $script_path -i $input_dir -o $compiled_path

Copy-Item -Path $compiled_path -Destination $vscode_path
Write-Output "Copied to $vscode_path"