const app = require(`./app.js`);
const cors = require(`cors`);
const expressFile = require(`express`);

const express = expressFile();

express.use(expressFile.json());
express.use(cors());

express.post(`/`, (req, res) => {
  const body = req.body;
  if (body.type === `login`) {
    res.json(app.login(body.username, body.password));
    console.log(`Account Returned!`);
  }
  if (body.type === `transfer`) {
    res.json(
      app.transfer(body.senderUsername, body.receiverUsername, body.amount)
    );
    console.log(`Transfer Processed!`);
  }
  if (body.type === `loan`) {
    res.json(app.loan(body.username, body.amount));
    console.log(`Loan Processed!`);
  }
  if (body.type === `closeAccount`) {
    res.json(app.closeAccount(body.username, body.password));
    console.log(`Account Closed!`);
  }
});

express.listen(3000, () => console.log(`Server listening on port 3000`));
