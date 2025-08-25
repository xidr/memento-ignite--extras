def filter_words(input_file, output_file, min_length=3, max_length=15):
    """
    Read words from input file, filter words with length >= min_length,
    and write filtered words to output file.

    Args:
        input_file (str): Path to input text file
        output_file (str): Path to output text file
        min_length (int): Minimum word length (default: 3)
    """
    try:
        # Read words from input file
        with open(input_file, 'r', encoding='utf-8') as f:
            words = f.read().strip().split('\n')

        # Filter words that are at least min_length characters long
        filtered_words = [word.strip() for word in words if min_length <= len(word.strip()) <= max_length]

        # Write filtered words to output file
        with open(output_file, 'w', encoding='utf-8') as f:
            for word in filtered_words:
                f.write(word + '\n')

        print(f"Successfully processed {len(words)} words.")
        print(f"Filtered to {len(filtered_words)} words (min length >= {min_length}, max length <= {max_length}).")
        print(f"Results written to: {output_file}")

    except FileNotFoundError:
        print(f"Error: Input file '{input_file}' not found.")
    except Exception as e:
        print(f"Error: {e}")