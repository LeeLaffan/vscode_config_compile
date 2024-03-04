$compiled_path = "L:\Code\Python\vscode_config_compile\output\output.json"
$vscode_path = "C:\Users\l\AppData\Roaming\Code\User\keybindings.json"

$script_path = "L:\Code\Python\vscode_config_compile\main.py"

$python_cmd = "python " + $script_path + " -c " + $compiled_path + " -v " + $vscode_path
print