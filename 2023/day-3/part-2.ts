import { getInput } from '../utils/reader'

type Map = string[][]

const input = getInput(3)?.split('\n')
if (input == undefined) process.exit(1)

export function parseMap(input: string[]): Map {
    return input.map(line => line.split('').map(char => char))
}

/**
 * @key gear coordinate in `y:x` format
 * @value array of all adjacent numbers
 */
type GearsDict = {
    [key: string]: number[]
}

export function getAllGears(map: Map): GearsDict {
    const result: GearsDict = {}

    for (let y = 0; y < map.length; y++) {
        let numBuffer = ''

        for (let x = 0; x <= map[y].length; x++) {
            const char = map[y][x]

            if (/\d/.test(char)) {
                numBuffer += char
            } else if (numBuffer.length > 0) {
                const gears = getAdjacentGears(map, y, x - numBuffer.length, x)
                for (let gear of gears) {
                    if (gear in result) {
                        result[gear].push(Number(numBuffer))
                    } else {
                        result[gear] = [Number(numBuffer)]
                    }
                }
                numBuffer = ''
            }
        }
    }
    return result
}

export function getAdjacentGears(
    map: Map,
    y: number,
    startX: number,
    endX: number
): string[] {
    const result: string[] = []

    const isGear = (y: number, x: number) => map[y]?.[x] === '*'
    const serialize = (y: number, x: number) => `${y}:${x}`

    for (let x = startX - 1; x <= endX; x++) {
        // above
        if (isGear(y - 1, x)) result.push(serialize(y - 1, x))
        // below
        if (isGear(y + 1, x)) result.push(serialize(y + 1, x))
    }
    // left
    if (isGear(y, startX - 1)) result.push(serialize(y, startX - 1))
    // right
    if (isGear(y, endX)) result.push(serialize(y, endX))

    return result
}

export function filterProperGears(gears: GearsDict): GearsDict {
    return Object.keys(gears)
        .filter(gear => gears[gear].length === 2)
        .reduce((acc, cur) => {
            // @ts-ignore
            acc[cur] = gears[cur]
            return acc
        }, {})
}

export function sumOfGearRatios(gears: GearsDict): number {
    return Object.values(gears)
        .map(([a, b]) => a * b)
        .reduce((a, c) => a + c)
}

console.log(sumOfGearRatios(filterProperGears(getAllGears(parseMap(input)))))
