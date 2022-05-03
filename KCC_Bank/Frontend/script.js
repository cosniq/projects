'use strict';

/////////////////////////////////////////////////
/////////////////////////////////////////////////
// BANKIST APP

// Elements
const loginPart = document.querySelector('.login');
const labelWelcome = document.querySelector('.welcome');
const labelDate = document.querySelector('.date');
const labelBalance = document.querySelector('.balance__value');
const labelSumIn = document.querySelector('.summary__value--in');
const labelSumOut = document.querySelector('.summary__value--out');
const labelSumInterest = document.querySelector('.summary__value--interest');
const labelTimer = document.querySelector('.timer');

const containerApp = document.querySelector('.app');
const containerMovements = document.querySelector('.movements');

const btnLogin = document.querySelector('.login__btn');
const btnTransfer = document.querySelector('.form__btn--transfer');
const btnLoan = document.querySelector('.form__btn--loan');
const btnClose = document.querySelector('.form__btn--close');
const btnSort = document.querySelector('.btn--sort');
const btnLogout = document.querySelector('.logout__btn');

const inputLoginUsername = document.querySelector('.login__input--user');
const inputLoginPin = document.querySelector('.login__input--pin');
const inputTransferTo = document.querySelector('.form__input--to');
const inputTransferAmount = document.querySelector('.form__input--amount');
const inputLoanAmount = document.querySelector('.form__input--loan-amount');
const inputCloseUsername = document.querySelector('.form__input--user');
const inputClosePin = document.querySelector('.form__input--pin');

const displayMovements = function (movements) {
  containerMovements.innerHTML = '';
  movements.forEach(function (mov, i) {
    const type = mov > 0 ? `deposit` : `withdrawal`;
    const html = `
        <div class="movements__row">
          <div class="movements__type movements__type--${type}">${
      i + 1
    } ${type}</div>
          <div class="movements__value">${mov} €</div>
        </div>`;

    containerMovements.insertAdjacentHTML(`afterbegin`, html);
  });
};

const printBalance = function (account) {
  labelBalance.textContent = `${account.balance} €`;
};

const calcDisplaySummary = function (account) {
  const incomes = account.movements
    .filter(mov => mov > 0)
    .reduce((acc, cur) => acc + cur, 0);
  labelSumIn.textContent = `${incomes} €`;

  const outgoing = account.movements
    .filter(mov => mov < 0)
    .reduce((acc, cur) => acc + cur, 0);
  labelSumOut.textContent = `${Math.abs(outgoing)} €`;

  const interest = account.movements
    .filter(mov => mov > 0)
    .map(deposit => (deposit * account.interestRate) / 100)
    .filter((interest, _, arr) => {
      // console.log(arr);
      return interest >= 1;
    })
    .reduce((acc, cur) => acc + cur, 0);

  labelSumInterest.textContent = `${interest.toFixed(2)} €`;
};

// HTTP Client
const httpClient = function (req) {
  try {
    const Http = new XMLHttpRequest();
    const url = `http://localhost:3000`;
    Http.open(`POST`, url, false);
    Http.setRequestHeader(`Content-Type`, `application/json`);
    let json;

    Http.onreadystatechange = function () {
      if (Http.readyState === 4 && Http.status === 200) {
        json = JSON.parse(Http.responseText);
      }
    };

    Http.send(req);
    return json;
  } catch (error) {
    console.log(`Error Catched!`);
    alert(`Unable to connect to backend service!`);
    return -10;
  }
};

// LOGIN
let currentAccount;

btnLogin.addEventListener(`click`, function (e) {
  // Prevent form from submitting
  e.preventDefault();

  const request = JSON.stringify({
    type: 'login',
    username: inputLoginUsername.value,
    password: Number(inputLoginPin.value),
  });

  currentAccount = httpClient(request);

  if (currentAccount != null && currentAccount != -10) {
    console.log(`Account owner: ${currentAccount.owner}`);
    // DISPLAY UI and message
    labelWelcome.textContent = `Welcome back, ${
      currentAccount.owner.split(` `)[0]
    }!`;

    containerApp.style.opacity = 100;

    // Clear the input fields
    inputLoginUsername.value = inputLoginPin.value = ``;
    inputLoginPin.blur();

    // Update the UI
    updateUI(currentAccount);

    loginPart.style.display = 'none';
    btnLogout.style.display = 'block';
  } else if (currentAccount === null) {
    console.log(`Error code null`);
    alert(`Bad username or password!\nTry again!`);
  }
});

