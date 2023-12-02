# Day 2 Part 1 Explanation

We are brute-forcing this -- there is almost definitely a better way to figure this out. First, we get the input and split it at the '\n' character, which gives us an array of each game

First we loop through each game, and get each round. An array of rounds is obtained just by splitting the game string at the ': ' character, and splitting the second half of the string (The list of semicolon separated rounds) at the '; ' character. This returns an array that looks like this:

```typescript
const array = ['1 red, 2 blue, 5 green', '2 green, 4 blue']
```

For every round in the array of rounds, we split the round at the ', ' character, this way we can get the individual count of cubes in each round. Remember, it doesn't matter which round is which, we MUST have less than the max number of cubes provided.

If any of the cube counts in any of the rounds is greater than the max amount of cubes in the bag (12 red, 13 green, 14 blue), then the round is invalid and we mark valid as false for that game.

Once we check all the rounds, we return to the main loop which then checks if valid is still true (by default it is), and if it is it pushes the game id (Which we pulled earlier) to an array.

Finally, we just reduce the array by adding all the values together.

# Day 2 Part 2 Explanation

Part 2 shares all the exact same logic as part 1. To complete this puzzle, instead of setting a `valid` variable and checking if the game is playable, we instead want to find the minimum number of cubes necessary to play each game. To start, we initialize three variables for each of the cube colors in each game, and set them all to 0.

We loop through each round of each game to get the individual cube counts of each round. If the count of that color is greater than the current red, green, or blue counts, set the variable to that count. We want the minmum number of cubes possible to play the round, so the biggest count of red, green, and blue cubes of each round in a game is, by definition, the minimum cubes necessary for play.

Then, for each round, we just multiply the red, green, and blue values, and push it to the gamePowers array. Finally, we add all of the values together.
