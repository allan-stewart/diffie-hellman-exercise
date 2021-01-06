const shared = require('./shared.js')

if (process.argv.length < 5) {
    console.log('Usage:')
    console.log('node decrypt.js <simpleKey> <inputFile> <outputFile>')
    process.exit(0)
}

const simpleKey = process.argv[2]
const ciphertextFile = process.argv[3]
const plaintextFile = process.argv[4]

console.log(`Simple key: ${simpleKey}`)
console.log(`Ciphertext file: ${ciphertextFile}`)

shared.decrypt(simpleKey, ciphertextFile, plaintextFile)
console.log(`Plaintext written to: ${plaintextFile}`)
