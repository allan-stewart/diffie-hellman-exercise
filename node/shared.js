const crypto = require('crypto')
const fs = require('fs')

const algorithm = 'aes-256-cbc'

const createIv = () => {
    const buffer = Buffer.alloc(16)
    crypto.randomFillSync(buffer)
    return buffer
}

const encrypt = (simpleKey, plaintextFile, ciphertextFile) => {
    const key = createKeyFromSimpleKey(simpleKey)
    const iv = createIv()
    const cipher = crypto.createCipheriv(algorithm, key, iv)

    let ciphertext = ''
    cipher.setEncoding('base64')

    cipher.on('data', (chunk) => ciphertext += chunk)
    cipher.on('end', () => writeFile(ciphertextFile, `${iv.toString('base64')}\n${ciphertext}`))

    cipher.write(fs.readFileSync(plaintextFile))
    cipher.end();
}

const decrypt = (simpleKey, ciphertextFile, plaintextFile) => {
    const key = createKeyFromSimpleKey(simpleKey)
    const data = loadFileAsText(ciphertextFile).split('\n')
    const iv = Buffer.from(data[0], 'base64')
    const decipher = crypto.createDecipheriv(algorithm, key, iv)
    let plaintext = '';

    decipher.on('data', (chunk) => plaintext += chunk);
    decipher.on('end', () => writeFile(plaintextFile, plaintext));

    decipher.write(data[1], 'base64');
    decipher.end();
}

const createKeyFromSimpleKey = (key) => {
    const hash = crypto.createHash('sha256')
    hash.update(Buffer.from(key + ''))
    return hash.digest()
}

const loadFileAsText = (filename) => {
    try {
        return fs.readFileSync(filename, {encoding: 'utf-8'})
    }
    catch (error) {
        console.error(error.message)
        process.exit()
    }
}

const writeFile = (filename, data) => fs.writeFileSync(filename, data)

module.exports = {
    encrypt,
    decrypt,
    loadFileAsText
}