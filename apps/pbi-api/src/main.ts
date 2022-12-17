/**
 * This is not a production server yet!
 * This is only a minimal backend to get started.
 */

import * as express from "express";
import * as path from "path";
import * as cors from "cors";
import * as bodyParser from "body-parser";
import * as  sqlite3 from "sqlite3";

interface Pbi {
  id?: number;
  pbi: string;
  project: string;
}

const db = new sqlite3.Database("./Times.db", sqlite3.OPEN_CREATE | sqlite3.OPEN_READWRITE, (err) => {
  if (err) {
    return console.error(err.message);
  }
  console.log("Connected to the SQlite database.");
});

//db.run("DROP TABLE pbi");
db.run("CREATE TABLE IF NOT EXISTS pbi(id INTEGER NOT NULL PRIMARY KEY, name text, project text)");

const app = express();
app.use(cors());
app.use(bodyParser.json());

app.use("/assets", express.static(path.join(__dirname, "assets")));

app.get("/api/pbi", (req, res) => {
  db.all(`SELECT * FROM main.pbi`, (err, rows: Pbi[]) => {
    if (err) {
      console.error(err.message);
    } else {
      res.send(rows);
    }
  });
});

app.post("/api/pbi", (req, res) => {
  const content: Pbi = req.body;
  console.log("Body", content);
  const command = `INSERT INTO pbi(name, project) VALUES('${content.pbi}', '${content.project}')`;
  console.debug(command);
  db.run(command);
  res.send({ message: "Welcome to pbi-api!" });
});

const port = process.env.port || 3333;
const server = app.listen(port, () => {
  console.log(`Listening at http://localhost:${port}/api`);
});
server.on("error", console.error);

