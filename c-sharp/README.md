# Diffie-Hellman Key Exchange Exercise
_C# / .NET Core Version_


## Math and BigInteger
As you can read in full detail on [wikipedia](https://en.wikipedia.org/wiki/Diffie%E2%80%93Hellman_key_exchange),
the core of the key exchange is based on the mathematical equation:

```
(g^a mod p)^b mod p = (g^b mod p)^a mod p
          A^b mod p = B^a mod p
```

Where `p` and `g` are pre-shared public values, and `a` and `b` are the secret integers.

This equation relies on exponents and will not work if you overflow or lose precision.
The `ulong` type is 64-bit (2^64), so if any of the exponents (such as a secret) are bigger than 64 then we will have a problem.
The `double` and `decimal` types support larger values, but are prone to rounding errors.

This means we will need to use the `BigInteger` type (even for much smaller keys than you would actually use in a production environment).
Creating one is really easy:

```csharp
BigInteger myBigInt = 12345;
var myParsedBigInt = BigInteger.Parse("67890");
```

And luckily, there is already a built-in method to perform the `x^y mod z` calculations:

```csharp
BigInteger.ModPow(x, y, z);
```


## Generating a Value to Share
Now we can start writing some code.
If you use the sample project, there is a `KeyExchange` class where you can add in this code.
You can execute it by running:

```
dotnet run key-exchange
```

For this exercise, we will use a modulus `p` value of 97
and a base `g` of 18.

The first step is to pick a secret integer `a` and generate a value to share with others.
The value to share is `g^a mod p`; calulate this using `BigInteger.ModPow(g, a, p);`

If you use 77 as your secret (`a`) you should get a value of 89.
This is the `A` value you can share publicly so that you can generate a shared key with another person.


## Generating an Encryption Key
You can calculate the secret key when you receive the public value `B` from the another person.
They calculated `B` by calculating `g^b mod p` just as we did with our secret `a`,
so we need to calulate `B^a mod p` (`BigInteger.ModPow(B, a, p)`).

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
dotnet run decrypt <simpleKey> ../example-encrypted.txt ./example-decrypted.txt
```

(Replacing `<simpleKey>` with the encryption key, of course!)

If you generated the correct key, you'll get an `example-decrypted.txt` file with readable text!


## Next Steps
You should now be able to pick your own secret value and exchange keys with other people.
You can write a text file and encrypt it using a command like:
```
dotnet run encrypt <simpleKey> ./file.txt ./encrypted.txt
```

Then send the file to the other person.
Or decrypt a message from them in the same manner as we did above.

If you want to learn more, you can read up on the [encryption details](../encryption-details.md) or look at the sample code to see how it works.
