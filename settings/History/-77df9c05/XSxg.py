import argparse
import os
import argparse


def write_json_file(folder_path, lines):
    print("Writing json file to folder: ", folder_path)
    with open(os.path.join(folder_path, "output.json"), "w") as f:
        f.write("[\n")
        for line in lines:
            f.write("\t" + line)
        f.write("\n]")
    print("Json file written successfully.")


def read_json_files(folder_path):
    json_files = [f for f in os.listdir(folder_path) if f.endswith(".json")]
    full_lines = []
    for file in json_files:
        with open(os.path.join(folder_path, file), "r") as f:
            lines = f.readlines()
            full_lines.extend(lines)
    return full_lines


def main():
    parser=argparse.ArgumentParser(description="sample argument parser")
    parser.add_argument("-c", "compiled", type=str, required=True, help="Mode to run the script")
    parser.add_argument("-v", "vscode", type=str, required=True, help="Mode to run the script")
    args=parser.parse_args()
    print(args)

    if args.mode=="Admin":
        print ("Hello Admin")
    else:
        print ("Hello Guest")

    full_lines = read_json_files("L:\\Code\\Python\\vscode_config_compile\\input\\")
    write_json_file("L:\\Code\\Python\\vscode_config_compile\\output\\", full_lines)


if __name__ == "__main__":
   main()