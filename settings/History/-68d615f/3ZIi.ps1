$compiled_path = "L:\Code\Python\vscode_config_compile\output\output.json"
$vscode_path = "C:\Users\l\AppData\Roaming\Code\User\keybindings.json"

$script_path = "L:\Code\Python\vscode_config_compile\main.py"

$python_cmd = "python -i" + $script_path + " "

python .\main.py --compiled "L:\Code\Python\vscode_config_compile\input\" --vscode "C:\Users\l\AppData\Roaming\Code\User\keybindings.json"
python -c L:/Code/Python/vscode_config_compile/input/ -v Test