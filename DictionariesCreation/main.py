
import words_utils

# Main execution
if __name__ == "__main__":
    # Configuration
    input_filename = "rus/filtered_words_rus_3_plus.txt"  # Input file name
    output_filename = "rus/filtered_words_rus_9_20.txt"  # Output file name

    min_length = 9
    max_length = 20

    # Run the filter function
    words_utils.filter_words(input_filename, output_filename, min_length, max_length)