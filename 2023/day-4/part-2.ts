import { getInput } from '../utils/reader'

const input = getInput(4)?.split('\n')
if (input == undefined) process.exit(1)

type Card = {
    num: number
    winning: string[]
    have: string[]
}

let result = 0

// Basically take our input array and turn it into an array of objects for further processing
const cards = input.map((line, num) => {
    let [left, right] = line.split(': ')[1].split(' | ')
    const winning = left
        .split(' ')
        .map(v => v.trim())
        .filter(v => v)
    const have = right
        .split(' ')
        .map(v => v.trim())
        .filter(v => v)
    return { num, winning, have }
})

const wonCards = new Map<number, Card[]>()

cards
    .map((card: Card) => card.have.filter(h => card.winning.includes(h)).length)
    .forEach((numWon, cardNum) => {
        wonCards.set(cardNum, cards.slice(cardNum + 1, cardNum + numWon + 1))
    })

while (cards.length > 0) {
    result++
    const c = cards.pop()!
    cards.push(...wonCards.get(c.num)!)
}

console.log(result)
