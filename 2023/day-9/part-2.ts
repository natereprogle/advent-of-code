import { getInput } from '../utils/reader'

const input = getInput(9)?.split('\n')
if (input == undefined) process.exit(1)

function extrapolate(array: number[]): number {
    if (array.every(val => val === 0)) return 0

    const deltas: number[] = array.slice(1).map((y, index) => y - array[index])
    const diff = extrapolate(deltas)
    return array[0] - diff
}

let total = 0

for (let line of input) {
    total += extrapolate(line.split(' ').map(v => +v))
}

console.log(total)
