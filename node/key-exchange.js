const readline = require('readline');

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

const p = 97n
const g = 18n

rl.question('Enter your secret: ', (secret) => {
    const a = BigInt(secret.trim())
    const A = (g ** a) % p
    console.log(`Value to share: ${A}`)

    rl.question('Enter the public value from the other person: ', (answer) => {
        const B = BigInt(answer.trim())
        const simpleKey = (B ** a) % p
        console.log(`Simple key: ${simpleKey}`)

        rl.close()
    })
})