btnLogout.addEventListener(`click`, function () {
  containerApp.style.opacity = 0;
  labelWelcome.textContent = `Log in to get started`;
  loginPart.style.display = 'block';
  btnLogout.style.display = 'none';
});

const updateUI = function (account) {
  // Display movements
  displayMovements(account.movements);
  // Display balance
  printBalance(account);
  // Display summary
  calcDisplaySummary(account);
};

// TRANSFERS

btnTransfer.addEventListener(`click`, function (e) {
  e.preventDefault();

  const transferAmount = Number(inputTransferAmount.value);

  const request = JSON.stringify({
    type: 'transfer',
    senderUsername: currentAccount.username,
    receiverUsername: inputTransferTo.value,
    amount: transferAmount,
  });

  const response = httpClient(request);

  if (response === -1) {
    console.log(`Error code -1`);
    alert(`Sender/Receiver account doesn't exist!`);
  }
  if (response === -2) {
    console.log(`Error code -2`);
    alert(`Receiver is the same as sender!`);
  }
  if (response === -3) {
    console.log(`Error code -3`);
    alert(`Amount isn't a positive number!`);
  }
  if (response === -4) {
    console.log(`Error code -4`);
    alert(`Sender's balance is too small!`);
  }
  if (response === 0) {
    console.log(`Response code 0`);
    const loginRequest = JSON.stringify({
      type: 'login',
      username: currentAccount.username,
      password: currentAccount.pin,
    });

    currentAccount = httpClient(loginRequest);

    updateUI(currentAccount);

    inputTransferTo.value = ``;
    inputTransferAmount.value = ``;
    inputTransferAmount.blur();
    inputTransferTo.blur();
  }
});

// Loan feature
btnLoan.addEventListener(`click`, function (e) {
  e.preventDefault();

  const loanAmount = Number(inputLoanAmount.value);

  const request = JSON.stringify({
    type: 'loan',
    username: currentAccount.username,
    amount: loanAmount,
  });

  const response = httpClient(request);

  if (response === 0) {
    console.log(`Response code 0`);
    const loginRequest = JSON.stringify({
      type: 'login',
      username: currentAccount.username,
      password: currentAccount.pin,
    });

    currentAccount = httpClient(loginRequest);

    // Update the UI
    updateUI(currentAccount);

    // Clear the inputfield
    inputLoanAmount.value = ``;
    inputLoanAmount.blur();
  }
  if (response === -1) {
    console.log(`Error code -1`);
    alert(`Account doesn't exist!`);
  }
  if (response === -2) {
    console.log(`Error code -2`);
    alert(`Loan denied or amount is negative!`);
  }
});

// Close account

btnClose.addEventListener(`click`, function (e) {
  e.preventDefault();

  if (
    currentAccount.username === inputCloseUsername.value &&
    currentAccount.pin == inputClosePin.value
  ) {
    const request = JSON.stringify({
      type: 'closeAccount',
      username: inputCloseUsername.value,
      password: inputClosePin.value,
    });

    const response = httpClient(request);

    if (response === 0) {
      console.log(`Response code 0`);
      // Hide UI
      containerApp.style.opacity = 0;
      inputClosePin.value = inputCloseUsername.value = ``;
      inputClosePin.blur();
      inputCloseUsername.blur();
      labelWelcome.textContent = `Log in to get started`;
      loginPart.style.display = 'block';
      btnLogout.style.display = 'none';
    }
    if (response === -1) {
      console.log(`Error code -1`);
      alert(`An error occurred!`);
    }
  } else {
    const errorMessage = `Data doesn't match!`;
    console.log(errorMessage);
    alert(errorMessage);
  }
});

const movements = [200, 450, -400, 3000, -650, -130, 70, 1300];
const eurToUsd = 1.1;

// MODIFY DATE
const setDate = function () {
  var today = new Date();
  var dd = String(today.getDate()).padStart(2, '0');
  var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
  var yyyy = today.getFullYear();

  today = dd + '/' + mm + '/' + yyyy;
  labelDate.textContent = today;
};

setDate();
