# Day 1 Part 1 Explanation

First we're loading the file and splitting it at \n, which gives us an array of all the strings in the text file

We check if it's undefined as a sanity check, and then initialize a global sum variable.

Then, for each string, we're going to do a few things

1. Use regex to replace all non-numberic characters with blank strings, leaving us with only number characters
2. Get the very first character in the entire string
3. Get the last character in a string. We can achieve this by implicitly converting it to a number then `mod 10` on it. Mod 10 on any number will always return the last character of the number, this is just basic algebra.
4. We finally convert the two strings to one number and add it to the global sum. This is done by adding firstInt to lastInt. firstInt is going to be a string sicne we did `fullInt[0]` to get it, but lastInt is a number since we used arithmetic to achieve the result. But, in JS, a string plus a number is a string still. So, `'1' + 2 = '12', not 3`. This achieves exactly what we want, since the Part 1 requires us to concatenate the first and last string together. The plus side to this is if the string only contains one numeric character, it still concatenates them both, which is what is supposed to happen.

# Day 1 Part 2 Explanation

Part 2 is quite a bit more involved. The _words_ of the numbers also count as numbers, so string `one32jfieojfive5` wouldn't be 35, it would be 15! How do we handle this?

1. We create a record of type `Record<string, number>`, and populate it starting at 1. The input has no 0 characters or 'zero' words in it, so we can ignore it
2. We then read the input and split it at '\n' then do a sanity check to see if it's undefined as normal

Next, we are going to do a TON of mapping on the array of . The first map (of three) converts each word to the corresponding number. This works by

1. We loop through every single character of the string passed
2. If that character can be converted to a number we push the character to the numbers array
3. Then we loop through every key in that record we create a while ago. Remeber, the key of the record is the number in word form, the value is the number in number form.
    1. We first substring the string at the current index of the main loop. So, if i is 12, then we substring the character at position 12.
    2. If the string, after being substringed, starts with the key we're iterating on, we push the corresponding value to the numbers array

<details><summary>How does this first map work?</summary>

This is confusing, so let me list an example using the string of 'one12jfieojfive5':

We start at position 0, so we then check if 'o' is a number. It isn't, so ignore it and go to the inside loop

We start at the first key in the `wordsToNum` record, which happens to be 'one'. After substringing the original string at position 0, we check to see if it starts with 'one'. It does! Let's push `wordsToNums[key]` to the numbers array, which is 1.

Next, i = 1. string[1] is 'n', which isn't a number. Substring the array at position 1, which cuts off the 'o', leaving us with 'ne12jfieojfive5'. Looping through every single key in the `wordsToNum` array leave us with nothing, so continue on.

Eventually, we get to i = 3. Is '1' a number? Yes! Push it to the array.

</details></br>

After the first map is done, we then map again to convert each element to only its first and last character. Finally, we map a third time to convert each of the elements to numbers, and use the `Array.prototype.reduce()` method to add them all together.

It all works like this!

Take an example input, `strings`

```
const strings = [
    'one12jfieojfive5'
    'sixsevenifeoeight9',
    'threefourfivesix'
]
```
.map on the first line converts to `['11255', '6789', '3456']`
.map again converts to `['15', '69', '36']`


We now map the final numbers array to convert the strings to numbers and then we reduce them by adding each number together

.map on `['15', '69', '36']` gets converted to `[15, 69, 36]`, then .reduce takes each element and adds it together. So, 15 + 69 = 84 + 36 = 120, which is your answer