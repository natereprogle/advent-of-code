import { getInput } from '../utils/reader'

const wordsToNums: Record<string, number> = {
    one: 1,
    two: 2,
    three: 3,
    four: 4,
    five: 5,
    six: 6,
    seven: 7,
    eight: 8,
    nine: 9,
}

function replaceWordsWithNumbers(string: string): string[] {
    const numbers = []

    // Loop through every character in the string
    for (let i = 0; i < string.length; i++) {
        // Get the current character
        const curr = string[i]
        // If the character is a real number, add that to the array
        if (Number.isFinite(+curr)) {
            numbers.push(curr)
            continue
        }
        // For every string, check if the substring equals that word. If so, get the number it corresponds to.
        /*
            If the string was 'one12jfieojfive5', then it would go through each one one by one. Let's say we were at i = 10, that would put us at the beginning of the 'jfive5' part
            We substring the whole string, leaving us with 'jfive5'
            We check if the string starts with our current key, which would be the string piece of the record above. If it doesn't move on.
            Next it would be i = 11, so substringing at i = 11 gives us 'five5'
            We check if the string matches our key, which would be the string piece of the reocrd above. Once we hit 5, this will be true, so we'll get the corresponding number and add it to the map
        */
        for (let key in wordsToNums) {
            if (string.substring(i).startsWith(key))
                numbers.push(`${wordsToNums[key]}`)
        }
    }

    return numbers
}

const input = getInput(1)?.split('\n')
if (input == undefined) process.exit(1)

/*
First we take the entire array of strings and use the map function to convert all strings to numbers
Then we use map *again* on that array to get the first and last digit.
The process looks like this:

const strings = [
    'one12jfieojfive5'
    'sixsevenifeoeight9',
    'threefourfivesix'
]

.map on the first line converts to ['11255', '6789', '3456']
.map again converts to ['15', '69', '36']
*/
const numbers = input
    .map(line => replaceWordsWithNumbers(line))
    .map(digits => digits[0] + digits[digits.length - 1])

/*
We now map the final numbers array to convert the strings to numbers and then we reduce them by adding each number together
.map on ['15', '69', '36'] gets converted to [15, 69, 36], then .reduce takes each number and adds it together. So, 15 + 69 = 84 + 36 = 120, which is your answer
*/
const answer = numbers.map(number => +number).reduce((a, b) => a + b, 0)

console.log(`The sum is ${answer}`)
