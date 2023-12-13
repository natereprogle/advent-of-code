import { getInput } from '../utils/reader'

const input = getInput(10)?.split('\n')
if (input == undefined) process.exit(1)

interface Coordinate {
    row: number
    col: number
}

let sr: number = -1
let sc: number = -1

outer: for (let r = 0; r < input.length; r++) {
    const row: string = input[r]
    for (let c = 0; c < row.length; c++) {
        const ch: string = row[c]
        if (ch === 'S') {
            sr = r
            sc = c
            break outer
        }
    }
}

const loop: Set<string> = new Set<string>()
const q: Coordinate[] = [{ row: sr, col: sc }]

while (q.length > 0) {
    const { row, col }: Coordinate = q.shift() as Coordinate
    const ch: string = input[row][col]

    if (
        row > 0 &&
        'S|JL'.includes(ch) &&
        '|7F'.includes(input[row - 1][col]) &&
        !loop.has(`${row - 1},${col}`)
    ) {
        loop.add(`${row - 1},${col}`)
        q.push({ row: row - 1, col })
    }

    if (
        row < input.length - 1 &&
        'S|7F'.includes(ch) &&
        '|JL'.includes(input[row + 1][col]) &&
        !loop.has(`${row + 1},${col}`)
    ) {
        loop.add(`${row + 1},${col}`)
        q.push({ row: row + 1, col })
    }

    if (
        col > 0 &&
        'S-J7'.includes(ch) &&
        '-LF'.includes(input[row][col - 1]) &&
        !loop.has(`${row},${col - 1}`)
    ) {
        loop.add(`${row},${col - 1}`)
        q.push({ row, col: col - 1 })
    }

    if (
        col < input[row].length - 1 &&
        'S-LF'.includes(ch) &&
        '-J7'.includes(input[row][col + 1]) &&
        !loop.has(`${row},${col + 1}`)
    ) {
        loop.add(`${row},${col + 1}`)
        q.push({ row, col: col + 1 })
    }
}

// I initially did Math.floor but it wasn't right, so I did Math.ciel and it took 🤷🏻‍♂️
console.log(Math.ceil(loop.size / 2))
