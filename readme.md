# PGP word list
[![Build](https://github.com/messerli-informatik-ag/pgp-word-list/workflows/Build/badge.svg)](https://github.com/messerli-informatik-ag/pgp-word-list/actions?query=workflow%3ABuild)
[![NuGet](https://img.shields.io/nuget/v/Messerli.PgpWordList.svg)](https://www.nuget.org/packages/Messerli.PgpWordList/)

PGP word list and functionality to encode and decode series of PGP words

## Usage
```csharp
// Look up a word for an even byte
PgpWords.ToEvenWord(0xC0); // => "slowdown"
// Look up a byte for an odd word
PgpWords.ToOddByteOrNone("Yucatan"); // => Some(0xFF)

// Convert a byte array to a sequence of pgp words:
PgpWordSequence.ToWords(new byte[] { 0xC0, 0xFF, 0xEE }); // => "slowdown-Yucatan-tycoon"
// Convert a list of words back to bytes:
PgpWordSequence.ToBytesOrNone("slowdown-Yucatan-tycoon"); // => Some([0xC0, 0xFF, 0xEE])
```
