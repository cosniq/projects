/*
// Coding challenge 1
const checkDogs = function (dogsJulia, dogsKate) {
  const dogsJuliaCorrect = dogsJulia.slice(1, -2);

  const dogs = [...dogsJuliaCorrect, ...dogsKate];
  dogs.forEach(function (dog, i, dogs) {
    const result = `Dog number ${i + 1} is ${
      dog >= 3 ? `an adult and is ${dog} years old` : `still a puppy 🐶`
    }`;
    console.log(result);
  });
};

checkDogs([3, 5, 2, 12, 7], [4, 1, 15, 8, 3]);
console.log(`---- SECOND SET ----`);
checkDogs([9, 16, 6, 8, 3], [10, 5, 6, 1, 4]);
*/

/*
// Coding challenge 2
const calcAverageHumanAge = function (ages) {
  humanAges = ages.map(current =>
    current <= 2 ? 2 * current : 16 + 4 * current
  );

  remainingDogs = humanAges.filter(current => current >= 18);

  console.log(remainingDogs);

  const average = remainingDogs.reduce(
    (acc, cur, _, arr) => acc + cur / arr.length,
    0
  );
  return average;
};

console.log(calcAverageHumanAge([5, 2, 4, 1, 15, 8, 3]));
console.log(calcAverageHumanAge([16, 6, 10, 5, 6, 1, 4]));
*/
/*
// Coding challenge 4
const calcAverageHumanAge2 = function (ages) {
  const average = ages
    .map(curr => (curr <= 2 ? 2 * curr : 16 + 4 * curr))
    .filter(curr => curr >= 18)
    .reduce((acc, cur, _, arr) => acc + cur / arr.length, 0);
  return average;
};

console.log(calcAverageHumanAge2([5, 2, 4, 1, 15, 8, 3]));
console.log(calcAverageHumanAge2([16, 6, 10, 5, 6, 1, 4]));
*/
