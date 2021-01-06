# Encryption Details
In this exercise, we create simple keys which are integers from 0-96.
But this value is too small to be used as a key for the AES 256 cipher in CBC mode.
Therefore, the sample code calculates a SHA 256 hash of the "simpleKey" (as a string; UTF-8 bytes) to use as an actual key.

For encryption, a 16-byte IV is generated.
The IV and the ciphertext are base64 encoded and placed together in an output file (with a `\n` delimiter).

For decryption, the IV and ciphertext are read from the file.
