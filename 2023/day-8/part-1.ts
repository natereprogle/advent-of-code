import { getInput } from '../utils/reader'

const input = getInput(8)?.split('\n')
if (input == undefined) process.exit(1)

const instructions = input.shift()!.split('')
input.shift() //Get rid of blank element

function inputToInstructionMap(
    instructions: string[]
): Map<string, [string, string]> {
    const instructionSet = new Map<string, [string, string]>()

    for (let instruction of instructions) {
        const [key, val] = instruction.split(' = ')
        let [left, right] = val.split(', ')
        left = left.substring(1)
        right = right.substring(0, right.length - 1)

        instructionSet.set(key, [left, right])
    }

    return instructionSet
}

const instructionMap = inputToInstructionMap(input)

let steps = 0
let curStep = 'AAA'

function countSteps(curStep: string) {
    for (let instruction of instructions) {
        let curInstruction = instructionMap.get(curStep)!
        if (instruction === 'L') {
            curStep = curInstruction[0]
        } else {
            curStep = curInstruction[1]
        }

        steps++

        // If we've found the end, no need to continue looping through the instruction set
        if (curStep === 'ZZZ') break
    }

    // If the end isn't found yet, run the loop again with the current step in mind
    if (curStep !== 'ZZZ') {
        countSteps(curStep)
    }

    return steps
}

console.log(countSteps(curStep))
