import argparse
import os
import argparse


def write_json_file(file, lines):
    print("Writing json file to folder: ", file)

    with open(file, "w") as f:
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
        full_lines.append("\n")
    return full_lines


def main():
    parser=argparse.ArgumentParser(description="sample argument parser")
    parser.add_argument("-i", "--input_dir", type=str, help="Mode to run the script")
    parser.add_argument("-o", "--output_file", type=str, help="Mode to run the script")
    args=parser.parse_args()
    print(args)

    full_lines = read_json_files(args.input_dir)
    write_json_file(args.output_file, full_lines)


if __name__ == "__main__":
   main()