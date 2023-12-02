import { getInput } from '../utils/reader'

const input = getInput(2)?.split('\n')
if (input == undefined) process.exit(1)

const validGames = []

// We can only have 12 red, 13 green, or 14 blue per round
for (const game of input) {
    let rounds = game.split(': ')[1].split('; ')
    let id = +game.split(': ')[0].replace(/[^0-9]/g, '')
    let valid = true

    for (const round of rounds) {
        let cubes = round.split(', ')

        for (const cube of cubes) {
            let number = cube.replace(/[^0-9]/g, '')

            if (cube.includes('red')) {
                if (+number > 12) valid = false
            } else if (cube.includes('green')) {
                if (+number > 13) valid = false
            } else {
                if (+number > 14) valid = false
            }
        }
    }

    if (valid) {
        validGames.push(id)
    }
}

console.log(validGames.reduce((a, b) => a + b))
