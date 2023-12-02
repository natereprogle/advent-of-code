import { getInput } from '../utils/reader'

const input = getInput(2)?.split('\n')
if (input == undefined) process.exit(1)

const gamePowers = []

// We can only have 12 red, 13 green, or 14 blue per round
for (const game of input) {
    let rounds = game.split(': ')[1].split('; ')

    let red = 0,
        blue = 0,
        green = 0

    for (const round of rounds) {
        let cubes = round.split(', ')

        for (const cube of cubes) {
            let number = cube.replace(/[^0-9]/g, '')

            if (cube.includes('red')) {
                if (+number > red) red = +number
            } else if (cube.includes('green')) {
                if (+number > green) green = +number
            } else {
                if (+number > blue) blue = +number
            }
        }
    }

    gamePowers.push(red * green * blue)
}

console.log(gamePowers.reduce((a, b) => a + b))
