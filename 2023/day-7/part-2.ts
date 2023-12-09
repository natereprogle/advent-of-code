import { getInput } from "../utils/reader";

const input = getInput(7)?.split("\n");
if (input == undefined) process.exit(1);

type GameObject = {
  hand: string;
  bet: number;
};

function parseInput(input: string[]) {
  const gameObjects: GameObject[] = [];

  const lines = input.filter((a) => a !== "");
  lines.map((line) => {
    let [hand, bet] = line.split(" ");
    gameObjects.push({ hand, bet: Number(bet) });
  });

  return gameObjects;
}

function getCardCounts(hand: string) {
  // Define an object in which the keys are either strings or numbers, and the values are numbers.
  // This way, typescript doesn't complain about attempting to index an object of type '{}' with a string, and we don't have to define the actual object up front
  let counts: { [x: string | number]: number } = {};

  for (let card of hand) {
    if (!counts[card]) {
      counts[card] = 1;
      continue;
    }
    counts[card]++;
  }
  return counts;
}

function isHighCard(counts: any) {
  for (let key in counts) {
    if (counts[key] > 1) {
      return false;
    }
  }
  return true;
}

function isOnePair(counts: any) {
  let pairsCount = 0;
  for (let key in counts) {
    if (counts[key] === 2) {
      pairsCount++;
    }
  }
  return pairsCount === 1;
}

function isTwoPair(counts: any) {
  let pairsCount = 0;
  for (let key in counts) {
    if (counts[key] === 2) {
      pairsCount++;
    }
  }
  return pairsCount === 2;
}

function isFullHouse(counts: any) {
  let roof = false;
  let base = false;
  for (let key in counts) {
    if (counts[key] === 2) {
      roof = true;
    }
    if (counts[key] === 3) {
      base = true;
    }
  }
  return roof && base;
}

function isThreeOfAkind(counts: any) {
  if (isFullHouse(counts)) {
    return false;
  }
  for (let key in counts) {
    if (counts[key] === 3) {
      return true;
    }
  }
  return false;
}

function isFourOfAKind(counts: any) {
  for (let key in counts) {
    if (counts[key] === 4) {
      return true;
    }
  }
  return false;
}

function isFiveOfAKind(counts: any) {
  for (let key in counts) {
    if (counts[key] === 5) {
      return true;
    }
  }
  return false;
}

function getHandWeight(hand: string) {
  const counts = getCardCounts(hand);
  switch (true) {
    case isFiveOfAKind(counts):
      return 7;
    case isFourOfAKind(counts):
      return 6;
    case isFullHouse(counts):
      return 5;
    case isThreeOfAkind(counts):
      return 4;
    case isTwoPair(counts):
      return 3;
    case isOnePair(counts):
      return 2;
    case isHighCard(counts):
      return 1;
    default:
      return 0;
  }
}

