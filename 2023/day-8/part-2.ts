import { getInput } from '../utils/reader'

const input = getInput(8)?.split('\n')
if (input == undefined) process.exit(1)

function solve(input: string[]) {
    const nodes: Record<string, { L: string; R: string }> = {}

    for (let i = 1; i < input.length; i++) {
        const line = input[i]
        nodes[line.substring(0, 3)] = {
            L: line.substring(7, 10),
            R: line.substring(12, 15),
        }
    }

    const instructions = input[0].split('') as Array<'R' | 'L'>
    const starts = []
    for (const key in nodes) {
        if (
            Object.prototype.hasOwnProperty.call(nodes, key) &&
            key[2] === 'A'
        ) {
            starts.push(key)
        }
    }

    const lengths = starts.map(start => {
        let steps = 0
        let curr = start
        for (let i = 0; curr[2] !== 'Z'; i = (i + 1) % instructions.length) {
            steps++
            curr = nodes[curr][instructions[i]]
        }
        return steps
    })

    function gcd(a: number, b: number) {
        while (b > 0) [a, b] = [b, a % b]
        return a
    }

    function lcm(a: number, b: number) {
        return (a * b) / gcd(a, b)
    }

    return lengths.reduce((n, x) => lcm(x, n), 1)
}

console.log(solve(input))
