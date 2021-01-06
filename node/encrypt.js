const shared = require('./shared.js')

if (process.argv.length < 5) {
    console.log('Usage:')
    console.log('node encrypt.js <simpleKey> <inputFile> <outputFile>')
    process.exit(0)
}

const simpleKey = process.argv[2]
const plaintextFile = process.argv[3]
const ciphertextFile = process.argv[4]

console.log(`Simple key: ${simpleKey}`)
console.log(`Plaintext file: ${plaintextFile}`)

shared.encrypt(simpleKey, plaintextFile, ciphertextFile)
console.log(`Ciphertext written to: ${ciphertextFile}`)
