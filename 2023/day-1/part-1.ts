import { getInput } from '../utils/reader'

const input = getInput(1)?.split('\n')
if (input == undefined) process.exit(1)

let sum = 0

for (let string of input) {
    // RegEx to strip all non-numeric characters from the string
    const fullInt = string.replace(/[^0-9]/g, '')

    // Get the first character of the string
    const firstInt = fullInt[0]

    // Get the last character of the string by mod-10'ing it. Math magic. Gotta do +fullInt to convert it to a number since % 10 doesn't work with strings, duh
    const lastInt = +fullInt % 10

    // Even though we're adding a string to a number, since the string is first implicitly converts lastInt to a string as well. Using + on strings just concatenates them
    // Number() converts to a number, which then we add to the sum
    const finalNum = Number(firstInt + lastInt)
    sum += finalNum
}

console.log(`The sum is ${sum}`)