function findHighestWeight(hand: string) {
  const cardOptions = [
    "A",
    "K",
    "Q",
    "T",
    "9",
    "8",
    "7",
    "6",
    "5",
    "4",
    "3",
    "2",
  ];

  let jokerCount = 0;
  let jokerLocations = [];

  // Example Hand: AJ24J
  // All comments explaining this will use this example hand

  for (let i = 0; i < hand.length; i++) {
    if (hand[i] === "J") {
      jokerCount++;
      jokerLocations.push(i);
    }
  }

  // jokerCount now equals 2
  // jokerLocation now equals [1, 4]

  if (jokerCount === 0) {
    return getHandWeight(hand);
  }

  let maxWeight = 0;
  const loopCount = cardOptions.length ** jokerCount;

  // loopCount now equals 12 ^ 2, or 144
  // This loop essentially goes through all possible variants of hands with Jokers in them.
  for (let i = 0; i <= loopCount; i++) {
    let checkHand;

    const shift1 = i;
    // Let's just use i = 0 for now. shift1 = 0
    const shift2 = shift1 % cardOptions.length;
    // shift2 = 0 % 12, which is 0
    const shift3 = shift2 % cardOptions.length;
    // shift3 = 0 % 12, which is 0

    if (jokerCount === 1) {
      checkHand = hand.split("");
      checkHand[jokerLocations[0]] = cardOptions[shift1];
      checkHand = checkHand.join("");
    }

    if (jokerCount === 2) {
      checkHand = hand.split("");
      // checkHand = ['A', 'J', '2', '4', 'J']
      checkHand[jokerLocations[0]] = cardOptions[shift2];
      // cardOptions[shift2] = cardOptions[0] = 'A', which we're setting to checkHand[jokerLocation[0]]. jokerLocation[0] is 1, so checkHand[1] is being replaced with 'A'
      // checkHand now equals ['A', 'A', '2', '4', 'J']
      checkHand[jokerLocations[1]] = cardOptions[shift1];
      // Same thing as above since shift3 is also 0
      // checkHand now equals ['A', 'A', '2', '4', 'A']
      checkHand = checkHand.join("");
      // checkHand now equals AA24A
    }

    if (jokerCount === 3) {
      checkHand = hand.split("");
      checkHand[jokerLocations[0]] = cardOptions[shift3];
      checkHand[jokerLocations[1]] = cardOptions[shift2];
      checkHand[jokerLocations[2]] = cardOptions[shift1];
      checkHand = checkHand.join("");
    }

    if (jokerCount === 5 || jokerCount === 4) {
      maxWeight = 7;
      break;
    }

    // Get the standard weight of the hand
    // currentWeight is now 4
    const currentWeight = getHandWeight(checkHand as string);
    maxWeight = Math.max(currentWeight, maxWeight);
    // maxWeight is now 4

    if (maxWeight === 7) {
      break;
    }
  }

  // Return max weight
  return maxWeight;
}

function hand1IsWinner(hand1: string, hand2: string) {
  const hand1Weight = findHighestWeight(hand1);
  const hand2Weight = findHighestWeight(hand2);

  if (hand1Weight === hand2Weight) {
    return handleSameType(hand1, hand2, true);
  } else {
    return hand1Weight > hand2Weight;
  }
}

function handleSameType(hand1: string, hand2: string, joker: boolean) {
  const cardWeights = getCardWeights(joker);
  for (let i = 0; i < hand1.length; i++) {
    const check1 = hand1[i];
    const check2 = hand2[i];
    const hand1Wins = cardWeights[check1] > cardWeights[check2];
    const hand2Wins = cardWeights[check1] < cardWeights[check2];
    if (hand1Wins) {
      return true;
    } else if (hand2Wins) {
      return false;
    }
  }
  // it's the same hand
  return true;
}

function getCardWeights(joker: boolean): { [x: string | number]: number } {
  let cardWeights;
  if (!joker) {
    cardWeights = {
      2: 1,
      3: 2,
      4: 3,
      5: 4,
      6: 5,
      7: 6,
      8: 7,
      9: 8,
      T: 9,
      J: 10,
      Q: 11,
      K: 12,
      A: 13,
    };
  } else {
    cardWeights = {
      2: 1,
      3: 2,
      4: 3,
      5: 4,
      6: 5,
      7: 6,
      8: 7,
      9: 8,
      T: 9,
      J: 0,
      Q: 11,
      K: 12,
      A: 13,
    };
  }

  return cardWeights;
}

function sortGames(games: GameObject[]) {
  return games.sort((game1, game2) => {
    if (hand1IsWinner(game1.hand, game2.hand)) {
      return 1;
    } else if (hand1IsWinner(game2.hand, game1.hand)) {
      return -1;
    } else {
      return 0;
    }
  });
}

function getMultipliedBetSum(sortedGames: GameObject[]) {
  let rank = 1;
  let res = 0;
  for (let game of sortedGames) {
    res += game.bet * rank;
    rank++;
  }
  return res;
}

const sortedGames = sortGames(parseInput(input));
console.log(getMultipliedBetSum(sortedGames));
