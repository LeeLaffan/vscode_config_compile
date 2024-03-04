import os
import sys
import argparse
import os
import sys
import argparse




def write_json_file(folder_path, lines):
    print("Writing json file to folder: ", folder_path)
    with open(os.path.join(folder_path, "output.json"), "w") as f:
        f.write("[\n")
        for line in lines:
            f.write("\t" + line + "\n")
        f.write("]\n")
    print("Json file written successfully.")

def main():
    parser=argparse.ArgumentParser(description="sample argument parser")
    parser.add_argument("-m", "--mode", type=str, required=True, help="Mode to run the script")
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