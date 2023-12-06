import { getInput } from '../utils/reader'

const input = getInput(6)?.split('\n')
if (input == undefined) process.exit(1)

console.time("Part 1")
const time = input[0]
    .split(': ')[1]
    .split(' ')
    .filter(x => x)
const distance = input[1]
    .split(': ')[1]
    .split(' ')
    .filter(x => x)

const waysToWin = []

for (let i = 0; i < time.length; i++) {
    const roundTime = time[i]
    const roundDist = distance[i]
    let roundsWon = 0

    for (let buttonHeld = +roundTime; buttonHeld > 0; buttonHeld--) {
        // Round time is the max time for the round
        // `j` is the time we spent holding the button
        if (+roundTime - buttonHeld <= 0 || +roundTime == buttonHeld) continue

        // Remaining round time is the time remaining after the button has been held
        const remainingRoundTime = +roundTime - buttonHeld

        // Get the max distance we can travel this round
        const travelDistanceMax = buttonHeld * remainingRoundTime

        if (travelDistanceMax > +roundDist) roundsWon += 1
    }
    waysToWin.push(roundsWon)
}

console.log(waysToWin.reduce((a, b) => a * b))
console.timeEnd("Part 1")
