I'm gonna be honest, this was a problem I don't know how to solve, so I used polumrak\_'s solution on reddit. [Credits here](https://www.reddit.com/r/adventofcode/comments/189m3qw/comment/kbtzwhw/?utm_source=share&utm_medium=web2x&context=3), I will attempt to explain how this works

# Day 3 Part 1

We start by converting the file into a 2d array, or an array of arrays, where the y axis is lines and the x axis is position in the row. We then call the flatMap function, which takes an array and flattens it by one level.
The example provided in MDN docs is:

```typescript
const arr1 = [1, 2, 1]

const result = arr1.flatMap(num => (num === 2 ? [2, 2] : 1))

console.log(result)
// Expected output: Array [1, 2, 2, 1]
```

We're not using the flatmap function to return a flattened array, but rather to be able to iterate through every line and keep track of its index. The "flattened" array we return is actually just the array of all valid part numbers.

The flatMap function takes a callback which has our line array (An array of all characters in the line), and an index, which is our y axis essentially. Within this callback we define our return, a nums array. This will be the array of all "part numbers", which we have to add together. We also define a numBuffer, that way we can store numbers larger than a single digit.

We then start by looping through every character in the line. We first check if the character is a digit, if it is add it to the numBuffer. If it isn't, we check if the numBuffer has a length >0, meaning that we have a full number finally.

Once we have a full number, we check if the number is adjacent to any symbols. This is done by checking above and below each of the characters in the number, and to the left and right. The way this is achieved is a for loop which starts at the position before the number (Since we can have diagonal numbers), and ends at the position after the number.

If it is adjacent to any valid symbols at all, we push it to the nums array. Once done, we return the nums array and add them all together.

# Day 3 Part 2

This one is a bit more involved, and uses JS language features I'm not quite familiar with. Nonetheless, I've attempted to break it down.

The problem defines a gear as two numbers which are adjacent to the same asterisk. We have to multiply them together to get its ratio, then add all the ratios together.

First we start with a dictionary called GearsDict, which is an object in which its key is a string and its value is the number. The key is defined in code as the gear coordinate in `y:x` format

We split up the input into a 2d array just like last time, and then start looping through it. We use the same numBuffer code, except instead of checking for adjacent symbols, we check for adjacent gears. We then check if the number has any adjacent \*'s next to it. if so, we push the coordinates of that gear into an array which we then return. A \* may not necessarily have two numbers next to it, but it could. After the array of gear locations is returned, we iterate through that array and add each of the gear coordinates and their cooresponding number or numbers to the GearsDict.

Once we have all the gear coordinates and their numbers, we then loop through all the keys in the gears dict, and filter it so that every gear has a length of 2, meaning it has two numbers in it and not just one. We then call a reduce function that, to be honest, I have no idea what it does.

Finally, we take the object of proper gears and first multiply the numbers together to get the gear ratios, then add all the gear ratios together. Thanks, polumrak, for sharing your solution!