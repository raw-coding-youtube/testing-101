# Why should I test my code?
When done right:

- saves time
- reliability & confidence
- improves codebase quality
- safety for future developers
- documentation

# Which Testing Framework?
- `xUnit.net` - newest, leanest, fast, tdd focused
- `NUnit` - mature, feature packed, fast
- `msTest` - mature, slow

I tend to follow this process of picking:
- `xUnit.net` is the default.
- If `xUnit.net` doesn't have a feature needed for your use case, `NUnit` probably has it:
    - parallelization control
    - assertion messages
    - platform support
    - tooling
- never `msTest`

[See More](https://xunit.net/docs/comparisons)

# What to Test?
Easiest choice would be: code that broke previously, write a test for it so that never happens again.
For newly written code - not all code is equal, what looks/sounds more important?

#### Banking App
- Display About Page
- Money Transfer

#### Facebook
- Being able to create a post
- Receive group notifications

#### Youtube
- Videos Playing
- Displaying Comments

#### Online Shop
- Updating Stock After Purchase
- Adding products to Cart

Behind everything you code, there's an external use case. How much $$$ would it cost you if that piece of code stopped working as intended?

# When do I know I have enough Tests?
When you feel confident about it, one can only judge after acquiring experience.

- Aim for 100% coverage.
- Time is important, there are always more problems to solve.
- Make mistakes early and take note.


# Relaying importance of tests to the business?
I think we've all faced this problem - the business side wants all the features and they want them now.
However the business doesn't know about all this stuff around the feature that makes it work.

Writing tests should not be an opt-in feature which you include when you have extra time, 
it should be the full package default. 
Tests are not alone in this category, here lay writing docs, useful commit messages and admin work.

Remember two things:

1. You are the professional, you know better, you say how long it takes.
2. All programmers suck at estimating, make sure you add padding for tests and other work. 

It's a learning process, start now.
