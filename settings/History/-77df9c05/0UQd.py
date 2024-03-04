import os
import sys
import argparse

def read_json_files(folder_path):
    print("Reading json files from folder: ", folder_path)
    full_lines = []
    full_lines.append("[")
    for file in os.listdir(folder_path):
        if file.endswith(".json"):
            print("Reading file: ", file)
            with open(os.path.join(folder_path, file), "r") as f:
                lines = [line.rstrip() for line in file]
                full_lines.extend(lines)    
    full_lines.append("[")
    print("Read lines count:", len(full_lines))
    return full_lines 

def write_json_files(folder_path, lines):
    print("Writing json files to folder: ", folder_path)

    with open(file_path, "w") as f:
        for i, line in enumerate(lines):
            file_path = os.path.join(folder_path, f"file_{i}.json")
            f.write(line)

    print("Write complete.")



def main():
    parser=argparse.ArgumentParser(description="sample argument parser")
    parser.add_argument("-m", "--mode", type=str, required=True, help="Mode to run the script")
    args=parser.parse_args()
    print(args)

    if args.mode=="Admin":
        print ("Hello Admin")
    else:
        print ("Hello Guest")

    full_files = read_json_files("L:\\Code\\Python\\vscode_config_compile\\input\\")
    write_json_files("L:\\Code\\Python\\vscode_config_compile\\output\\", full_files)
if __name__ == "__main__":
   main()