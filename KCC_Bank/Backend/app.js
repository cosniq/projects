// The main code
const fs = require('fs');

// Read the database
const dbReader = function (fileName) {
  return JSON.parse(fs.readFileSync(`./${fileName}`)).accounts;
};

// Write to database
const dbWriter = function (fileName, accounts) {
  const dataToWrite = JSON.stringify({ accounts });
  fs.writeFileSync(`./${fileName}`, dataToWrite);
};

// For Login
const accountFinder = function (username, password) {
  // Returns an account that's username and password matches
  // If it doens't find the account, it returns null
  const accounts = dbReader(`database.json`);

  const accountToReturn = accounts.find(
    account =>
      account?.username === username && account?.pin === Number(password)
  );

  if (accountToReturn) {
    return accountToReturn;
  } else {
    return null;
  }
};

// For Money Transfer
const transferMoney = function (senderUsername, receiverUsername, amount) {
  // Transfers money from one account to another one
  // If transfer is succesfull: returns 0
  // If sender/receiver account doesn't exist: returns -1
  // If sender === receiver: returns -2
  // If amount is negative or 0: return -3
  // If sender doesn't have enough money: returns -4

  if (typeof amount != 'number') {
    amount = Number(amount);
  }

  if (senderUsername === receiverUsername) {
    return -2;
  }

  const accounts = dbReader(`database.json`);

  const senderAccount = accounts.find(
    account => account.username === senderUsername
  );
  if (senderAccount === undefined) {
    return -1;
  }
  const senderAccountIndex = accounts.indexOf(senderAccount);

  const receiverAccount = accounts.find(
    account => account.username === receiverUsername
  );
  if (receiverAccount === undefined) {
    return -1;
  }
  const receiverAccountIndex = accounts.indexOf(receiverAccount);

  if (amount <= 0) {
    return -3;
  }

  if (senderAccount.balance < amount) {
    return -4;
  }

  senderAccount.movements.push(-amount);
  senderAccount.balance -= amount;

  receiverAccount.movements.push(amount);
  receiverAccount.balance += amount;

  accounts.splice(senderAccountIndex, 1, senderAccount);
  accounts.splice(receiverAccountIndex, 1, receiverAccount);

  dbWriter(`database.json`, accounts);
  return 0;
};

// For Loan
const requestLoan = function (accountUsername, amount) {
  // If the loan is accepted and succesfully written: returns 0
  // If the account doesn't exist: returns -1
  // If the amount is negative or the loan is denied: returns -2
  const accounts = dbReader(`database.json`);

  const currentAccount = accounts.find(
    acc => acc?.username === accountUsername
  );
  if (currentAccount === undefined) {
    return -1;
  }
  const currentAccountIndex = accounts.indexOf(currentAccount);

  if (amount > 0 && currentAccount.movements.some(mov => mov >= amount * 0.1)) {
    currentAccount.movements.push(amount);
    currentAccount.balance += amount;
  } else {
    return -2;
  }

  accounts.splice(currentAccountIndex, 1, currentAccount);

  dbWriter(`database.json`, accounts);
  return 0;
};

// For Close Account
const closeAccount = function (username, password) {
  // If delete is successfull: returns 0
  // If account doesn't exist: returns -1

  const accounts = dbReader(`database.json`);
  const accountToDelete = accounts.find(
    account =>
      account?.username === username && account?.pin === Number(password)
  );

  if (accountToDelete === undefined) {
    return -1;
  }
  const accountToDeleteIndex = accounts.indexOf(accountToDelete);

  accounts.splice(accountToDeleteIndex, 1);

  dbWriter(`database.json`, accounts);
  return 0;
};

exports.login = accountFinder;
exports.transfer = transferMoney;
exports.loan = requestLoan;
exports.closeAccount = closeAccount;
