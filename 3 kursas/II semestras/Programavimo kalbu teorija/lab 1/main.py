import math
import binascii


class Helper:
    def __init__(self):
        self.input_array = []
        self.input_file_name = "input.txt"
        self.output_file_name = "output.txt"

    def convert_to_binary(self, number):
        return bin(number)

    def read_from_file(self):
        with open(self.input_file_name, "r") as file:
            for idx, row in enumerate(file.readlines()):
                if idx != 0:
                    self.input_array.append(int(row))

    def process_number(self, number):
        X1 = self.convert_to_binary(number)              # binary of number
        b1 = self.count_ones_from_binary(X1)             # 1's in binary number
        X2 = bin(int(str(number), 16))                 # binary of hex number
        b2 = self.count_ones_from_binary(X2)             # 1's in binary of hex number
        encrypted = self.encrypt_number(number, b1, b2)  # Encrypt number with XOR
        return f"{b1} {b2}"

    def encrypt_number(self, number, b1, b2):
        return number ^ (b1 * b2)

    def count_ones_from_binary(self, binary_number):
        b1 = 0
        for idx, char in enumerate(binary_number):
            if idx > 1:
                if char == '1':
                    b1 += 1
        return b1

    def main(self):
        with open(self.output_file_name, "w") as file:
            for input_number in self.input_array:
                file.write(f"{self.process_number(input_number)}\n")


helper = Helper()
helper.read_from_file()
helper.main()
