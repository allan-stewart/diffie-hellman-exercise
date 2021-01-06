# Diffie-Hellman Key Exchange Exercise
_Node.js Version_

> **NOTE:**
> The sample code relies only on the core modules of Node.js; no `npm install` (or `npm ci`) necessary!

## Math and BigInt
As you can read in full detail on [wikipedia](https://en.wikipedia.org/wiki/Diffie%E2%80%93Hellman_key_exchange),
the core of the key exchange is based on the mathematical equation:

```
(g^a mod p)^b mod p = (g^b mod p)^a mod p
          A^b mod p = B^a mod p
```

Where `p` and `g` are pre-shared public values, and `a` and `b` are the secret integers.

This equation relies on exponents and will not work if you overflow or lose precision.
Unfortunately, the default `number` type in JavaScript is a floating-point value which lacks the precision needed to calculate even very small keys.

This means we will need to use the `BigInt` type.
Creating one is really easy:

```javascript
const myBigInt = 12345n
const myParsedBigInt = BigInt('67890')
```

(Note the use of `n` next to the number to tell the interpreter to treat it as a `BigInt`.)

Normally you might use the built-in `Math.pow` method to calculate exponents.
Unfortunately, this won't work with a `BigInt` so you'll need to use the `**` operator instead.
The `%` operator calculates the modulus on a `BigInt` as usual.

Therefore, to execute the `x^y mod z` calculation, you'll have code like:

```javascript
(x ** y) % z
```


## Generating a Value to Share
Now we can start writing some code.
The sample code provides a `key-exchange.js` file where you can put your code, then you can run it by calling:
```
node key-exchange.js
```

For this exercise, we will use a modulus `p` value of 97
and a base `g` of 18.

The first step is to pick a secret integer `a` and generate a value to share with others.
The value to share is `g^a mod p`; calulate this using `(g ** a) % p`

If you use 77 as your secret (`a`) you should get a value of 89.
This is the `A` value you can share publicly so that you can generate a shared key with another person.


## Generating an Encryption Key
You can calculate the secret key when you receive the public value `B` from the another person.
They calculated `B` by calculating `g^b mod p` just as we did with our secret `a`,
so we need to calulate `B^a mod p` (`(B ** a) mod p`).

The resulting value is the encryption key we can use to encrypt messages for or decrypt messages from them.

> **NOTE:**
> In this case, we have a very simplistic key.
> In a production environment you would need to use much larger values and battle-tested code in order to be safe.
> Because we used a `p` value of 97, there are only 97 possible keys, so brute forcing the key is pretty easy to do.
> 
> This exercise is only for learning / demonstration purposes to help you understand the basic crypto concepts.

If you use 42 for `B` then you should get a key of 34.
We will call this the "simpleKey".


## Decrypting a Message
That you can generate an encryption key,
you should be able to decrypt messages from another party.

To test that this is working correctly, generate a key using a secret `a = 38` and value from other person `B = 33`.

Then run the following command:
```
node decrypt.js <simpleKey> ../example-encrypted.txt ./example-decrypted.txt
```

(Replacing `<simpleKey>` with the encryption key, of course!)

If you generated the correct key, you'll get an `example-decrypted.txt` file with readable text!


## Next Steps
You should now be able to pick your own secret value and exchange keys with other people.
You can write a text file and encrypt it using a command like:
```
node encrypt.js <simpleKey> ./file.txt ./encrypted.txt
```

Then send the file to the other person.
Or decrypt a message from them in the same manner as we did above.

If you want to learn more, you can read up on the [encryption details](../encryption-details.md) or look at the sample code to see how it works.
