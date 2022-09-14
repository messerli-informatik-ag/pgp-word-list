# Changelog

## 0.1.0
- Initial release

## 0.1.1
- Add support for .NET Standard 2.0.

## 0.1.2
* Fix `PgpWordAggregationParser.Parse` not throwing an exception for invalid words.
* Fix `PgpWordAggregationParser` not working correctly with multi-character separators.
* Add `PgpWordAggregationParser.ParseOrNone` for graceful error handling.

## 0.2.0
* Rewrite library to have a more compact API surface.
* Store words in a list instead of a dictionary.
* Update to Funcky 3.0.
