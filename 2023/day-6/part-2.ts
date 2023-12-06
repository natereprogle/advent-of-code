import { getInput } from '../utils/reader'

const input = getInput(6)?.split('\n')
if (input == undefined) process.exit(1)

console.time('Part 2')
const time = input[0].split(': ')[1].trim().replace(/\s/g, '')
const distance = input[1].split(': ')[1].replace(/\s/g, '')

const waysToWin = []

let roundsWon = 0

for (let buttonHeld = +time; buttonHeld > 0; buttonHeld--) {
    // Round time is the max time for the round
    // `j` is the time we spent holding the button
    if (+time - buttonHeld <= 0 || +time == buttonHeld) continue

    // Remaining round time is the time remaining after the button has been held
    const remainingRoundTime = +time - buttonHeld

    // Get the max distance we can travel this round
    const travelDistanceMax = buttonHeld * remainingRoundTime

    if (travelDistanceMax > +distance) roundsWon += 1
}

waysToWin.push(roundsWon)

console.log(waysToWin.reduce((a, b) => a * b))
console.timeEnd('Part 2')
