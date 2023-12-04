import { getInput } from '../utils/reader'

const input = getInput(4)?.split('\n')
if (input == undefined) process.exit(1)

let total = 0

for (let card of input) {
    let winningNums = card
        .split(': ')[1]
        .split(' | ')[0]
        .split(' ')
        .filter(n => n !== '')
    let myNums = card
        .split(': ')[1]
        .split(' | ')[1]
        .split(' ')
        .filter(n => n !== '')

    let foundNums = []

    for (let num of myNums) {
        if (winningNums.indexOf(num) >= 0) foundNums.push(num)
    }

    if (foundNums.length == 0) continue

    let cardValue = 0

    for (let x = 0; x < foundNums.length; x++) {
        if (cardValue == 0) cardValue = 1
        else cardValue *= 2
    }

    total += cardValue
}

console.log(total)
