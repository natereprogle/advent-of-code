# Day 6 Part 1

Finally, difficulty level went down a bit haha. This one only took me about 30 mins after wrapping my head around the problem's ask

So, we first split the input at line breaks, then we're splitting the time and distance by `' '` to get just the individual rounds, and finally we're using `.filter(x => x)` to get rid of pesky `''` items in the array.

So, we're left with an two arrays that look like this:

```typescript
const time = ['40', '82', '91', '66']
const dist = ['277', '1338', '1349', '1063']
```

Now we can just brute force the problem. We'll do a for loop that loops for as many items as there are in the time array, and we'll get the corresponding distance from the distance array.

We'll then loop through every potential amount of button hold times. Our loop will look like this: `for (let buttonHeld = +roundTime; buttonHeld > 0; buttonHeld--) {}`. Remember, 0 milliseconds and the full round time cannot be used because we'll either go nowhere, or we won't have anytime to go anywhere, so we can skip those.

We'll calculate the remaining time of the game by subtracting the round total time from the time we held the button. Finally, we'll calculate the distance we can travel by multiplying the button hold time by the remaining time. Also remember that the amount of time we spend holding the button is a 1:1 equivalent of how many millimeters we go per millisecond in the race. Finally, we check if that distance is larger than the current record for that round. If it is, we add 1 to the roundsWon counter, indicating that's a possible way to win.

Once we've looped through all the possible lengths of time we can for holding the button, we just push the `roundsWon` variable to the waysToWin array.

We'll finally reduce that array with `waysToWin.reduce((a, b) => a * b)`, and that gives us our Part 1 answer

# Day 6 Part 2

Part 2 is literally the _exact_ same problem as Part 1, except we now have to do one round instead of 4 rounds. Turns out, the spaces in the original problem should be ignored.

So, instead of splitting the string at every `' '`, we just trim it to remove leading and trailing whitespace, then .replace() every `\s` character with `''`. Then we reomve the outside loop that loops through every round, and just have the inner loop go through the single round. That's literally it. It does, however, take significantly longer. (Part 2 took 9.454 seconds, while Part 1 only took 2.995 MILLIseconds).
