$input_dir = "L:\Code\Python\vscode_config_compile\input"
$compiled_path = "L:\Code\Python\vscode_config_compile\output\output.json"
$vscode_path = "L:\Code\Python\vscode_config_compile\output\output.json"

$script_path = "L:\Code\Python\vscode_config_compile\main.py"

python $script_path -i $input_dir -o $compiled_path

Copy-Item -Path $compiled_path -Destrination $vscode_path
``` 