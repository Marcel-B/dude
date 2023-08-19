import * as express from "express";
import * as path from "path";
import * as cors from "cors";
import * as bodyParser from "body-parser";
import * as  sqlite3 from "sqlite3";
import { Projekt, Pbi } from "@dude/stunden-domain";
import { Eintrag } from "@dude/stunden-domain";


const db = new sqlite3.Database("./db/Times.db", sqlite3.OPEN_CREATE | sqlite3.OPEN_READWRITE, (err) => {
  if (err) {
    return console.error(err.message);
  }
  console.log("Connected to the SQlite database.");
});

//db.run("DROP TABLE eintrag");
//db.run("DROP TABLE pbi");
db.run(`create table if not exists pbi(id INTEGER NOT NULL PRIMARY KEY, name TEXT, projektId TEXT, FOREIGN KEY(projektId) references projekt(id))`);
db.run(`create table if not exists projekt(id TEXT NOT NULL PRIMARY KEY, name TEXT)`);
db.run(`create table if not exists eintrag(id INTEGER NOT NULL PRIMARY KEY, text TEXT, stunden REAL, datum TEXT, abrechenbar BOOLEAN)`);

const app = express();
app.use(cors());
app.use(bodyParser.json());

app.use("/assets", express.static(path.join(__dirname, "assets")));

app.delete("/api/table", (req, res) => {
  try {
    db.run("DROP TABLE eintrag");
    db.run("DROP TABLE pbi");
    db.run("DROP TABLE projekt");
    db.run(`create table if not exists pbi(id INTEGER NOT NULL PRIMARY KEY, name TEXT, projektId TEXT, FOREIGN KEY(projektId) references projekt(id))`);
    db.run(`create table if not exists projekt(id TEXT NOT NULL PRIMARY KEY, name TEXT)`);
    db.run(`create table if not exists eintrag(id INTEGER NOT NULL PRIMARY KEY, text TEXT, stunden REAL, datum TEXT, abrechenbar BOOLEAN)`);
  } catch (e) {
    console.error(e);
  }
});

app.get("/api/pbi", (req, res) => {
  db.all(`SELECT * FROM main.pbi`, (err, rows: Pbi[]) => {
    if (err) {
      console.error(err.message);
    } else {
      res.send(rows);
    }
  });
});

app.delete("/api/pbi/:id", (req, res) => {
  db.run(`DELETE FROM main.pbi WHERE id = ?`, [req.params.id], (err) => {
    if (err) {
      console.error(err.message);
    } else {
      res.send("ok");
    }
  });
});

app.post("/api/pbi", (req, res) => {
  const pbi: Pbi = req.body;
  console.log("Body", pbi);
  const command = `INSERT INTO pbi(name, projektId) VALUES('${pbi.name}', '${pbi.projektId}')`;
  console.debug(command);
  db.run(command);
  db.get(`SELECT * FROM pbi WHERE id = last_insert_rowid()`, (err, row: Pbi) => {
    res.send(row);
  });
});

app.post("/api/eintrag", (req, res) => {
  const eintrag: Eintrag = req.body;
  console.log("Body", eintrag);
  const command = `INSERT INTO eintrag(text, stunden, abrechenbar, datum) VALUES('${eintrag.text}', '${eintrag.stunden}', '${eintrag.abrechenbar}', '${eintrag.datum}')`;
  console.debug(command);
  db.run(command);
  db.get(`SELECT * FROM eintrag WHERE id = last_insert_rowid()`, (err, row: Eintrag) => {
    res.send(row);
  });
});

app.get("/api/eintrag", (req, res) => {
  db.all(`SELECT * FROM main.eintrag`, (err, rows: Eintrag[]) => {
    if (err) {
      console.error(err.message);
    } else {
      res.send(rows);
    }
  });
});

app.get("/api/projekt", (req, res) => {
  db.all(`SELECT * FROM main.projekt`, (err, rows: Projekt[]) => {
    if (err) {
      console.error(err.message);
    } else {
      res.send(rows);
    }
  });
});

app.post("/api/projekt", (req, res) => {
  try {
    const projekt: Projekt = req.body;
    console.log("Body", projekt);
    const command = `INSERT INTO projekt(id, name) VALUES('${projekt.id}', '${projekt.name}')`;
    console.debug(command);
    db.run(command);
    res.send(projekt);
  } catch (e) {
    console.error(e);
    res.send("error");
  }
});

app.get("/api/projekt/:projektId", (req, res) => {
  const projektId = req.params.projektId;
  db.get(`SELECT * FROM main.projekt WHERE id = '${projektId}'`, (err, rows: Projekt[]) => {
    if (err) {
      console.error(err.message);

    } else {
      res.send(rows);
    }
  });
});

app.delete("/api/projekt/:id", (req, res) => {
  db.run(`DELETE FROM main.projekt WHERE id = ?`, [req.params.id], (err) => {
    if (err) {
      console.error(err.message);
    } else {
      res.send("ok");
    }
  });
});

const port = process.env.port || 3333;

const server = app.listen(port, () => {
  console.log(`Listening at http://localhost:${port}/api`);
});
server.on("error", console.error);


